namespace EntertechFP.UI.Models.Entitities
{
    public class CategoryDto
    {

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<EventDto> Events { get; set; }
    }
}
