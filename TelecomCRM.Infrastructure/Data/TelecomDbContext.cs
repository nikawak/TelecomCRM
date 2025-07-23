using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TelecomCRM.Infrastructure.Data
{
    public class TelecomDbContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Service> TelecomServices { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<GeoLocation> GeoLocations { get; set; }
        public DbSet<NetworkDevice> NetworkDevices { get; set; }
        public DbSet<CoverageZone> CoverageZones { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public TelecomDbContext(DbContextOptions<TelecomDbContext> options)
        : base(options)
        {
        }
    }
}
