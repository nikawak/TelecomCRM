public class Subscription : Entity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
}