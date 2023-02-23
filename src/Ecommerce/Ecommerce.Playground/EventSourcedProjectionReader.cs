using Ecommerce.EntityFramework;
using Ecommerce.Projections;
using System.Reflection;
using System.Text.Json;
using System.Text;

namespace Ecommerce.Playground
{
    public class EventSourcedProjectionReader
    {
        private readonly EcommerceEventsDbContext context;

        public EventSourcedProjectionReader(EcommerceEventsDbContext context)
        {
            this.context = context;
        }

        public T Load<T>(string id) where T : IHaveProjectionId, new()
        {
            var projType = typeof(T);
            var events = context.ProjectionsEvents.Where(x => x.Origin == projType.AssemblyQualifiedName && x.EventId == id);
            var proj = new T();
            foreach (var e in events)
            {
                var eventType = Type.GetType(e.EventType);
                var method = projType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.Name == nameof(IEventHandler<IEvent>.Handle))
                    .FirstOrDefault(x => x.GetParameters().First().ParameterType == eventType);

                var @event = FromByteArray(eventType, e.EventData);
                method?.Invoke(proj, new object[] { @event });
            }

            return proj;
        }

        private static object FromByteArray(Type type, byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            var obj = JsonSerializer.Deserialize(json, type);
            return obj;
        }
    }
}