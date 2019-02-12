using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using BO;

namespace DAL
{
    public abstract class BaseRepository<TModel, T> where TModel : ModelBase 
                                                    where T : class
    {
        protected abstract Func<T, int> GetPrimaryKeyValue { get; }

        /// TODO: Рассмотреть вариант как virtual, AutoMapper
        
        /// <summary>
        ///  сконвертить в модель
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract TModel ConvertToModel(T entity);

        /// <summary>
        /// сконвертить в dbEntity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        public List<TModel> GetFiltered(Expression<Func<T, bool>> filter)
        {
            using (var db = DBContext.GetContext())
            {
                return db.Set<T>().Where(filter).Select(x => ConvertToModel(x)).ToList();
            }
        }

        public List<TModel> GetFiltered<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> order)
        {
            using (var db = DBContext.GetContext())
            {
                return db.Set<T>().Where(filter).OrderBy(order).Select(x => ConvertToModel(x)).ToList();
            }
        }

        public TModel GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            using (var db = DBContext.GetContext())
            {
                var entity = db.Set<T>().FirstOrDefault(filter);
                return entity != null ? ConvertToModel(entity) : null;
            }
        }
        
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

        public void AddRange(IReadOnlyCollection<TModel> collection)
        {
            using (var db = DBContext.GetContext())
            {
                var set = db.Set<T>();
                foreach (var item in collection)
                {
                    var entity = ConvertToEntity(item);
                    set.Add(entity);
                }
                
                db.SaveChanges();
            }
        }

        public void DeleteRange(IReadOnlyCollection<int> ids)
        {
            /// TODO: Тут надо EF обновлять скорее всего
            throw new NotImplementedException();
        }
    }
}
