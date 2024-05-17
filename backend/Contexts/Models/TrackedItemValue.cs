namespace Contexts.Models
{
    public class TrackedItemValue
    {
        public int TrackedItemValueId { get; set; }
        
        public int TrackedItemId { get; set; }

        public DateTime Date { get; set; }

        public int? GemValue { get; set; }

        public int? TreasureValue { get; set; }
    }
}
