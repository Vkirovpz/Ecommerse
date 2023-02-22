namespace Ecommerce.EntityFramework
{
    public class EventRecord
    {
        public int Id { get; set; }

        public string EventId { get; init; }

        public string EventType { get; init; }

        public byte[] EventData { get; init; }

        public string Origin { get; init; }
    }
}