using Agc.GoodShepherd.Domain.Enums;
using Agc.GoodShepherd.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Agc.GoodShepherd.Infrastructure.DataAccess.DataSeed;

public static class MerchandiseSeed
{
    public static async Task SeedMerchandise(this AppDbContext dbContext)
    {
        if (dbContext.Merchandises.Any()) return;

        var project = await dbContext.Projects.FirstOrDefaultAsync(x => x.Title == "Project 50@50");
        var categories = await dbContext.Categories.ToListAsync();
        var tags = await dbContext.Tags.Where(x=>x.TagType==TagTypes.Merchandise).ToListAsync();

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
        
        dbContext.Merchandises.AddRange(merchandises);
        await dbContext.SaveChangesAsync();
    }
}