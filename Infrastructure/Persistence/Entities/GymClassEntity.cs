namespace Infrastructure.Persistence.Entities;

public class GymClassEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Trainer { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int MaxCapacity { get; set; }
}