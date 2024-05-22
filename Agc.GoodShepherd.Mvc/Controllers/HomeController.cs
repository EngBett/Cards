using System.Diagnostics;
using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Application.Queries.Announcements;
using Agc.GoodShepherd.Application.Queries.Sermons;
using Agc.GoodShepherd.Application.Queries.Tukios;
using Agc.GoodShepherd.Common.Enums;
using Agc.GoodShepherd.Domain.Enums;
using Agc.GoodShepherd.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Agc.GoodShepherd.Mvc.Models;
using Agc.GoodShepherd.Mvc.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agc.GoodShepherd.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMediator _mediator;
    private readonly IAppDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, IMediator mediator, IAppDbContext dbContext)
    {
        _logger = logger;
        _mediator = mediator;
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomePageViewModel();
        //Sermons
        var sermons = await _mediator.Send(new GetSermonsQuery { OrderByDesc = true, PageSize = 5 });
        if (sermons.Code == ResponseCodes.Success) model.Sermons = sermons.Result.DataList;
        
        //announcements
        var announcements = await _mediator.Send(new GetAnnouncementsQuery { OrderByDesc = true, PageSize = 3 });
        if (announcements.Code == ResponseCodes.Success) model.Announcements = announcements.Result.DataList;
        
        //events
        var events = await _mediator.Send(new GetTukiosQuery() { OrderByDesc = true, PageSize = 3 });
        if (events.Code == ResponseCodes.Success) model.Events = events.Result.DataList;
        
        return View(model);
    }

    public async Task<IActionResult> Privacy()
    {
        
        if (_dbContext.Merchandises.Any()) return View();

        var project = await _dbContext.Projects.FirstOrDefaultAsync(x => x.Title == "Project 50@50");
        var categories = await _dbContext.Categories.ToListAsync();
        var tags = await _dbContext.Tags.Where(x=>x.TagType==TagTypes.Merchandise).ToListAsync();

        var merchandises = new List<Merchandise>();
        categories.ForEach(x =>
        {
            merchandises.Add(new Merchandise()
            {
                ProjectId = project.Id,
                CategoryId = x.Id,
                Name = x.Name,
                Price = new Random().Next(899, 5000),
                ImageUrl = x.ImageUrl,
                Stock = new Random().Next(9, 15),
                Tags = tags
            });
        });
        
        _dbContext.Merchandises.AddRange(merchandises);
        await _dbContext.SaveChangesAsync(new CancellationToken());
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}