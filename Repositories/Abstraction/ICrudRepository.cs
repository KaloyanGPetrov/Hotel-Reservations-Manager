using Hotel_Reservations_Manager.Data.Entities;

namespace Hotel_Reservations_Manager.Repositories.Abstraction
{
    public interface ICrudRepository<T>
       where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task<ICollection<T>> GetAllAsync();
        ICollection<T> GetByFilter(Func<T, bool> predicate);
        Task<T?> GetByIdAsync(int id);
    }
}
