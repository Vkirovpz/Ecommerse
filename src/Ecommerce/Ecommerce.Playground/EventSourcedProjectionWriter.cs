using Ecommerce;
using Ecommerce.EntityFramework;
using System.Text.Json;
using System.Text;
using Ecommerce.Projections;
using System.Data;

public class EventSourcedProjectionWriter : IProjectionWriter
{
    private readonly IEnumerable<Type> handlersTypes;
    private readonly EcommerceEventsDbContext eventsDbContext;

    public EventSourcedProjectionWriter(IEnumerable<Type> handlersTypes, EcommerceEventsDbContext eventsDbContext)
    {
        this.handlersTypes = handlersTypes.Where(x => x.GetInterfaces().Contains(typeof(IHaveProjectionId)));
        this.eventsDbContext = eventsDbContext;
    }

    public void Handle(IEvent e)
    {
        var records = new List<ProjectionEventRecord>();
        foreach (var handlerType in handlersTypes)
        {
            var handler = handlerType.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GenericTypeArguments.First() == e.GetType());
            if (handler is null)
                continue;

            var projection = (IHaveProjectionId)Activator.CreateInstance(handlerType);
            var record = new ProjectionEventRecord
            {
                EventId = projection.GetId(e),
                EventType = e.GetType().AssemblyQualifiedName,
                EventData = ToByteArray(e),
                Origin = handlerType.AssemblyQualifiedName
            };

            records.Add(record);
        }

        eventsDbContext.ProjectionsEvents.AddRange(records);
        eventsDbContext.SaveChanges();
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
