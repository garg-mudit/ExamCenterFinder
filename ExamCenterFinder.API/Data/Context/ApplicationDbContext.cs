using ExamCenterFinder.API.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace ExamCenterFinder.API.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .HasDbFunction(typeof(ApplicationDbContext).GetMethod(nameof(CalculateDistanceMiles), new[] { typeof(double), typeof(double), typeof(double), typeof(double) }))
                .HasName(nameof(CalculateDistanceMiles));
        }

        public DbSet<ExamCenter> ExamCenters { get; set; }

        public DbSet<ExamCenterSlot> ExamCenterSlots { get; set; }

        public DbSet<ExamCenterSlotBooking> ExamCenterSlotBookings { get; set; }

        public double CalculateDistanceMiles(double lat1, double long1, double lat2, double long2) => throw new NotSupportedException();

    }
}
