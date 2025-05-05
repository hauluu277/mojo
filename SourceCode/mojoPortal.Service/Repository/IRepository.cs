using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mojoportal.Service.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAllBySqlQuery();
        IEnumerable<TEntity> GetAllBySqlQuery<TEntity>();
        IEnumerable<T> GetAllAsEnumerable();
        IQueryable<T> GetAllAsQueryable();

        IQueryable<T> Filter(Expression<Func<T, bool>> filter);

        void Insert(T item);

        void InsertRange(IEnumerable<T> items);

        void Update(T item);

        void Delete(object id);

        void DeleteRange(IEnumerable<T> items);

        T Find(object id);

        T FindIfNull(object id);

        void Save();

        void ExecuteSQL(string query);

        bool CheckExisted(Dictionary<string, object> keyValueProperty, Dictionary<string, object> keyValueIdentity = null);
    }
}
