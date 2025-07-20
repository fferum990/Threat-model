using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ThreatModelingApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private ObservableObject _currentViewModel;
        public ObservableObject CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public ICommand StartQuestionnaireCommand { get; }
        public ICommand ShowThreatsCommand { get; }
        public ICommand UpdateDatabaseCommand { get; }
        public ICommand ShowSettingsCommand { get; }

        public MainViewModel()
        {
            StartQuestionnaireCommand = new RelayCommand(() =>
            {
                CurrentViewModel = new QuestionnaireViewModel(SetCurrentView);
            });

            ShowThreatsCommand = new RelayCommand(() =>
            {
                // Передаём пустой список, если не анализировали ещё
                CurrentViewModel = new ThreatsListViewModel(new List<(string Question, string Answer)>());
            });

            UpdateDatabaseCommand = new RelayCommand(() =>
            {
                CurrentViewModel = new UpdateViewModel();
            });

            ShowSettingsCommand = new RelayCommand(() =>
            {
                CurrentViewModel = new SettingsViewModel();
            });

            // Начальный экран
            CurrentViewModel = new QuestionnaireViewModel(SetCurrentView);
        }

        private void SetCurrentView(ObservableObject vm)
        {
            CurrentViewModel = vm;
        }
    }
}
