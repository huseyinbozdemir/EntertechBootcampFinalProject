namespace EntertechFP.UI.Models.Entitities
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool IsSeen { get; set; }

        public UserDto User { get; set; }
    }
}
