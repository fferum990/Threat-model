using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace ThreatModelingApp.ViewModels
{
    public class ThreatsListViewModel : ObservableObject
    {
        public List<string> RelevantThreats { get; }

        public ThreatsListViewModel(List<(string Question, string Answer)> answers)
        {
            // Простая логика: если где-то "Нет", добавим угрозу
            RelevantThreats = new();

            foreach (var (question, answer) in answers)
            {
                if (answer == "Нет")
                {
                    RelevantThreats.Add($"⚠ Угроза по: {question}");
                }
            }

            if (RelevantThreats.Count == 0)
                RelevantThreats.Add("✅ Угроз не выявлено.");
        }
    }
}
