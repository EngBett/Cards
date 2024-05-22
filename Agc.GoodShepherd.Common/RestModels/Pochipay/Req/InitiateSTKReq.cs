using System.Text.Json.Serialization;

namespace Agc.GoodShepherd.Common.RestModels.Pochipay.Req;

public class InitiateSTKReq
{
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; }

    [JsonPropertyName("billRefNumber")]
    public string BillRefNumber { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }
    
    [JsonPropertyName("paybillNumber")]
    public string PaybillNumber { get; set; }

    [JsonPropertyName("amount")]
    public int Amount { get; set; }
}