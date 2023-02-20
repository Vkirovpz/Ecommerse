using Ecommerce;
using Ecommerce.Cart;
using Ecommerce.Customer;
using System.Text;
using System.Text.Json;

public class CustomerRepository : IAggregateRootRepository<CustomerAggregate>
{
    private static readonly List<Tuple<string, Type, byte[]>> data = new();

    private readonly IAggregateRootRepository<ShoppingCart> cartRepository;

    public CustomerRepository(IAggregateRootRepository<ShoppingCart> cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public Task<CustomerAggregate> LoadAsync(string id)
    {
        var found = data.Where(x => x.Item1 == id);
        if (found.Any() == false)
            return Task.FromResult<CustomerAggregate>(null);

        var state = new CustomerState();
        foreach (var record in found)
        {
            var bytes = record.Item3;
            var obj = FromByteArray(record.Item2, bytes);
            state.Restore((dynamic)obj);
        }

        var customer = new CustomerAggregate(state);
        //var cart = cartRepository.LoadAsync(customer.State.Cart.State.Id);

        return Task.FromResult(customer);
    }

    public Task SaveAsync(CustomerAggregate aggregateRoot)
    {

        foreach (var e in aggregateRoot.State.UnsavedEvents)
        {
            var bytes = ToByteArray(e);
            data.Add(new Tuple<string, Type, byte[]>(aggregateRoot.State.Id, e.GetType(), bytes));
        }
        return Task.CompletedTask;
    }

    private static byte[] ToByteArray<T>(T obj)
    {
        if (obj == null)
            return null;

        var json = JsonSerializer.Serialize(obj, obj.GetType());
        var bytes = Encoding.UTF8.GetBytes(json);
        return bytes;
    }

    private static object FromByteArray(Type type, byte[] data)
    {
        var json = Encoding.UTF8.GetString(data);
        var obj = JsonSerializer.Deserialize(json, type);
        return obj;
    }
}
