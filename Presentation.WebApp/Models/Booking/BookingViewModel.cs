namespace Presentation.WebApp.Models.Booking;

public class BookingViewModel
{
    public string Title { get; set; } = "Book Your Session";
    public DateTime SelectedDate { get; set; } = DateTime.Now;

    //lägg till träningspass senare
    public List<string> AvailableTimes { get; set; } = new() { "08:00", "10:00", "14:00", "17:00" };
}