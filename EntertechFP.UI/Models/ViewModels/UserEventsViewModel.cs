using EntertechFP.UI.Models.Entitities;

namespace EntertechFP.UI.Models.ViewModels
{
    public class UserEventsViewModel
    {
        public List<EventDto> Events { get; set; }
        public List<int> NextAttends { get; set; }
    }
}
