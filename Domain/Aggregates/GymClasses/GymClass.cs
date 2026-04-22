using System;

namespace Domain.Aggregates.GymClasses
{
    public class GymClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Trainer { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Intensity { get; set; } = null!;
    }
}