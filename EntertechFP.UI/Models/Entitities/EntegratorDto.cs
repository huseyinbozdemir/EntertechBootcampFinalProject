using System.Text.Json.Serialization;

namespace EntertechFP.UI.Models.Entitities
{
    public class EntegratorDto
    {
        public int EntegratorId { get; set; }
        public string EntegratorName { get; set; }
        public string DomainName { get; set; }
        public string EmailAdress { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EntegratorEventDto> EntegratorEvents { get; set; }

    }
}
