using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Models.DTOs
{
    public class AnnouncementDto
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
