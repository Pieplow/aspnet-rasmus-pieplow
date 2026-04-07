namespace Domain.Aggregates.Memberships;

    public sealed class Membership
    {
    private Membership(string id, string title, string description, List<string> benefits, decimal price, int monthlyClasses)
    {
        Id = Required(id, nameof(Id));
        Title = Required(title, nameof(Title));
        Description = Required(description, nameof(Description));
        Benefits = benefits;
        Price = CheckPriceValue (price, nameof (Price));
        MonthlyClasses = monthlyClasses;
    }

            public string Id { get; }
            public string Title { get; set; }

            public string Description { get; set; }

            public List<string> Benefits { get; set; }
            public decimal Price { get; private set; }
            public int MonthlyClasses { get; private set; }

            private static string Required(string value, string propertyName)
                    {
                if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"The {propertyName} is required.");
               
                return value.Trim();
                    }

               private static decimal CheckPriceValue(decimal value, string propertyName)
                    {
                if (value < 0)
                throw new ArgumentException($"The {propertyName} must be a positive value.");
               
                return value;
                }

                public static Membership Create(string title, string description, List<string> benefits, decimal price = 0, int MonthlyClasses =0) => 
                    new(Guid.NewGuid().ToString(), title, description, benefits, price, MonthlyClasses);

                public static Membership Create(string id, string title, string description, List<string> benefits, decimal price = 0, int MonthlyClasses = 0) =>
                    new(id, title, description, benefits, price, MonthlyClasses);
}   

