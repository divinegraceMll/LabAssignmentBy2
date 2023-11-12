using Student.Web.Api.Models;

namespace Student.Web.Api.Data
{
    public interface IRepository<T>
    {
        void Add(T newT);
        void Update<K>(K id, T input);
        void Delete(T item);

        Task<bool> SaveAllChangesAsync();
    }
}