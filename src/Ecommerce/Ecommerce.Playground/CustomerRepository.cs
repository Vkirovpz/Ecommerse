using Ecommerce;
using Ecommerce.Cart;
using Ecommerce.Customer;
using System.Text;
using System.Text.Json;

public class CustomerRepository : IAggregateRootRepository<CustomerAggregate>
{
    private static readonly List<Tuple<string, Type, byte[]>> data = new();

    private readonly IAggregateRootRepository<ShoppingCart> cartRepository;
    private readonly ProjectionHandler projectionHandler;

    public CustomerRepository(IAggregateRootRepository<ShoppingCart> cartRepository, ProjectionHandler projectionHandler)
    {
        this.cartRepository = cartRepository;
        this.projectionHandler = projectionHandler;
    }

    public Task<CustomerAggregate> LoadAsync(string id)
    {
        var found = data.Where(x => x.Item1 == id);
        if (found.Any() == false)
            return Task.FromResult<CustomerAggregate>(null);

        var state = new CustomerState();
        foreach (var record  in found)
        {
            var bytes = record.Item3;
            var obj = FromByteArray(record.Item2, bytes);
            state.Restore((dynamic)obj);
        }

        if (state.Cart is not null)
        {
            var cartId = state.Cart.State.Id;
            var cart = cartRepository.LoadAsync(state.Cart.State.Id.Value);
            state.SetCart(cart.Result);
        }

        var customer = new CustomerAggregate(state);

        return Task.FromResult(customer);
    }

    public Task SaveAsync(CustomerAggregate aggregateRoot)
    {
        foreach (var e in aggregateRoot.State.UnsavedEvents)
        {
            var bytes = ToByteArray(e);
            data.Add(new Tuple<string, Type, byte[]>(aggregateRoot.State.Id.Value, e.GetType(), bytes));
        }

        var cart = aggregateRoot.State.Cart;

        if (cart is not null)
        {
            cartRepository.SaveAsync(cart);
        }

        foreach (var e in aggregateRoot.State.UnsavedEvents)
        {
            projectionHandler.Handle(e);
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