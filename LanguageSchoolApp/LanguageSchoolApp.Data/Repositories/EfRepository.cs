using LanguageSchoolApp.Data.Model.Contracts;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace LanguageSchoolApp.Data.Repositories
{
    public class EfRepository<T> : IEfRepository<T>
         where T : class, IDeletable
    {
        private readonly MsSqlDbContext context;

        public EfRepository(MsSqlDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Returns all existing courses except those that have been deleted, i.e. those with flad isDeleted = true are not returned.
        /// </summary>
        public IQueryable<T> AllNotDeleted
        {
            get
            {
                return this.context.Set<T>().Where(x => !x.IsDeleted);
            }
        }

        /// <summary>
        /// Returns all existing courses together with those that have been deleted, i.e. those with flad isDeleted = true are returned as well.
        /// </summary>
        public IQueryable<T> All
        {
            get
            {
                return this.context.Set<T>();
            }
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.context.Set<T>().Add(entity);
            }
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            var entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
