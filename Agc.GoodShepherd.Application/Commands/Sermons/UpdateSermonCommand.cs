using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Common.Models;
using MediatR;

namespace Agc.GoodShepherd.Application.Commands.Sermons;

public class UpdateSermonCommand : IRequest<ApiResponse<SermonDm?>>
{
    public string Minister { get; set; }
    public string Title { get; set; }
    public string BibleBook { get; set; }
    public string BibleChapter { get; set; }
    public string BibleVerse { get; set; }
    public string BibleVersion { get; set; }
    public DateTime SermonDate { get; set; }
    public string Body { get; set; }
    public string ImageUrl { get; set; }
    public string SermonId { get; set; }
}