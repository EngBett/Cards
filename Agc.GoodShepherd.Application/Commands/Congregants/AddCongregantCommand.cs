using System.Net;
using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Common.Models;
using Agc.GoodShepherd.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Agc.GoodShepherd.Application.Commands.Congregants;

public class AddCongregantCommand:IRequest<ApiResponse<bool>>
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public bool Acknowledge { get; set; }
}

public class AddCongregantCommandHandler:IRequestHandler<AddCongregantCommand,ApiResponse<bool>>
{
    private readonly IAppDbContext _dbContext;
    private readonly IHttpContextAccessor _contextAccessor;

    public AddCongregantCommandHandler(IAppDbContext dbContext,IHttpContextAccessor contextAccessor)
    {
        _dbContext = dbContext;
        _contextAccessor = contextAccessor;
    }
    public async Task<ApiResponse<bool>> Handle(AddCongregantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ipAddress = GetRemoteIpAddress();

            var exists = await _dbContext.Congregants.FirstOrDefaultAsync(x => x.IpAddress == ipAddress, cancellationToken: cancellationToken);
            
            if(exists!=null) return ResponseMessage.Error(false, "You already submitted your data");
            
            if(!(await UniqueEmail(request.Email,cancellationToken))) return ResponseMessage.Error(false, "Data with that email already exist");
            if(!(await UniquePhoneNumber(request.PhoneNumber,cancellationToken))) return ResponseMessage.Error(false, "Data with that phone number already exist");
            
            var congregant = new Congregant() { 
                FirstName = request.FirstName,
                MiddleName=request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                IpAddress = ipAddress
            };

           await _dbContext.Congregants.AddAsync(congregant, cancellationToken);
           await _dbContext.SaveChangesAsync(cancellationToken);
           return ResponseMessage.Success(true);
        }
        catch (Exception e)
        {
            return ResponseMessage.Error(false);
        }
    }
    
    private async Task<bool> UniqueEmail(string email, CancellationToken cancellationToken)
    {
        var congregant =
            await _dbContext.Congregants.FirstOrDefaultAsync(x => x.Email == email,
                cancellationToken: cancellationToken);
        return congregant == null;
    }

    private async Task<bool> UniquePhoneNumber(string phone, CancellationToken cancellationToken)
    {
        var congregant =
            await _dbContext.Congregants.FirstOrDefaultAsync(x => x.PhoneNumber == phone,
                cancellationToken: cancellationToken);
        return congregant == null;
    }

    private string GetRemoteIpAddress()
    {
        var remoteIpAddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress;
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