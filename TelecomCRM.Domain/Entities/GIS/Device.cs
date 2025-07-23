public class NetworkDevice : Entity
{
    public DeviceType DeviceType { get; set; } // "Router", "Switch", etc.
    public string SerialNumber { get; set; }
    public string Status { get; set; }

    public GeoLocation Location { get; set; }
}
