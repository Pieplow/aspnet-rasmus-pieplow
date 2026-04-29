using Application.Bookings.Responses;
using Application.Memberships.Responses;

namespace Presentation.WebApp.ViewModels;

public class MyAccountViewModel
{

    public List<BookingResponse> Bookings { get; set; } = new();

    public MembershipResponse? Membership { get; set; }

}