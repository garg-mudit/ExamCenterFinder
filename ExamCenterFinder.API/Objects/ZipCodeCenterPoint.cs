namespace ExamCenterFinder.API.Objects
{
    public class ZipCodeCenterPoint
    {


        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ZipCodeCenterPoint(string zipCode, double latitude, double longtitude)
        {
            ZipCode = zipCode;
            Latitude = latitude;
            Longitude = longtitude;
        }
    }

}
