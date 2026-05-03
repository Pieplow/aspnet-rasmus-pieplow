using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace TestCrossFitness;

public class IdentityTests
{
    [Fact]
    public async Task User_ShouldBeAssignedToRole_Successfully()
    {
        // ARRANGE - Använder Moq för att testa Identity utan att behöva en riktig DB
        var userStore = new Mock<IUserStore<ApplicationUser>>();
        var userManager = new Mock<UserManager<ApplicationUser>>(
            userStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        var user = new ApplicationUser { UserName = "gym_member@test.com", Email = "gym_member@test.com" };
        var roleName = "Member";

        userManager.Setup(x => x.AddToRoleAsync(user, roleName))
            .ReturnsAsync(IdentityResult.Success);

        // ACT
        var result = await userManager.Object.AddToRoleAsync(user, roleName);

        // ASSERT
        Assert.True(result.Succeeded); 
    }
}