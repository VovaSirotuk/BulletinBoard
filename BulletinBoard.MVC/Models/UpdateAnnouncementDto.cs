using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.MVC.Models
{
    public class UpdateAnnouncementDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string SubCategory { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
