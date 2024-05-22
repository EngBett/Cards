using Agc.GoodShepherd.Application.DisplayModels;

namespace Agc.GoodShepherd.Mvc.ViewModels;

public class HomePageViewModel
{
    public IEnumerable<SermonDm> Sermons { get; set; }
    public IEnumerable<AnnouncementDm> Announcements { get; set; }
    public IEnumerable<TukioDm> Events { get; set; }
}