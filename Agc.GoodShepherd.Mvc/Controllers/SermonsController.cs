using Agc.GoodShepherd.Application.Commands.Sermons;
using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Application.Queries.Sermons;
using Agc.GoodShepherd.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Agc.GoodShepherd.Mvc.Controllers;

public class SermonsController(IMediator mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetSermonsQuery query)
    {
        var sermons = await mediator.Send(query);

        if (sermons.Code != ResponseCodes.Success)
        {
            ModelState.AddModelError("", sermons.Message);
        }

        return View(sermons.Result);
    }

    [HttpGet]
    public async Task<IActionResult> Show(string id)
    {
        SermonDm? result = await GetSermon(id);

        if (result == null) return NotFound();

        return View(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([FromBody] AddSermonCommand command)
    {
        return View();
    }


    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(string id)
    {
        SermonDm? result = await GetSermon(id);

        if (result == null) return NotFound();

        return View(result);
    }

    [HttpPatch("edit/{id}")]
    public async Task<IActionResult> Edit([FromForm] UpdateSermonCommand command, string id)
    {
        var res = await mediator.Send(command);

        if (res.Code != ResponseCodes.Success)
        {
            if (res.Errors != null && res.Errors.Any())
            {
                var i = 0;
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError($"{i}", error);
                }
            }

            ModelState.AddModelError("", res.Message);
        }

        return RedirectToAction("Edit", new { id });
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        SermonDm? result = await GetSermon(id);

        if (result == null) return NotFound();

        await mediator.Send(new DeleteSermonCommand { SermonId = id });
        
        return RedirectToAction("Index");
    }

    private async Task<SermonDm?> GetSermon(string id)
    {
        SermonDm? result = null;
        var sermon = await mediator.Send(new GetSermonsQuery() { SermonId = id });
        if (sermon.Code == ResponseCodes.Success && sermon.Result.DataList.Any())
        {
            result = sermon.Result.DataList.FirstOrDefault();
        }

        return result;
    }
}