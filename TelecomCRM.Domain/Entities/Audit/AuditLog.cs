public class AuditLog : Entity
{
    public string EntityType { get; set; }
    public int EntityId { get; set; }
    public LogAction Action { get; set; }
    public DateTime Timestamp { get; set; }
    public string? PerformedBy { get; set; }
    public string? ChangesJson { get; set; }
}
