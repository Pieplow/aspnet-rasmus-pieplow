namespace Presentation.WebApp.Models.Booking;

public class BookingViewModel
{
    public string PageTitle { get; set; } = "Book Your Training";
    public List<GymClass> AvailableClasses { get; set; } = new();
}

public class GymClass
{
    public string Name { get; set; } = null!;
    public string Trainer { get; set; } = null!;
    public DateTime Time { get; set; } 
    public string Intensity { get; set; } = null!;
}