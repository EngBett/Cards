using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Common.Extensions;
using Agc.GoodShepherd.Domain.Models;

namespace Agc.GoodShepherd.Application.Dtos;

public static class SermonDto
{
    public static SermonDm ToDto(this Sermon? x) => x == null
        ? null
        : new SermonDm()
        {
            Id = x.Id,
            Minister = x.Minister,
            Title = x.Title,
            BibleBook = x.BibleBook,
            BibleChapter = x.BibleChapter,
            BibleVerse = x.BibleVerse,
            BibleVersion = x.BibleVersion.GetDescription(),
            Body = x.Body,
            SermonDate = x.SermonDate,
            DateCreated = x.DateCreated,
            DateUpdated = x.DateUpdated
        };
}