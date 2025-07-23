using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreatModelingApp.Core.Models
{
    public class OrganizationInfo
    {
        public string Name { get; set; }
        public string InnOgrn { get; set; }
        public string Type { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool ConsentGiven { get; set; }
    }
}