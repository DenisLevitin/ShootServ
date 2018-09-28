using System.Collections.Generic;
using BL;
using BO;

namespace ShootServ.Areas.ShootingRange.Models
{
    public static class ShootingRangeModelLogic
    {
        /// <summary>
        /// Получить список тиров по региону
        /// </summary>
        /// <param name="regionId">ид. региона</param>
        /// <returns></returns>
        public static List<ShootingRangeModelParams> GetAllByRegion(int? regionId)
        {
            var res = new List<ShootingRangeModelParams>();
            
            var blShootingRange = new ShootingRangeLogic();
            var list = regionId.HasValue ? blShootingRange.GetByRegion(regionId.Value) : new List<ShootingRangeParams>();

            foreach (var item in list)
            {
                res.Add(new ShootingRangeModelParams
                {
                    Address = item.Address,
                    Id = item.Id,
                    Info = item.Info,
                    Name = item.Name,
                    Phone = item.Phone,
                    RegionId = item.IdRegion,
                    Town = item.Town
                });
            }

            return res;
        }

        /// <summary>
        /// Добавить тир
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        public static ResultInfo Add(ShootingRangeModelParams model, int userId)
        {
            var blShootingRange = new ShootingRangeLogic();

            var shootingRangeParams = new ShootingRangeParams
            {
                Address = model.Address,
                Id = 0,
                IdRegion = model.RegionId,
                Info = model.Info,
                Name = model.Name,
                Phone = model.Phone,
                Town = model.Town,
                IdUser = userId
            };

            return blShootingRange.Add(shootingRangeParams, userId);
        }

        public static ResultInfo Delete(int idShootingRange, int idUser)
        {
            return new ShootingRangeLogic().Delete(idShootingRange, idUser);
        }
    }
}