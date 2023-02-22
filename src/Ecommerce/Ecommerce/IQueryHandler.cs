using Ecommerce.Customer;

namespace Ecommerce
{
    public interface IQueryHandler<T, U> 
        where T : IQuery
        where U : class
    {
        Task<QueryResult<U>> HandleAsync(T query);
    }
}
