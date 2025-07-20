namespace ThreatModelingTool.Core.Models
{
    public class UserSettings
    {
        public string DefaultSavePath { get; set; } = @"C:\ThreatModels";
        public bool AutoCheckUpdates { get; set; } = true;
        public string LastUsedTemplate { get; set; }
        public string Theme { get; set; } = "Light";
    }
}