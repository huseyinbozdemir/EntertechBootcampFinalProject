using System.Text.Json.Serialization;

namespace EntertechFP.UI.Models.Entitities
{
    public class CategoryDto
    {

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EventDto> Events { get; set; }
    }
}
