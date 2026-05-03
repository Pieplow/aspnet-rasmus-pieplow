using Application.GymClasses;
using Domain.Aggregates.GymClasses;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Context.Extensions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestCrossFitness;

public class GymClassTests
{
    private DataContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new DataContext(options);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSuccessfullyRemoveFromDatabase()
    {
        // Arrange
        var context = GetInMemoryContext();
        var repo = new GymClassRepository(context);
        var uow = new UnitOfWork(context);
        var service = new GymClassService(repo, uow);

        var entity = new Infrastructure.Persistence.Entities.GymClassEntity
        {
            Id = 1,
            Name = "Yoga",
            Trainer = "Anna",
            StartTime = DateTime.Now,
            MaxCapacity = 10
        };
        context.GymClasses.Add(entity);
        await context.SaveChangesAsync();

        // Act
        await service.DeleteAsync(1);

        // Assert
        var exists = await context.GymClasses.AnyAsync(x => x.Id == 1);
        Assert.False(exists);
    }
}