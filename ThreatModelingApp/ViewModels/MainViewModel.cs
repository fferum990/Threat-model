using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using ThreatModelingApp.Core.Models;

namespace ThreatModelingApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object currentViewModel;

        [ObservableProperty]
        private bool isSurveyComplete;

        public RelayCommand StartQuestionnaireCommand { get; }
        public RelayCommand ShowThreatsCommand { get; }
        public RelayCommand UpdateDatabaseCommand { get; }
        public RelayCommand ShowSettingsCommand { get; }

        public MainViewModel()
        {
            StartQuestionnaireCommand = new RelayCommand(StartQuestionnaire);
            ShowThreatsCommand = new RelayCommand(ShowThreats);
            UpdateDatabaseCommand = new RelayCommand(UpdateDatabase);
            ShowSettingsCommand = new RelayCommand(ShowSettings);

            StartQuestionnaire(); // начнем с опроса
        }

        public void NavigateTo(ObservableObject viewModel)
        {
            CurrentViewModel = viewModel;

            if (viewModel is ThreatsListViewModel)
                IsSurveyComplete = true;
        }

        private void StartQuestionnaire()
        {
            CurrentViewModel = new QuestionnaireViewModel(NavigateTo);
        }

        private void ShowThreats()
        {
            CurrentViewModel = new ThreatsListViewModel();
        }

        private void UpdateDatabase()
        {
            CurrentViewModel = new UpdateViewModel();
        }

        private void ShowSettings()
        {
            CurrentViewModel = new SettingsViewModel();
        }
    }
}
