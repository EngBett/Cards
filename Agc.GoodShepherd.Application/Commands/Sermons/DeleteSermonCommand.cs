using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Common.Models;
using MediatR;

namespace Agc.GoodShepherd.Application.Commands.Sermons;

public class DeleteSermonCommand : IRequest<ApiResponse<SermonDm?>>
{
    public string SermonId { get; set; }
}