using Ecommerce.Cart;
using System.Text.Json;
using System.Text;
using Ecommerce.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Playground
{
    public class ShoppingCartRepository : IAggregateRepository<ShoppingCart>
    {
        private readonly IProjectionHandler projectionHandler;
        private readonly EcommerceEventsDbContext dbContext;

        public ShoppingCartRepository(IProjectionHandler projectionHandler, EcommerceEventsDbContext dbContext)
        {
            this.projectionHandler = projectionHandler;
            this.dbContext = dbContext;
        }

        public Task<ShoppingCart> LoadAsync(string id)
        {
            var found = dbContext.Events.Where(x => x.EventId == id);
            if (found.Any() == false)
                return Task.FromResult<ShoppingCart>(null);

            var cartId = new ShoppingCartId(id);
            var state = new ShoppingCartState(cartId);
            foreach (var record in found)
            {
                var bytes = record.EventData;
                var type = Type.GetType(record.EventType);
                var obj = FromByteArray(type, bytes);
                state.Restore((dynamic)obj);
            }

            var cart = new ShoppingCart(state);
            return Task.FromResult(cart);
        }

        public async Task SaveAsync(ShoppingCart entity)
        {
            var records = new List<EventRecord>();
            foreach (var e in entity.State.UnsavedEvents)
            {
                var bytes = ToByteArray(e);
                records.Add(new EventRecord
                {
                    EventId = entity.State.Id.Value,
                    EventType = e.GetType().AssemblyQualifiedName,
                    EventData = bytes,
                    Origin = entity.GetType().AssemblyQualifiedName
                });
            }

            await dbContext.Events.AddRangeAsync(records);
            await dbContext.SaveChangesAsync();

            foreach (var e in entity.State.UnsavedEvents)
            {
                projectionHandler.Handle(e);
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
