using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Application.Queries.Congregants;
using Agc.GoodShepherd.Common.Enums;
using Agc.GoodShepherd.Common.Models;

namespace Agc.GoodShepherd.Ui.Components.Pages.Congregants;

public partial class Index
{
    private GetCongregantsQuery Model { get; set; }
    private PagedResult<IEnumerable<CongregantDm>> Congregants { get; set; } = new();
    private List<string> Errors { get; set; } = new();
    private bool Loading { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = new GetCongregantsQuery();
        await GetCongregants();
    }

    private async Task GetCongregants()
    {
        Loading = true;
        var res = await Mediator.Send(Model);
        Loading = false;
        if (res.Code != ResponseCodes.Success)
        {
            Errors.AddRange(res.Errors.Any() ? res.Errors : new[] { res.Message });
            return;
        }

        Congregants = res.Result;
    }

    private async Task SearchCongregants(string searchTerm)
    {
        if (Loading) return;
        
        Model.SearchTerm = searchTerm;
        await GetCongregants();
    }

    private async Task NextPage()
    {
        
    }
    
    private async Task PreviousPage()
    {
        
    }
}