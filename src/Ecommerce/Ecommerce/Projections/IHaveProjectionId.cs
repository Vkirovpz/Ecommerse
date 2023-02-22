namespace Ecommerce.Projections
{
    public interface IHaveProjectionId
    {
        string GetId(IEvent e);
    }
}
