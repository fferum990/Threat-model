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
        private int _currentIndex = 0;
        private readonly List<QuestionAnswer> _answers = new();

        private readonly List<string> _questions = new()
        {
            "Это первый вопрос???",
            "Используется ли двухфакторная аутентификация?",
            "Проводится ли регулярное обновление системы?"
        };

        [ObservableProperty]
        private string currentQuestion;

        public IRelayCommand AnswerYesCommand { get; }
        public IRelayCommand AnswerNoCommand { get; }

        public QuestionnaireViewModel()
        {
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

                File.WriteAllText("result.json", JsonSerializer.Serialize(result));

                if (App.Current.MainWindow.DataContext is MainViewModel main)
                {
                    //main.IsSurveyComplete = true;
                    main.CurrentViewModel = new ThreatsListViewModel();
                }
            }
        }
    }
}
