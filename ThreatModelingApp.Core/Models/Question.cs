using System.Collections.Generic;
using ThreatModelingApp.Core.Enums;

namespace ThreatModelingApp.Core.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string HelpText { get; set; }
        public QuestionType Type { get; set; }
        public QuestionSection Section { get; set; }
        public int Order { get; set; }
        public List<AnswerOption> PossibleAnswers { get; set; } = new List<AnswerOption>();
    }
}