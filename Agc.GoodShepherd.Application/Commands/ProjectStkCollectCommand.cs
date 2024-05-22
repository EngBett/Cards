using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Common.Enums;
using Agc.GoodShepherd.Common.Models;
using Agc.GoodShepherd.Common.Options;
using Agc.GoodShepherd.Common.RestModels.Pochipay.Req;
using Agc.GoodShepherd.Common.RestServices;
using Agc.GoodShepherd.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Agc.GoodShepherd.Application.Commands;

public class ProjectStkCollectCommand : IRequest<ApiResponse<bool>>
{
    public string ProjectId { get; set; }
    public string PhoneNumber { get; set; }
    public int Amount { get; set; }
}

public class ProjectStkCollectCommandHandler : IRequestHandler<ProjectStkCollectCommand, ApiResponse<bool>>
{
    private readonly IAppDbContext _dbContext;
    private readonly IPochipayServices _pochipayServices;
    private readonly MpesaOptions _options;

    public ProjectStkCollectCommandHandler(IAppDbContext dbContext, IPochipayServices pochipayServices, IOptions<MpesaOptions> options)
    {
        _dbContext = dbContext;
        _pochipayServices = pochipayServices;
        _options = options.Value;
    }

    public async Task<ApiResponse<bool>> Handle(ProjectStkCollectCommand request, CancellationToken cancellationToken)
    {
        var project =
            await _dbContext.Projects.FirstOrDefaultAsync(x => x.Id == request.ProjectId,
                cancellationToken: cancellationToken);

        if (project == null)
            return ResponseMessage.Error(false, "Project not found.", responseCodes: ResponseCodes.NotFound);


        var mpesaPayment = new MpesaPayment
        {
            PhoneNumber = request.PhoneNumber,
            OrderId = project.Id,
            BillReferenceNumber = project.Id,
            Narration = "Project contribution",
            Amount = request.Amount,
        };

        var transaction = new ProjectTransaction
        {
            MpesaPaymentId = mpesaPayment.Id,
            ProjectId = project.Id,
            PhoneNumber = request.PhoneNumber,
            Amount = request.Amount,
            MpesaPayment = mpesaPayment
        };

        var stkPush = await _pochipayServices.InitiateStk(new InitiateSTKReq()
        {
            OrderId = mpesaPayment.Id,
            PaybillNumber = _options.PaybillNumber,
            PhoneNumber = request.PhoneNumber,
            Amount = request.Amount,
            BillRefNumber = request.ProjectId.Replace("-","")[..10]
        });

        if (stkPush.Result==null || !stkPush.Result.IsSuccessful) return ResponseMessage.Error(false, stkPush.Message, responseCodes:stkPush.Code);

        _dbContext.ProjectTransactions.Add(transaction);
        if (await _dbContext.SaveChangesAsync(cancellationToken) > 0)
        {
            return ResponseMessage.Success(true, "Check your phone to complete transaction.");
        }

        return ResponseMessage.Error(false, "Something went wrong");
    }
}