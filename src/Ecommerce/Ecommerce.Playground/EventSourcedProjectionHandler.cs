using Ecommerce;
using Ecommerce.EntityFramework;
using System.Text.Json;
using System.Text;
using Ecommerce.Projections;
using System.Data;

public class EventSourcedProjectionHandler : IProjectionHandler
{
    private readonly IEnumerable<Type> handlersTypes;
    private readonly EcommerceEventsDbContext eventsDbContext;

    public EventSourcedProjectionHandler(IEnumerable<Type> handlersTypes, EcommerceEventsDbContext eventsDbContext)
    {
        this.handlersTypes = handlersTypes.Where(x => x.GetInterfaces().Contains(typeof(IHaveProjectionId)));
        this.eventsDbContext = eventsDbContext;
    }

    public void Handle(IEvent e)
    {
        //var handlers = handlersTypes.Where(x => x.IsAssignableTo(typeof(IEventHandler<>).GetGenericTypeDefinition()));
        var records = new List<EventRecord>();
        foreach (var handlerType in handlersTypes)
        {
            var handler = handlerType.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GenericTypeArguments.First() == e.GetType());
            if (handler is null)
                continue;

            var projection = (IHaveProjectionId)Activator.CreateInstance(handlerType);
            var record = new EventRecord
            {
                EventId = projection.GetId(e),
                EventType = e.GetType().AssemblyQualifiedName,
                EventData = ToByteArray(e),
                Origin = handlerType.AssemblyQualifiedName
            };

            records.Add(record);
        }

        eventsDbContext.Events.AddRange(records);
        eventsDbContext.SaveChanges();

        //if (events.Contains(e.GetType()) == false)
        //    return;
        //var interestedHandlers = handlersTypes.Where(x => x.GetInterfaces().Any(i => i.GetType() == typeof(IEventHandler<>) && i.GenericTypeArguments.First() == e.GetType()));
        //foreach (var type in interestedHandlers)
        //{
        //    var projection = (IHaveProjectionId)Activator.CreateInstance(type);

        //    dbContext.Set<EventRecord>(type.Name).Add(new EventRecord
        //    {
        //        Id = projection.GetId(e),
        //        Type = e.GetType().FullName,
        //        Data = ToByteArray(e)
        //    });
        //}

        //dbContext.SaveChanges();
    }

    private static byte[] ToByteArray<T>(T obj)
    {
        if (obj == null)
            return null;

        var json = JsonSerializer.Serialize(obj, obj.GetType());
        var bytes = Encoding.UTF8.GetBytes(json);
        return bytes;
    }
}
