using Application.Bookings.Responses;
using Application.Memberships.Responses;

namespace Presentation.WebApp.ViewModels;

public class MyAccountViewModel
{
        public string Email { get; set; } = "";
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }

        public List<BookingResponse> Bookings { get; set; } = new();
        public MembershipResponse? Membership { get; set; }
   

}