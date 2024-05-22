namespace Agc.GoodShepherd.Common.RestModels;

public class MpesaStkPaymentResult
{
    public Body Body { get; set; }
}

public class Body
{
    public StkCallback StkCallback { get; set; }
}

public class CallbackMetadata
{
    public List<MpesaItem> Item { get; set; } = new List<MpesaItem>();
}

public class MpesaItem
{
    public string Name { get; set; }
    public object? Value { get; set; }
}

public class StkCallback
{
    public string MerchantRequestID { get; set; }
    public string CheckoutRequestID { get; set; }
    public int ResultCode { get; set; } = -1;
    public string ResultDesc { get; set; }
    public CallbackMetadata CallbackMetadata { get; set; }
}