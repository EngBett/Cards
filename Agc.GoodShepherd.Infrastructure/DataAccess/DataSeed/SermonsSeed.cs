using Agc.GoodShepherd.Domain.Enums;
using Agc.GoodShepherd.Domain.Models;

namespace Agc.GoodShepherd.Infrastructure.DataAccess.DataSeed;

public static class SermonsSeed
{
    public static async Task SeedSermons(this AppDbContext dbContext)
    {
        if (dbContext.Sermons.Any()) return;

        dbContext.Sermons.AddRange(new[]
        {
            new Sermon
            {
                Title = "Fasting and Prayer",
                Minister = "Ronald Bett",
                BibleBook = "Matthew",
                BibleChapter = "17:20-21",
                BibleVerse =
                    "So Jesus said to them, “Because of your unbelief; for assuredly, I say to you, if you have faith as a mustard seed, you will say to this mountain, ‘Move from here to there,’ and it will move; and nothing will be impossible for you. However, this kind does not go out except by prayer and fasting.”",
                BibleVersion = BibleVersions.NewKingJamesVersion,
                Body =
                    "[\n    \"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\",\n    \"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?\",\n    \"At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.\"\n]",
                SermonDate = DateTime.Today.AddDays(-2),
                SermonMedia = new SermonMedia("PrayerNFasting", ".png",
                    "https://images.pexels.com/photos/247851/pexels-photo-247851.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
                    "2b0fdb3ea2bd4b5ea003849ff9bc4fde", "png", MediaTypes.Image)
            },
            new Sermon
            {
                Title = "Spiritual Babies",
                Minister = "Ronald Bett",
                BibleBook = "1st Corinthians",
                BibleChapter = "3:1",
                BibleVerse =
                    "And I, brethren, could not speak to you as to spiritual men, but as to men of flesh, as to infants in Christ.",
                BibleVersion = BibleVersions.NewKingJamesVersion,
                Body =
                    "[\n    \"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\",\n    \"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?\",\n    \"At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.\"\n]",
                SermonDate = DateTime.Today.AddDays(-2),
                SermonMedia = new SermonMedia("SpiritualBabies", ".png",
                    "https://images.pexels.com/photos/2093718/pexels-photo-2093718.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
                    "2b0fdb3ea2bd4b5ea003849ff9bc4fde", "png", MediaTypes.Image)
            }
        });
        
        await dbContext.SaveChangesAsync();
    }
}