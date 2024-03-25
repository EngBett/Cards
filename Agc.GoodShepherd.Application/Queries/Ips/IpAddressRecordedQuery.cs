using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Agc.GoodShepherd.Application.Queries.Ips;

public class IpAddressRecordedQuery : IRequest<ApiResponse<bool>>
{
}

public class IpAddressRecordedQueryHandler : IRequestHandler<IpAddressRecordedQuery, ApiResponse<bool>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IpAddressRecordedQueryHandler(IAppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
    {
        _appDbContext = appDbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResponse<bool>> Handle(IpAddressRecordedQuery request, CancellationToken cancellationToken)
    {
        var ipAddress = GetRemoteIpAddress();
        var user = await _appDbContext.Congregants.FirstOrDefaultAsync(x => x.IpAddress == ipAddress);

        return ResponseMessage.Success(user != null);
    }
    
    private string GetRemoteIpAddress()
    {
        var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
        var result = "";
        if (remoteIpAddress == null) return result;
        // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
        // This usually only happens when the browser is on the same machine as the server.
        if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
        {
            remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList
                .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        }
        result = remoteIpAddress.ToString();

        Console.WriteLine();
        Console.WriteLine(result);
        Console.WriteLine();

        return result;
    }
}