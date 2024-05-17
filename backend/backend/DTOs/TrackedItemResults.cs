using Contexts.Models;

namespace backend.DTOs
{
    public class TrackedItemResults
    {
        public List<TrackedItemValue> Entries { get; set; }
        public TrackedItemValue MaxGemValue { get; set; }
        public TrackedItemValue MinGemValue { get; set; }
        public TrackedItemValue MaxTreasureValue { get; set; }
        public TrackedItemValue MinTreasureValue { get; set; }
    }
}
