namespace EntertechFP.UI.Models.Entitities
{
    public class EntegratorEventDto
    {
        public int EntegratorId { get; set; }
        public int EventId { get; set; }

        public EntegratorDto Entegrator { get; set; }
        public EventDto Event { get; set; }
    }
}
