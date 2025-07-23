public class CoverageZone : Entity
{
    public string Name { get; set; }
    public string GeometryJson { get; set; } // GeoJSON polygon (для Leaflet)
}
