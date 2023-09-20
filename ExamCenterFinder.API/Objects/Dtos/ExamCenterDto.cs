namespace ExamCenterFinder.API.Objects.Dtos
{
    public class ExamCenterDto
    {
        public string ExamCenterName { get; set; }
        public string Address { get; set; }
        public double DistanceMiles { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Seats { get; set; }
        public SlotDetailsDto SlotDetails { get; set; }
    }
}
