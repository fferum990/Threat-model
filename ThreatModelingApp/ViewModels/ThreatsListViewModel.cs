using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ThreatModelingApp.Core.Models;

namespace ThreatModelingApp.ViewModels
{
    public partial class ThreatsListViewModel : ObservableObject
    {
        [ObservableProperty]
        private string threatSummary;

        public ThreatsListViewModel()
        {
            if (File.Exists("result.json"))
            {
                var json = File.ReadAllText("result.json");
                var result = JsonSerializer.Deserialize<ThreatAnalysisResult>(json);

                var relevantThreats = new List<string>();

                foreach (var answer in result.Answers)
                {
                    if (answer.Answer == "Нет")
                        relevantThreats.Add($"⚠ Угроза по: {answer.Question}");
                }

                ThreatSummary = relevantThreats.Count == 0
                    ? "✅ Угроз не выявлено."
                    : string.Join("\n", relevantThreats);
            }
            else
            {
                ThreatSummary = "Результаты не найдены.";
            }
        }
    }
}
