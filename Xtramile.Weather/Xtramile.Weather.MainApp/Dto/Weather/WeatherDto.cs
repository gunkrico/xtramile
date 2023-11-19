namespace Xtramile.Weather.MainApp.Dto.Weather
{
    public class WeatherDto
    {
        public string City { get; set; }
        public string Country { get; set; }    
        public string Location { get; set; }
        public int Time { get; set; }
        public decimal Wind { get; set; }
        public decimal WindDirection { get; set; }
        public int Visibility { get; set; }
        public string SkyCondition { get; set; }
        public decimal TemperatureCelcius { get; set; }
        public decimal TemperatureFahrenheit { get; set; }
        public int DewPoint { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
    }
}
