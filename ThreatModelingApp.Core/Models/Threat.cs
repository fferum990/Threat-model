using System.Collections.Generic;
using ThreatModelingApp.Core.Enums;

namespace ThreatModelingApp.Core.Models
{
    public class Threat
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public ThreatCategory Category { get; set; }
        //public ThreatLevel Level { get; set; }
        public bool IsCustom { get; set; }
        public List<ThreatCondition> Conditions { get; set; } = new List<ThreatCondition>();
        public List<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
	    //public RiskLevel RiskLevel { get; set; }
        public List<Mitigation> Mitigations { get; set; } = new();
        public string Reference { get; set; }
        public bool IsSelected { get; set; }
    }

    public class Mitigation
    {
        public string Description { get; set; }
        public string Implementation { get; set; }
    }
}