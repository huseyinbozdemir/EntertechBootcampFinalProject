using System.Text.Json.Serialization;

namespace EntertechFP.UI.Models.Entitities
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EventDto> Events { get; set; }
    }
}
