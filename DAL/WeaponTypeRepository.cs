using BO;
using System;

namespace DAL
{
    public class WeaponTypeRepository : BaseRepository<WeaponTypeParams, WeaponTypes>
    {
        protected override Func<WeaponTypes, int> GetPrimaryKeyValue => 
            throw new NotImplementedException();

        protected override WeaponTypes ConvertToEntity(WeaponTypeParams model)
        {
            return new WeaponTypes
            {
                Id = model.Id,
                Name = model.Name,
                KeyChar = model.Keychar,
                PictureUrl = model.PictureUrl
            };
        }

        protected override WeaponTypeParams ConvertToModel(WeaponTypes entity)
        {
            return new WeaponTypeParams
            {
                Id = entity.Id,
                Name = entity.Name,
                Keychar = entity.KeyChar,
                PictureUrl = entity.PictureUrl
            };
        }
    }
}
