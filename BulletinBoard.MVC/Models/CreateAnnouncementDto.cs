using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.MVC.Models
{
    public class CreateAnnouncementDto
    {
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
