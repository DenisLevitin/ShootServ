using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
using DAL;
using System.IO;
using OpenXmlHelpers;

namespace BL
{
    /// <summary>
    /// БЛ для заявок
    /// </summary>
    public class EntryForCompetitionsLogic
    {
        private readonly EFEntryForCompetitions _dalEntryForCompetitions;
        private readonly ShooterLogic _shooterLogic;
        private readonly UserLogic _userLogic;
        private readonly EFCupCompetitionType _cupCompetitionType;
        private readonly CupLogic _cupLogic;
        private readonly ShootingClubLogic _clubLogic;

        public EntryForCompetitionsLogic()
        {
            _dalEntryForCompetitions = new EFEntryForCompetitions();
            _shooterLogic = new ShooterLogic();
            _userLogic = new UserLogic();
            _cupCompetitionType = new EFCupCompetitionType();
            _cupLogic = new CupLogic();
            _clubLogic = new ShootingClubLogic();
        }

       /// <summary>
       /// Создать заявку
       /// </summary>
       /// <param name="idUser">ид. пользователя</param>
       /// <param name="idCup">ид. соревнования</param>
       /// <param name="idCompetitionType">ид. типу упражнения</param>
       /// <returns></returns>
        public ResultInfo CreateEntry(int idUser, int idCup, int idCompetitionType)
        {
            var res = new ResultInfo();

           var cupCompType = _cupCompetitionType.GetByCupAndCompType(idCup, idCompetitionType);
           var user = _userLogic.Get(idUser);

           if (user.IdRole == (int) RolesEnum.Shooter)
           {
               var cup = _cupLogic.Get(idCup);
               if (cup.DateEnd > DateTime.Now.AddHours(8)) // TODO: Если будет нужно проработать сдвиг во времени по часовому поясу
               {
                   var shooter = _shooterLogic.GetByUser(idUser);

                   var entry = new EntryForCompetitionsParams
                   {
                       DateCreate = DateTime.Now,
                       IdCupCompetitionType = cupCompType.Id,
                       IdEntryStatus = (int)EntryStatusParams.EntryStatusEnum.Waiting,
                       IdShooter = shooter.Id
                   };

                   res = _dalEntryForCompetitions.Add(entry);
               }
               else 
               {
                   res.IsOk = false;
                   res.ErrorMessage = "Нельзя заявиться на соревнования, которые закончились";
               }
               
           }
           else
           {
               res.IsOk = false;
               res.ErrorMessage = "Пользователь не может заявиться, так как не является стрелком";
           }

           

            return res;
        }

        /// <summary>
        /// Создать печатную форму заявки в Excel
        /// </summary>
        /// <param name="path">путь до файла шаблона</param>
        /// <param name="entryDetails">список заявленных</param>
        /// <param name="cup">соревнование</param>
        /// <param name="town">город</param>
        /// <param name="club">клуб</param>
        /// <returns></returns>
        private ResultInfoRef<ByteRes> CreateExcelEntryPrint(string path, List<ShooterEntryDetailsParams> entryDetails, CupParams cup, string town, SexEnum sex, ShooterClubParams club = null)
        {
            var res = new ResultInfoRef<ByteRes>();

            try
            {
                var templateStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                var ms = new MemoryStream();
                templateStream.CopyTo(ms, 64); // C размером буфера не уверен
                templateStream.Close();

                var workshheet = OpenXmlHelper.GetWorksheet(ms, 1);
                //OpenXmlHelper.GetRanges(ms, 1, "A10", "G12");
                var competiotionsNames = entryDetails.SelectMany( x => x.Competitions ).Distinct().ToList();

                workshheet.Cell(10, 6).Value = sex == SexEnum.Men ? "Выполняемые упражнения, мужчины" : "Выполняемые упражнения, женщины";

                workshheet.Cell(4, 2).Value = club != null ? club.Name : "";
                workshheet.Cell(6, 3).Value = cup.Name;
                workshheet.Cell(8, 2).Value = string.Format("в г. {0}", town);
                workshheet.Cell(8, 5).Value = cup.DateStart.ToString("dd.MM.yyyy");
                workshheet.Cell(8, 6).Value = "по " + cup.DateEnd.ToString("dd.MM.yyyy");

                int row = 13;
                int j = 1;
                foreach(var item in entryDetails)
                {
                    var col = 1;

                    workshheet.Cell(row, 1).Value = j.ToString();
                    workshheet.Cell(row, 2).Value = string.Format("{0} {1} {2}", item.FamilyName, item.Name, item.FatherName);
                    workshheet.Cell(row, 3).Value = item.Category;
                    workshheet.Cell(row, 4).Value = item.Town;
                    workshheet.Cell(row, 5).Value = item.BirthDate.ToString("dd.MM.yyyy");

                    var sb = new StringBuilder();
                    item.Competitions.ToList().ForEach( x => sb.Append(x + " "));
                    workshheet.Cell(row, 6).Value = sb.ToString();

                    for (int i = 1; i <= 7; i++)
                    {
                        workshheet.Cell(row, i).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    }

                    if (j > 2)
                    {
                        workshheet.Row(row).InsertRowsBelow(1);
                    }

                    j++;
                    row++;
                }

                OpenXmlHelper.Save(ms, workshheet.Workbook);

                res.Data.Bytes = ms.ToArray();


            }
            catch (Exception exc)
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "Произошла ошибка при обработки файла шаблона заявки";
                res.Result.Exc = exc;
            }

            return res;

        }

        /// <summary>
        /// Печать бланка заявки
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <param name="idClub">ид. стрелового клуба</param>
        /// <returns></returns>
        public ResultInfoRef<FileContent> PrintEntryList(string path, int idCup, SexEnum sex, int idClub = - 1)
        {
            var res = new ResultInfoRef<FileContent>();
            res.Data = new FileContent(); /// TODO: Приходится писать вот такую заглушку 

            //1. Получаем данные о заявках
            var queryEntrysList = idClub != -1 ? _shooterLogic.GetEntryShootersOnCupAndClub(idCup, idClub, true, sex) : _shooterLogic.GetEntryShootersOnCup(idCup, true, sex);

            //2. Если есть данные о клубе, то мы редактируем в шаблоне данные о клубе
            ShooterClubParams club = null;
            var cup = _cupLogic.Get(idCup);

            var town = new ShootingRangeLogic().Get(cup.IdShootingRange).Town;
            string sexStr = sex == SexEnum.Men ? "мужчины" : "женщины";
            if (idClub != -1)
            {
                club = _clubLogic.Get(idClub);
                res.Data.FileName = string.Format("Заявка на соренование {0} от клуба {1} ,{2}.xlsx", cup.Name, club.Name, sexStr);
            }
            else res.Data.FileName = string.Format("Заявка на соревнование {0} от всех клубов ,{1}.xlsx", cup.Name, sexStr);

            //3. Формируем документ
            var createDoc = CreateExcelEntryPrint(path, queryEntrysList, cup, town, sex, club);
            if ( createDoc.Result.IsOk) 
            {
                res.Data.Content = createDoc.Data.Bytes;
            }
            else res.Result = createDoc.Result;

            return res;
        }
    }
}
