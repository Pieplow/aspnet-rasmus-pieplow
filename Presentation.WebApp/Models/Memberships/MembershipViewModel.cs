namespace Presentation.WebApp.ViewModels;


public class MembershipViewModel
{
    // Lista medlemskap från databasen
    public IEnumerable<Application.Memberships.Responses.MembershipResponse> Memberships { get; set; } = [];

    // Dynamiska fält för CTA-boxen
    public string CtaTitle { get; set; } = "Get Your Membership";
    public string CtaDescription { get; set; } = "Our memberships give you access to all equipment, personal training.";
    public string CtaPhoneNumber { get; set; } = "(+46) 8 410 521 00";
    public string CtaButtonText { get; set; } = "Call Us Today";
}