namespace EntertechFP.UI.Models.Entitities
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<EventDto> Events { get; set; }
    }
}
