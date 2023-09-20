using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamCenterFinder.API.Data.Entities
{
    public class ExamCenter
    {
        public ExamCenter()
        {
            ExamCenterSlots = new HashSet<ExamCenterSlot>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExamCenterId { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public required string Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public int MaximumSeatingCapacity { get; set; }

        [InverseProperty(nameof(ExamCenterSlot.ExamCenter))]
        public ICollection<ExamCenterSlot> ExamCenterSlots { get; set; }
    }
}
