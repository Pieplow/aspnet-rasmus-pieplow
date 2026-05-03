using Domain.Aggregates.GymClasses;
using Xunit;

namespace TestCrossFitness;

public class BookingTests
{
    [Fact]
    public void GymClass_Book_ShouldIncreaseCurrentBookings()
    {
        // Arrange
        var gymClass = new GymClass("Yoga", "Anna", DateTime.Now, 10);

        // Act
        gymClass.Book();

        // Assert
        Assert.Equal(1, gymClass.CurrentBookings);
    }

    [Fact]
    public void GymClass_Book_ShouldThrowException_WhenFull()
    {
        // Arrange
        var gymClass = new GymClass("Spinning", "Kalle", DateTime.Now, 1);
        gymClass.Book(); // Fyller passet

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => gymClass.Book());
    }
}