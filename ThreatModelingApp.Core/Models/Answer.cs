namespace ThreatModelingApp.Core.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public object Value { get; set; }
        public string Comment { get; set; }
    }
}