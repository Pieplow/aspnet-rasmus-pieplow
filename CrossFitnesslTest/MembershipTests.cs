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
        var context = GetInMemoryContext();
        var uow = new UnitOfWork(context);

        var userId = "test-user-123";

        
        var membership = new MembershipEntity { UserId = userId };

        context.Memberships.Add(membership);
        await uow.CompleteAsync();

        var savedMembership = await context.Memberships.FirstOrDefaultAsync(m => m.UserId == userId);

        Assert.NotNull(savedMembership);
        Assert.Equal(userId, savedMembership.UserId);
    }
}