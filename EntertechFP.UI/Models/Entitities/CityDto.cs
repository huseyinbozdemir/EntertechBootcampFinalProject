namespace EntertechFP.UI.Models.Entitities
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public List<EventDto> Events { get; set; }
    }
}
