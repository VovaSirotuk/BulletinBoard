using BulletinBoard.Data.AnnouncementRep;
using BulletinBoard.Models;
using BulletinBoard.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementRepository _repository;

        public AnnouncementController(IAnnouncementRepository repository)  {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAll()
        {
            var annoucement = await _repository.GetAllAsync();
            return Ok(annoucement);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetById(int id)
        {
            var announcement = await _repository.GetByIdAsync(id);
            if (announcement == null)
                return NotFound();

            return Ok(announcement);
        }

        // POST: api/announcement
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUpdateAnnouncementDto announcementDTO)
        {
            var announcement = new Announcement
            {
                Title = announcementDTO.Title,
                Description = announcementDTO.Description,
                CreatedDate = DateTime.UtcNow,
                Status = announcementDTO.Status,
                Category = announcementDTO.Category,
                SubCategory = announcementDTO.SubCategory
            };
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.CreateAsync(announcement);
            return Ok();
        }

        // PUT: api/announcement/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateUpdateAnnouncementDto announcementDTO)
        {
            var announcement = new Announcement
            {
                Id = id,
                Title = announcementDTO.Title,
                Description = announcementDTO.Description,
                CreatedDate = DateTime.UtcNow,
                Status = announcementDTO.Status,
                Category = announcementDTO.Category,
                SubCategory = announcementDTO.SubCategory
            };
            if (id != announcement.Id)
                return BadRequest("ID in URL does not match ID in body");

            await _repository.UpdateAsync(announcement);
            return Ok();
        }

        // DELETE: api/announcement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}
