namespace Ecommerce
{
    public interface IAggregateRootRepository<T>
    {
        Task<T> LoadAsync(string id);
        Task SaveAsync(T aggregateRoot);
    }
}