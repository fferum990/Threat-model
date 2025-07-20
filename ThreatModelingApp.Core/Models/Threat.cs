using System.Collections.Generic;
using ThreatModelingApp.Core.Enums;

namespace ThreatModelingApp.Core.Models
{
    public class Threat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ThreatCategory Category { get; set; }
        public ThreatLevel Level { get; set; }
        public bool IsCustom { get; set; }
        public List<ThreatCondition> Conditions { get; set; } = new List<ThreatCondition>();
        public List<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
    }
}