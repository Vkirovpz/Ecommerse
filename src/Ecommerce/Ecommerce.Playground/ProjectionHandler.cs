using Ecommerce;

public class ProjectionHandler
{
    private readonly IEnumerable<Type> handlersTypes;

    public ProjectionHandler(IEnumerable<Type> handlersTypes)
    {
        this.handlersTypes = handlersTypes;
    }

    public void Handle(IEvent e)
    {
        var interestedHandlers = handlersTypes.Where(x => x.GetInterfaces().Any(i => i.GenericTypeArguments.First() == e.GetType()));
        foreach (var type in interestedHandlers)
        {
            var handler = (dynamic)Activator.CreateInstance(type);
            handler.Handle((dynamic)e);
        }
    }
}
