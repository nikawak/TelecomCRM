public class GeoLocation : Entity
{
    public double Latitude { get; set; }    // Широта
    public double Longitude { get; set; }   // Долгота

    public string? AddressHint { get; set; }  // Опционально: текст адреса или описания

    public string? GeoJson { get; set; }      // Опционально: если нужен Polygon, LineString и т.п.
}