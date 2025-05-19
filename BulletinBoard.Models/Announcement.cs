namespace BulletinBoard.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public String Status { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
