using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamCenterFinder.API.Data.Entities
{
    public class ExamCenterSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SlotId { get; set; }

        public int ExamCenterId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsFilled { get; set; }

        [ForeignKey(nameof(ExamCenterId))]
        public ExamCenter ExamCenter { get; set; }
    }
}
