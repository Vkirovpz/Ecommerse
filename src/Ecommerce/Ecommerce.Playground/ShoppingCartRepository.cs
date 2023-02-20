using Ecommerce.Cart;
using System.Text.Json;
using System.Text;

namespace Ecommerce.Playground
{
    public class ShoppingCartRepository : IAggregateRootRepository<ShoppingCart>
    {
        private static readonly List<Tuple<string, Type, byte[]>> data = new();
        public Task<ShoppingCart> LoadAsync(string id)
        {
            var found = data.Where(x => x.Item1 == id);
            if (found.Any() == false)
                return Task.FromResult<ShoppingCart>(null);

            var state = new ShoppingCartState();
            foreach (var record in found)
            {
                var bytes = record.Item3;
                var obj = FromByteArray(record.Item2, bytes);
                state.Restore((dynamic)obj);
            }

            var cart = new ShoppingCart(state);
            return Task.FromResult(cart);
        }

        public Task SaveAsync(ShoppingCart aggregateRoot)
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
}
