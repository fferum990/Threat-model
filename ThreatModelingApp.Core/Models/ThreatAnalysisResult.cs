using System;
using System.Collections.Generic;

namespace ThreatModelingApp2.Models
{
    public class ThreatAnalysisResult
    {
        public DateTime CompletedAt { get; set; } = DateTime.Now;

        public List<QuestionAnswer> Answers { get; set; } = new();
    }

    public class QuestionAnswer
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
