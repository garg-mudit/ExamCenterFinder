using ExamCenterFinder.API.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace ExamCenterFinder.API.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ExamCenter> ExamCenters { get; set; }

        public DbSet<ExamCenterSlot> ExamCenterSlots { get; set; }

        public DbSet<ExamCenterSlotBooking> ExamCenterSlotBookings { get; set; }
    }
}
