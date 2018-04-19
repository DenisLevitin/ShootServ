using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BO;

namespace DAL
{
    public abstract class BaseRepository<TModel, T> where TModel : ModelBase 
                                                    where T : class
    {
        protected abstract Func<T, int> GetPrimaryKeyValue { get; }

        protected abstract TModel ConvertToModel(T entity);

        protected abstract T ConvertToEntity(TModel model);

        public TModel Get(int id)
        {
            using (var db = DBContext.GetContext())
            {
                return ConvertToModel(db.Set<T>().FirstOrDefault(x => GetPrimaryKeyValue(x) == id ));
            }
        }

        public List<TModel> GetAll()
        {
            using (var db = DBContext.GetContext())
            {
                return db.Set<T>().ToList().Select(ConvertToModel).ToList();
            }
        }

        /// TODO: GetFiltered

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Create(TModel model)
        {
            using (var db = DBContext.GetContext())
            {
                var entity = ConvertToEntity(model);
                db.Set<T>().Add(entity);
                db.SaveChanges();
                return GetPrimaryKeyValue(entity);
            }
        }

        public void Delete(int id)
        {
            using (var db = DBContext.GetContext())
            {
                var entity = db.Set<T>().FirstOrDefault(x => GetPrimaryKeyValue(x) == id);
                if (entity != null)
                {
                    db.Set<T>().Remove(entity);
                    db.SaveChanges();
                }
                else
                {
                    // logger.warning
                }
            }
        }

        public void Update(TModel model, int id)
        {
            using (var db = DBContext.GetContext())
            {
                model.Id = id;
                var updating = ConvertToEntity(model);
                db.Set<T>().Attach(updating);
                db.Entry(updating).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
