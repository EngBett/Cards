using Agc.GoodShepherd.Domain.Enums;
using Agc.GoodShepherd.Domain.Models;

namespace Agc.GoodShepherd.Infrastructure.DataAccess.DataSeed;

public static class TagSeed
{
    public static async Task SeedTags(this AppDbContext dbContext)
    {
        if (dbContext.Tags.Any()) return;

        var tags = new List<Tag>()
        {
            new()
            {
                Name = "X Small",
                Abrv = "XS",
                TagType = TagTypes.Merchandise
            },
            new()
            {
                Name = "Small",
                Abrv = "S",
                TagType = TagTypes.Merchandise
            },
            new()
            {
                Name = "Large",
                Abrv = "L",
                TagType = TagTypes.Merchandise
            },
            new()
            {
                Name = "XLarge",
                Abrv = "XL",
                TagType = TagTypes.Merchandise
            }
            
        };
        
        dbContext.AddRange(tags);
        await dbContext.SaveChangesAsync();
    }
}