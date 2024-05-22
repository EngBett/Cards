using Agc.GoodShepherd.Common.RestModels.Pochipay.Req;
using Agc.GoodShepherd.Common.RestModels.Pochipay.Res;
using Refit;

namespace Agc.GoodShepherd.Common.RestServices;

public interface IPochipayServices
{
    [Post("/api/payment/initiatestkpush")]
    Task<Models.ApiResponse<InitiateSTKRes>> InitiateStk([Body] InitiateSTKReq req);
}