public class SupportTicket : Entity
{
    public int CustomerId { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public TicketStatus Status { get; set; }

    public GeoLocation? Location { get; set; } // для GIS

    public Customer Customer { get; set; }
}