public class Service : Entity
{
    public string Name { get; set; } // "��������", "���������"
    public string Description { get; set; }
    public decimal MonthlyFee { get; set; }

    public List<Subscription> Subscriptions { get; set; }
}
