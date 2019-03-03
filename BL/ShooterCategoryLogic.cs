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
        private readonly ShooterCategoryRepository _dalShooterCategory;

        private static IReadOnlyCollection<ShooterCategoryParams> _shooterCategories;

        public ShooterCategoryLogic()
        {
            _dalShooterCategory = new ShooterCategoryRepository();
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
        public IReadOnlyCollection<ShooterCategoryParams> GetAll()
        {
            if (_shooterCategories == null)
            {
                _shooterCategories = _dalShooterCategory.GetAll().OrderBy(x => x.OrderSort).ToList();
            }

            return _shooterCategories;
        }
    }
}
