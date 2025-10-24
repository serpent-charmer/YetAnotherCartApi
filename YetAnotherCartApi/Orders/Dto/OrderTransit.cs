namespace YetAnotherCartApi.Orders.Dto
{
    public class OrderTransit
    {
        public Guid Uid { get => Guid.Parse(Id); }
        public string Id { get; set; }
        public Guid? ArriveAt { 
            get {
                if (!string.IsNullOrEmpty(ArriveAtId))
                {
                    return Guid.Parse(ArriveAtId);
                }
                return null;
            }
        }
        public string ArriveAtId { get; set; }
        public string? Status { get; set; }
    }
}
