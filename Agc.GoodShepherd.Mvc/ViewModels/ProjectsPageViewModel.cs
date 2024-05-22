using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Common.Models;

namespace Agc.GoodShepherd.Mvc.ViewModels;

public class ProjectsPageViewModel
{
    public PagedResult<IEnumerable<ProjectDm>> Projects { get; set; }
}