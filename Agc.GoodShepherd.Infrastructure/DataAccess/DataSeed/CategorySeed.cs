using Agc.GoodShepherd.Domain.Models;

namespace Agc.GoodShepherd.Infrastructure.DataAccess.DataSeed;

public static class CategorySeed
{
    public static async Task SeedCategory(this AppDbContext dbContext)
    {
        if (dbContext.Categories.Any()) return;

        var categories = new List<Category>()
        {
            new()
            {
                Name = "T-Shirts",
                ImageUrl = "https://picsum.photos/500?blur=10"
            },
            new()
            {
                Name = "Hoodies",
                ImageUrl = "https://picsum.photos/500?blur=10"
            },
            new()
            {
                Name = "Wrist Bands",
                ImageUrl = "https://picsum.photos/500?blur=10"
            },
            new()
            {
                Name = "Mugs",
                ImageUrl = "https://picsum.photos/500?blur=10"
            }
        };
        
        dbContext.Categories.AddRange(categories);
    }
}

