public class Customer : Entity
{
    public string IdentityId { get; set; }
    public string FullName {  get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public List<Subscription> Subscriptions { get; set; }
    public List<SupportTicket> Tickets { get; set; }
}