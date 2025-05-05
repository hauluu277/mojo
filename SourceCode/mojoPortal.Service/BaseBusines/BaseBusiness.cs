using mojoportal.Service.Repository;
using mojoportal.Service.UoW;
using mojoPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace mojoportal.Service.BaseBusines
{
    public class BaseBusiness<T> : IRepository<T>, IDisposable where T : class
    {
        public GenericRepository<T> repository;
        protected UnitOfWork unitOfWork;
        private bool disposed;
        public HinetContext context;

        protected BaseBusiness(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = this.unitOfWork.GetRepository<T>();
            this.context = this.unitOfWork.Context;
            //this.context.Configuration.AutoDetectChangesEnabled = false;
            //this.context.Configuration.LazyLoadingEnabled = false;
        }

        protected BaseBusiness()
        {
            this.unitOfWork = new UnitOfWork();
            this.repository = this.unitOfWork.GetRepository<T>();
            this.context = this.unitOfWork.Context;
            //this.context.Configuration.AutoDetectChangesEnabled = false;
            //this.context.Configuration.LazyLoadingEnabled = false;
        }
        public virtual void Save()
        {
            repository.Save();
        }

        public T Find(object id)
        {
            return repository.Find(id);
        }

        public T FindIfNull(object id)
        {
            return repository.FindIfNull(id);
        }

        public void Save(T item)
        {
            try
            {
                var type = item.GetType();
                var property = type.GetProperty("ItemID");
                if (property == null)
                {
                    property = type.GetProperty("Id");
                }
                if (property == null)
                {
                    property = type.GetProperty("ID");
                }
                if (property == null)
                {
                    property = type.GetProperty("RowGuid");
                }
                if (property == null)
                {
                    property = type.GetProperty("ItemId");
                }
                if (property == null)
                {
                    property = type.GetProperty("SiteID");
                }
                if (property != null)
                {
                    var getvalue = property.GetValue(item).ToString();
                    if (getvalue == "0")
                    {
                        this.repository.Insert(item);
                    }
                    else
                    {
                        this.repository.Update(item);
                    }
                }
                else
                {
                    throw new Exception("Không tồn tại ID");
                }

                this.repository.Save();
            }
            catch (Exception ex)
            {
                //LogHelper.Error(string.Format("UserService.Save: {0}", ex.Message));
                throw new Exception(ex.Message);
            }
        }

        public void SaveTemporarily(T item)
        {
            try
            {
                var type = item.GetType();
                var property = type.GetProperty("ID");
                if (property != null)
                {
                    var getvalue = property.GetValue(item).ToString();
                    if (getvalue == "0")
                    {
                        this.repository.Insert(item);
                    }
                    else
                    {
                        this.repository.Update(item);
                    }
                }
                else
                {
                    throw new Exception("Không tồn tại ID");
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(string.Format("UserService.Save: {0}", ex.Message));
                throw new Exception(ex.Message);
            }
        }


        public IEnumerable<T> GetAllAsEnumerable()
        {
            return repository.GetAllAsEnumerable();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return repository.GetAllAsQueryable();
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> filter)
        {
            return repository.Filter(filter);
        }

        public void Insert(T item)
        {
            repository.Insert(item);
        }

        public void Update(T item)
        {
            repository.Update(item);
        }

        public void Delete(object id)
        {
            repository.Delete(id);
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            repository.Delete(filter);
        }


        public void DeleteRange(IEnumerable<T> items)
        {
            repository.DeleteRange(items);
        }

        public void ExecuteSQL(string query)
        {
            repository.ExecuteSQL(query);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void InsertRange(IEnumerable<T> items)
        {
            repository.InsertRange(items);
        }

        public bool CheckExisted(Dictionary<string, object> keyValueProperty, Dictionary<string, object> keyValueIdentity)
        {
            return repository.CheckExisted(keyValueProperty, keyValueIdentity);
        }

        public IEnumerable<T> GetAllBySqlQuery()
        {
            return repository.GetAllBySqlQuery();
        }

        public IEnumerable<TEntity> GetAllBySqlQuery<TEntity>()
        {
            return repository.GetAllBySqlQuery<TEntity>();
        }
    }
}
