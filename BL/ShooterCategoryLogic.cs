using BO;
using DAL;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    /// <summary>
    /// Логика работы со справочником разрядов разрдами
    /// </summary>
    public class ShooterCategoryLogic
    {
        private readonly EFShooterCategory _dalShooterCategory;

        public ShooterCategoryLogic()
        {
            _dalShooterCategory = new EFShooterCategory();
        }

        /// <summary>
        /// Получить разряд стрелка по идентификатору
        /// </summary>
        /// <param name="id">ид. разряда</param>
        /// <returns></returns>
        public ShooterCategoryParams Get(int id)
        {
            return _dalShooterCategory.GetById(id);
        }

        /// <summary>
        /// Получить список разрядов
        /// </summary>
        /// <returns></returns>
        public List<ShooterCategoryParams> GetAll()
        {
            return _dalShooterCategory.GetAll().OrderBy(x=>x.OrderSort).ToList();
        }
    }
}
