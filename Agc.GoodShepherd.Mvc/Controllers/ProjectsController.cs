using Agc.GoodShepherd.Application.Commands;
using Agc.GoodShepherd.Application.Queries.Projects;
using Agc.GoodShepherd.Common.Enums;
using Agc.GoodShepherd.Mvc.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Agc.GoodShepherd.Mvc.Controllers;

public class ProjectsController : Controller
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetProjectsQuery query)
    {
        var model = new ProjectsPageViewModel();
        var projects = await _mediator.Send(query);
        
        if(projects.Code==ResponseCodes.Success) model.Projects = projects.Result;
        
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Show([FromQuery] GetSingleProjectQuery query)
    {
        var model = new ShowProjectPageViewModel();
        var project = await _mediator.Send(query);

        if (project.Code != ResponseCodes.Success) return NotFound();
        
        model.Project = project.Result;
        
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Collect([FromBody] ProjectStkPushViewModel model)
    {
        var req = new ProjectStkCollectCommand
        {
            Amount = model.Amount,
            PhoneNumber = model.PhoneNumber,
            ProjectId = model.ProjectId
        };
        var stkPush = await _mediator.Send(req);
        return Ok(stkPush);
    }
    
    
}