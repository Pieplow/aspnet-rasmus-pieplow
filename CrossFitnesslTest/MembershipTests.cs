using Application.Memberships.Commands;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Context.Extensions;
using Infrastructure.Persistence.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestCrossFitness;

public class MembershipTests
{
    private DataContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new DataContext(options);
    }

    [Fact]
    public async Task CreateMembership_ShouldSaveToDatabase()
    {
        // ARRANGE
        var context = GetInMemoryContext();
        var uow = new UnitOfWork(context);
        var userId = "test-user-123";

        // ACT - Här lägger vi till Title och Description som saknades i image_edc8e4.png
        var membership = new MembershipEntity
        {
            UserId = userId,
            Title = "Standard",        
            Description = "Beskrivning" 
        };

        context.Memberships.Add(membership);
        await uow.CompleteAsync();

        // ASSERT
        var savedMembership = await context.Memberships.FirstOrDefaultAsync(m => m.UserId == userId);
        Assert.NotNull(savedMembership);
        Assert.Equal("Standard", savedMembership.Title);
    }
}