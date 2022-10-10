namespace EntertechFP.UI.Models.Entitities
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public ICollection<EventDto> Events { get; set; }
        public ICollection<NotificationDto> Notifications { get; set; }
    }
}
