using BulletinBoard.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Data.AnnouncementRep
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDbContext _context;

        public AnnouncementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            return await _context.Announcements
                .FromSqlRaw<Announcement>("EXEC spGetAllAnnouncements")
                .ToListAsync();
        }

        public async Task<Announcement> GetByIdAsync(int id)
        {
            var result = await _context.Database
                                        .SqlQueryRaw<Announcement>($"EXEC spGetAnnouncementById @Id = {id}")
                                        .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task CreateAsync(Announcement announcement)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC spCreateAnnouncement @Title = {announcement.Title}, @Description = {announcement.Description},@CreatedDate = {announcement.CreatedDate}, @Status = {announcement.Status}, @Category = {announcement.Category}, @SubCategory = {announcement.SubCategory}"
        );
        }

        public async Task UpdateAsync(Announcement announcement)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC spUpdateAnnouncement @Id = {announcement.Id}, @Title = {announcement.Title}, @Description = {announcement.Description}, @Status = {announcement.Status}, @Category = {announcement.Category}, @SubCategory = {announcement.SubCategory}"
            );
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC spDeleteAnnouncement @Id = {id}"
            );
        }
    }
}
