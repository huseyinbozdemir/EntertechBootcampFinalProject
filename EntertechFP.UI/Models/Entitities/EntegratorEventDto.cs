using System.Text.Json.Serialization;

namespace EntertechFP.UI.Models.Entitities
{
    public class EntegratorEventDto
    {
        public int EntegrationId { get; set; }
        public int EntegratorId { get; set; }
        public int EventId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EntegratorDto Entegrator { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EventDto Event { get; set; }
    }
}
