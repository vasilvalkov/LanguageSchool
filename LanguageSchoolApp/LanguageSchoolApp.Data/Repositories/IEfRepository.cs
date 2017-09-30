using System.Linq;
using LanguageSchoolApp.Data.Model.Contracts;

namespace LanguageSchoolApp.Data.Repositories
{
    public interface IEfRepository<T>
        where T : class, IDeletable
    {
        IQueryable<T> AllNotDeleted { get; }

        IQueryable<T> All { get; }

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}