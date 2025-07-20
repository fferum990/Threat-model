namespace ThreatModelingApp.Core.Models
{
    public class DocumentTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; } // Путь к .dotx файлу
        public bool IsDefault { get; set; }
    }
}