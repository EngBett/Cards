namespace Agc.GoodShepherd.Application.ViewModels;

public class OrderItemVm
{
    public string Description { get; set; }
    public decimal Units { get; set; }
    public string ServiceId { get; set; }
    public IEnumerable<string>? ExtraServices { get; set; }
}