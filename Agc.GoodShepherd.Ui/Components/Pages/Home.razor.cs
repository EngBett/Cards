namespace Agc.GoodShepherd.Ui.Components.Pages;

public partial class Home
{
    protected override async Task OnInitializedAsync()
    {
        NavManager.NavigateTo("https://goodshepherdagc.org/");
    }
}