using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Application.Dtos;
using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Common.Extensions;
using Agc.GoodShepherd.Common.Models;
using Agc.GoodShepherd.Domain.Enums;
using Agc.GoodShepherd.Domain.Models;
using MediatR;
using Newtonsoft.Json;

namespace Agc.GoodShepherd.Application.Commands.Sermons;

public class AddSermonCommand : IRequest<ApiResponse<SermonDm?>>
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
}

public class AddSermonCommandHandler : IRequestHandler<AddSermonCommand, ApiResponse<SermonDm?>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly Random _random;

    public AddSermonCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<ApiResponse<SermonDm?>> Handle(AddSermonCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var sermon = new Sermon
            {
                Minister = request.Minister,
                Title = request.Title,
                BibleBook = request.BibleBook,
                BibleChapter = request.BibleChapter,
                BibleVerse = request.BibleVerse,
                BibleVersion = request.BibleVersion.ToEnum<BibleVersions>(),
                SermonDate = request.SermonDate,
                Body = request.Body,
                SermonMedia = new SermonMedia
                {
                    Extension = "png",
                    Url = request.ImageUrl,
                    MediaType = MediaTypes.Image,
                    FileReference = Guid.NewGuid().ToString(),
                    FileName = Guid.NewGuid().ToString()
                }
            };
            await _appDbContext.Sermons.AddAsync(sermon, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResponseMessage.Success<SermonDm?>(sermon.ToDto());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}