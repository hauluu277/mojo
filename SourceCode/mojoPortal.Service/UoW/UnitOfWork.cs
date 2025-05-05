using mojoportal.Service.Repository;
using mojoPortal.Model;
using System;
using System.Collections.Generic;

namespace mojoportal.Service.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private HinetContext context { get; set; }
        public HinetContext Context
        {
            get { return context; }
            set { context = value; }
        }
        private bool disposed = false;
        private Dictionary<string, object> repositories = new Dictionary<string, object>();
        public UnitOfWork()
        {
            context = new HinetContext();
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public GenericRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
                repositories.Add(type, repositoryInstance);
            }
            return (GenericRepository<T>)repositories[type];
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
    }
}
