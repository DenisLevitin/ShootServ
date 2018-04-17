using BO;
using DAL;
using System.Collections.Generic;

namespace BL
{
    /// <summary>
    /// БЛ для типа соревнования
    /// </summary>
    public class CupTypeLogic
    {
        private readonly CupTypeRepository _dalCupType;

        public CupTypeLogic()
        {
            _dalCupType = new CupTypeRepository();
        }

        public CupTypeParams Get(int id)
        {
            return _dalCupType.Get(id);
        }

        public List<CupTypeParams> GetAll()
        {
            return _dalCupType.GetAll();
        }
    }
}
