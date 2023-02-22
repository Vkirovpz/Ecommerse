using Ecommerce.Customer.Queries;
using Ecommerce.EntityFramework;

namespace Ecommerce.Customer
{
    public class CustomerQueryService :
        IQueryHandler<GetCustomerById, CustomerDto>
    {
        private readonly EcommerceDbContext _dbContext;
        public CustomerQueryService(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<QueryResult<CustomerDto>> HandleAsync(GetCustomerById query)
        {
            var dataCustomer = _dbContext.Customers.FirstOrDefault(c => c.Id == query.Id);
            if (dataCustomer is not null)
            {
                var viewCustomer = new CustomerDto(dataCustomer.FirstName, dataCustomer.LastName);
                return Task.FromResult(QueryResult<CustomerDto>.From(viewCustomer));
            }

            return Task.FromResult(QueryResult<CustomerDto>.Empty);
        }
    }

    public record CustomerDto(string FirstName, string LastName)
    {
        public string FullName => $"{FirstName} {LastName}";
    }

    public class QueryResult<T> where T : class
    {
        QueryResult() { }

        public QueryResult(T result)
        {
            Result = result ?? throw new ArgumentNullException(nameof(result));
        }

        public readonly static QueryResult<T> Empty = new();

        public T Result { get; }

        public bool Success => Result != null;

        public static QueryResult<T> From(T result) => new(result);
    }
}
