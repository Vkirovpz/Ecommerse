namespace Ecommerce.Customer.Events
{
    public class CustomerLastNameChanged : IEvent
    {
        public CustomerLastNameChanged(string id, string oldLastName, string newLastName)
        {
            Id = id;
            OldLastName = oldLastName;
            NewLastName = newLastName;
        }

        public string Id { get; }
        public string OldLastName { get; }
        public string NewLastName { get; }
    }
}