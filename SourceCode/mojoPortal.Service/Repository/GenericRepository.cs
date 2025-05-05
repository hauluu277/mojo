using mojoPortal.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mojoportal.Service.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private HinetContext context { get; set; }
        public HinetContext Context
        {
            get { return context; }
            set { context = value; }
        }
        protected DbSet<T> dbSet;
        public GenericRepository(HinetContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        public void Delete(object id)
        {
            T entity = dbSet.Find(id);
            if (context.Entry(entity).State == EntityState.Deleted)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            dbSet.RemoveRange(items);
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            var items = dbSet.Where(filter);
            dbSet.RemoveRange(items);
        }

        public void ExecuteSQL(string query)
        {
            ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreCommand(query);
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter);
        }

        public T Find(object id)
        {
            return dbSet.Find(id);
        }

        public T FindIfNull(object id)
        {
            T result = dbSet.Find(id) ?? (T)Activator.CreateInstance(typeof(T));
            return result;
        }

        [DbFunction("Entities", "NonUnicodeConvert")]
        public static string NonUnicodeConvert(string input)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }


        public bool CheckExisted(Dictionary<string, object> keyValueProperty, Dictionary<string, object> keyValueIdentity)
        {
            bool result = true;
            if (keyValueProperty == null || keyValueProperty.Count == 0)
            {
                return false;
            }
            else
            {
                Type objType = typeof(T);
                var propertyName = keyValueProperty.Keys.ElementAt(0);
                var propertyValue = keyValueProperty.Values.ElementAt(0);

                if (keyValueIdentity == null || keyValueIdentity.Count == 0)
                {
                    result = this.dbSet.AsNoTracking().ToList()
                        .Where(x => objType.GetProperty(propertyName).GetValue(x) != null)
                        .Where(x => objType.GetProperty(propertyName).GetValue(x).Equals(propertyValue))
                        .Count() > 0;
                }
                else
                {

                    var identityPropertyName = keyValueIdentity.Keys.ElementAt(0);
                    var identityPropertyValue = keyValueIdentity.Values.ElementAt(0);

                    result = this.dbSet.AsNoTracking().ToList()
                        .Where(x => objType.GetProperty(propertyName).GetValue(x) != null)

                        .Where(x => objType.GetProperty(identityPropertyName)
                        .GetValue(x).Equals(identityPropertyValue) == false)

                        .Where(x => objType.GetProperty(propertyName)
                        .GetValue(x).Equals(propertyValue))
                        .Count() > 0;
                }
            }
            return result;
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return dbSet.AsQueryable<T>();
        }

        public IEnumerable<T> GetAllBySqlQuery()
        {
            string tableName = typeof(T).Name;
            string command = $"select * from {tableName}";
            var result = context.Database.SqlQuery<T>(command).ToList();
            return result;
        }

        public IEnumerable<TEntity> GetAllBySqlQuery<TEntity>()
        {
            string tableName = typeof(TEntity).Name;
            string command = $"select * from {tableName}";
            var result = context.Database.SqlQuery<TEntity>(command).ToList();
            return result;
        }

        public IEnumerable<T> GetAllAsEnumerable()
        {
            return dbSet.ToList();
        }

        public void Insert(T item)
        {
            dbSet.Add(item);
        }

        public void InsertRange(IEnumerable<T> items)
        {
            dbSet.AddRange(items);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T item)
        {
            dbSet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
