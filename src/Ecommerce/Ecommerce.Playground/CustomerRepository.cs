using Ecommerce.Cart;
using Ecommerce.Customer;
using Ecommerce.EntityFramework;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Playground
{
    public class CustomerRepository : IAggregateRepository<CustomerAggregate>
    {
        private readonly IAggregateRepository<ShoppingCart> cartRepository;
        private readonly IProjectionWriter projectionWriter;
        private readonly EcommerceEventsDbContext dbContext;

        public CustomerRepository(IAggregateRepository<ShoppingCart> cartRepository, IProjectionWriter projectionHandler, EcommerceEventsDbContext dbContext)
        {
            this.cartRepository = cartRepository;
            this.projectionWriter = projectionHandler;
            this.dbContext = dbContext;
        }

        public Task<CustomerAggregate> LoadAsync(string id)
        {
            var found = dbContext.Events.Where(x => x.EventId == id);
            if (found.Any() == false)
                return Task.FromResult<CustomerAggregate>(null);

            var state = new CustomerState();
            foreach (var record in found)
              {
                var bytes = record.EventData;
                var type = Type.GetType(record.EventType);
                var obj = FromByteArray(type, bytes);
                state.Restore((dynamic)obj);

                if (state.Cart is not null)
                {
                    var cart = cartRepository.LoadAsync(state.Cart.State.Id.Value);
                    state.SetCart(cart.Result);
                }
            }

            var customer = new CustomerAggregate(state);

            return Task.FromResult(customer);
        }

        public async Task SaveAsync(CustomerAggregate aggregateRoot)
        {
            var records = new List<EventRecord>();
            foreach (var e in aggregateRoot.State.UnsavedEvents)
            {
                var bytes = ToByteArray(e);
                records.Add(new EventRecord
                {
                    EventId = aggregateRoot.State.Id.Value,
                    EventType = e.GetType().AssemblyQualifiedName,
                    EventData = bytes
                });
            }

            var cart = aggregateRoot.State.Cart;

            await dbContext.Events.AddRangeAsync(records);
            await dbContext.SaveChangesAsync();

            if (cart is not null)
            {
                await cartRepository.SaveAsync(cart);
            }

            foreach (var e in aggregateRoot.State.UnsavedEvents)
            {
                projectionWriter.Handle(e);
            }


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
}