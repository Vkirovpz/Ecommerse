//using Ecommerce;
//using Ecommerce.EntityFramework;
//using Ecommerce.Projections;

//public class ProjectionHandler : IProjectionHandler
//{
//    private readonly IEnumerable<Type> handlersTypes;
//    private readonly EcommerceDbContext dbContext;

//    public ProjectionHandler(IEnumerable<Type> handlersTypes, EcommerceDbContext dbContext)
//    {
//        this.handlersTypes = handlersTypes.Where(x => x.GetInterfaces().Contains(typeof(IHaveProjectionId)) == false);
//        this.dbContext = dbContext;
//    }

//    public void Handle(IEvent e)
//    {
//        var interestedHandlers = handlersTypes.Where(x => x.GetInterfaces().Any(i => i.GenericTypeArguments.FirstOrDefault() == e.GetType()));
//        foreach (var type in interestedHandlers)
//        {
//            var handler = (dynamic)Activator.CreateInstance(type, dbContext);
//            handler.Handle((dynamic)e);
//        }
//    }
//}
