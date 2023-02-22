namespace Ecommerce
{
    public interface IProjectionHandler
    {
        void Handle(IEvent e);
    }
}