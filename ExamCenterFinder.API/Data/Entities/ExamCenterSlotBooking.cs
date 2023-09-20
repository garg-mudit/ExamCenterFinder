using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamCenterFinder.API.Data.Entities
{
    public class ExamCenterSlotBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookingId { get; set; }
        public int SlotId { get; set; }
        public int SlotBookedOn { get; set; }

        // Add other slot-booking-specific properties here


        [ForeignKey(nameof(SlotId))]
        public required ExamCenterSlot ExamCenterSlot { get; set; }
    }
}
