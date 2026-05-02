namespace Domain.Aggregates.GymClasses;

public class Gym
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Address { get; private set; } = null!;

    private Gym() { }

    public Gym(string name, string address)
    {
        Id = Guid.NewGuid();
        Name = name;
        Address = address;
    }

    // DDD-princip: Gymmet "äger" sina pass
    private readonly List<GymClass> _classes = new();
    public virtual IReadOnlyCollection<GymClass> Classes => _classes.AsReadOnly();
}