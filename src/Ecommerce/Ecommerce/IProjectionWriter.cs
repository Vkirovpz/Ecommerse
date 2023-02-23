namespace Ecommerce
{
    public interface IProjectionWriter
    {
        void Handle(IEvent e);
    }
}