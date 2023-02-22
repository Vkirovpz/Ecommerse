namespace Ecommerce
{
    public interface IAggregateRepository<T>
    {
        Task<T> LoadAsync(string id);
        Task SaveAsync(T aggregate);
    }
}