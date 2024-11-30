namespace _00017102_WAD_CW_server.Repositories
{
    public interface IRepository<T>
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
