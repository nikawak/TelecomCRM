

using Microsoft.AspNetCore.Identity;

public class Customer : Entity
{
    public string IdentityId { get; set; }
    public IdentityUser UserInfo { get; set; }
    public string Address { get; set; }
    public string FullName { get; set; }
    public List<Subscription> Subscriptions { get; set; }
    public List<SupportTicket> Tickets { get; set; }
}