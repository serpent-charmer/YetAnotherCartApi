namespace YetAnotherCartApi.Orders.Dto
{
    public class OrderId
    {
        public Guid Uid { get => Guid.Parse(Id); }
        public string Id { get; set; }
    }
}
