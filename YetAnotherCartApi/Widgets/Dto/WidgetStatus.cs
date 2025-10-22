namespace YetAnotherCartApi.Widgets.Dto
{
    public class WidgetStatus
    {
        public IEnumerable<WidgetUid> widget { get; set; }
        public bool status { get; set; }
    }
}
