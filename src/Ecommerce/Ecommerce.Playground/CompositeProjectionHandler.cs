//using Ecommerce;

//public class CompositeProjectionHandler : IProjectionHandler
//{
//    private readonly EventSourcedProjectionHandler eventSourcedProjectionHandler;
//    private readonly ProjectionHandler projectionHandler;

//    public CompositeProjectionHandler(EventSourcedProjectionHandler eventSourcedProjectionHandler, ProjectionHandler projectionHandler)
//    {
//        this.eventSourcedProjectionHandler = eventSourcedProjectionHandler;
//        this.projectionHandler = projectionHandler;
//    }

//    public void Handle(IEvent e)
//    {
//        projectionHandler.Handle(e);
//        eventSourcedProjectionHandler.Handle(e);
//    }
//}
