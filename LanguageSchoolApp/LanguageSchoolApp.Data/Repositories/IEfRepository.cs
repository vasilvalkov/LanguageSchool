using System.Linq;
using LanguageSchoolApp.Data.Model.Contracts;

namespace LanguageSchoolApp.Data.Repositories
{
    public interface IEfRepository<T>
        where T : class, IDeletable
    {
        IQueryable<T> All { get; }

        IQueryable<T> AllAndDeleted { get; }

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}