using Agc.GoodShepherd.Domain.Enums;

namespace Agc.GoodShepherd.Domain.Models;

public class ProjectTransaction:BaseEntity
{
    public string MpesaPaymentId { get; set; }
    public string ProjectId { get; set; }
    public string PhoneNumber { get; set; }
    public int Amount { get; set; }
    public TransactionStatuses Status { get; set; } = TransactionStatuses.Pending;
    public MpesaPayment MpesaPayment { get; set; }
    public Project Project { get; set; }
}