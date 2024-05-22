using System.Text.Json.Serialization;

namespace Agc.GoodShepherd.Common.RestModels.Pochipay.Res;

public class InitiateSTKRes
{
    [JsonPropertyName("merchantRequestId")]
    public string MerchantRequestId { get; set; }

    [JsonPropertyName("isSuccessful")]
    public bool IsSuccessful { get; set; }
}