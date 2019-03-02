using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// Класс для работы с таблицей Categories
    /// </summary>
    public class ShooterCategoryRepository
    {
        private ShooterCategoryParams Convert(ShooterCategory dalShooterCategory)
        {
            return new ShooterCategoryParams
            {
                Id = dalShooterCategory.Id,
                Name = dalShooterCategory.Name,
                OrderSort = dalShooterCategory.OrderSort,
                PictureUrl = dalShooterCategory.PictureUrl
            };
        }

        /// <summary>
        /// Получить разряд по ид.
        /// </summary>
        /// <param name="catId">ид. разряда</param>
        /// <returns></returns>
        public ShooterCategoryParams GetById(int catId)
        {
            ShooterCategoryParams shooterCategory;
            using (var db = DBContext.GetContext())
            {
                shooterCategory = Convert(db.ShooterCategory.First(x => x.Id == catId));
            }

            return shooterCategory;
        }

        /// <summary>
        /// Получить полный список разрядов
        /// </summary>
        /// <returns></returns>
        public List<ShooterCategoryParams> GetAll()
        {
            var res = new List<ShooterCategoryParams>();
            using (var db = DBContext.GetContext())
            {
                res = db.ShooterCategory.ToList().ConvertAll(Convert);
            }

            return res;
        }
    }
}
