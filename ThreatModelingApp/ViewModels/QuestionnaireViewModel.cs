using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ThreatModelingApp.Core.Models;

namespace ThreatModelingApp.ViewModels
{
    public partial class QuestionnaireViewModel : ObservableObject
    {
        private readonly Action<ObservableObject> _navigateTo;
        private int _currentIndex = 0;
        private readonly List<QuestionAnswer> _answers = new();

        private readonly List<string> _questions = new()
        {
            "Это первый вопрос?",
            "Используется ли двухфакторная аутентификация?",
            "Проводится ли регулярное обновление системы?"
        };

        [ObservableProperty]
        private string currentQuestion;

        public IRelayCommand AnswerYesCommand { get; }
        public IRelayCommand AnswerNoCommand { get; }

        public QuestionnaireViewModel(Action<ObservableObject> navigateTo)
        {
            _navigateTo = navigateTo;

            AnswerYesCommand = new RelayCommand(OnAnswerYes);
            AnswerNoCommand = new RelayCommand(OnAnswerNo);

            CurrentQuestion = _questions[_currentIndex];
        }

        private void OnAnswerYes()
        {
            _answers.Add(new QuestionAnswer { Question = CurrentQuestion, Answer = "Да" });
            NextQuestion();
        }

        private void OnAnswerNo()
        {
            _answers.Add(new QuestionAnswer { Question = CurrentQuestion, Answer = "Нет" });
            NextQuestion();
        }

        private void NextQuestion()
        {
            _currentIndex++;
            if (_currentIndex < _questions.Count)
            {
                CurrentQuestion = _questions[_currentIndex];
            }
            else
            {
                var result = new ThreatAnalysisResult
                {
                    CompletedAt = DateTime.Now,
                    Answers = _answers
                };

                var json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("result.json", json);

                _navigateTo?.Invoke(new ThreatsListViewModel());
            }
        }
    }
}
