using Agc.GoodShepherd.Domain.Enums;

namespace Agc.GoodShepherd.Domain.Models;

public class MpesaPayment : BaseEntity
{
    public string PhoneNumber { get; set; }
    public string OrderId { get; set; }
    public string BillReferenceNumber { get; set; }
    public string? Narration { get; set; }
    public decimal Amount { get; set; }
    public string? MerchantRequestId { get; set; }
    public string? CheckoutRequestId { get; set; }
    public string? ResponseCode { get; set; }
    public string? ResponseDescription { get; set; }
    public string? ResultCode { get; set; }
    public string? ResultDescription { get; set; }
    public string? MpesaReference { get; set; }
    public TransactionStatuses Status { get; set; } = TransactionStatuses.Pending;
    public bool? IsSuccessful { get; set; }
    public string? ShortCode { get; set; }
    public string? PartyB { get; set; }
}