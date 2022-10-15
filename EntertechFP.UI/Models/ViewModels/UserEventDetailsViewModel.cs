using EntertechFP.UI.Models.Entitities;

namespace EntertechFP.UI.Models.ViewModels
{
    public class UserEventDetailsViewModel
    {
        public EventDto Event { get; set; }
        public bool IsAttended { get; set; }
        public List<EntegratorDto> Entegrators { get; set; }
    }
}
