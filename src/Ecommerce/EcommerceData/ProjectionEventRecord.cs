namespace Ecommerce.EntityFramework
{
    public class ProjectionEventRecord
    {
        public int Id { get; set; }

        public string EventId { get; init; }

        public string EventType { get; init; }

        public byte[] EventData { get; init; }

        public string Origin { get; init; }
    }
}