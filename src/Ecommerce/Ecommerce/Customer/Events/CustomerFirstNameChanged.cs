namespace Ecommerce.Customer.Events
{
    public class CustomerFirstNameChanged : IEvent
    {
        public CustomerFirstNameChanged(string id, string oldFirstName, string newFirstName)
        {
            Id = id;
            OldFirstName = oldFirstName;
            NewFirstName = newFirstName;
        }

        public string Id { get; }
        public string OldFirstName { get; }
        public string NewFirstName { get; }
    }
}