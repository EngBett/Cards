using System.Net;
using Agc.GoodShepherd.Application.Commands.Congregants;
using Agc.GoodShepherd.Application.Queries.Ips;
using Agc.GoodShepherd.Common.Enums;
using Agc.GoodShepherd.Common.Shared;
using Azure.Core;
using Microsoft.AspNetCore.Components;

namespace Agc.GoodShepherd.Ui.Components.Pages.Congregants;

public partial class AddCongregant
{
    [SupplyParameterFromForm] private AddCongregantCommand Model { get; set; } = new();

    private List<string> Errors { get; set; } = new();
    private string Success { get; set; }
    private bool IpRegistered { get; set; }
    private bool Loading { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await IsIpRegistered();
    }

    private async Task IsIpRegistered()
    {
        Loading = true;

        var res = await Mediator.Send(new IpAddressRecordedQuery());
        if (res.Code != ResponseCodes.Success)
        {
            Errors.AddRange(res.Errors.Any() ? res.Errors : new[] { res.Message });
            return;
        }

        IpRegistered = res.Result;
        StateHasChanged();
        Loading = false;
    }

    private async Task SubmitCongregant()
    {
        if (Loading) return;


        if (!Model.Acknowledge)
        {
            Errors.Add("Please confirm that your data is correct.");
            return;
        }

        Loading = true;

        var res = await Mediator.Send(Model);

        Loading = false;
        if (res.Code != ResponseCodes.Success)
        {
            Errors.AddRange(res.Errors.Any() ? res.Errors : new[] { res.Message });
            return;
        }

        Model = new AddCongregantCommand();
        Success = "Data submitted successfully. Thanks!";
    }
    
}