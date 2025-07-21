using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ThreatModelingApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object currentViewModel;

        public MainViewModel()
        {
            StartQuestionnaireCommand = new RelayCommand(StartQuestionnaire);
            ShowThreatsCommand = new RelayCommand(ShowThreats);
            UpdateDatabaseCommand = new RelayCommand(UpdateDatabase);
            ShowSettingsCommand = new RelayCommand(ShowSettings);

            CurrentViewModel = new QuestionnaireViewModel(); // по умолчанию
        }

        public RelayCommand StartQuestionnaireCommand { get; }
        public RelayCommand ShowThreatsCommand { get; }
        public RelayCommand UpdateDatabaseCommand { get; }
        public RelayCommand ShowSettingsCommand { get; }

        private void StartQuestionnaire() => CurrentViewModel = new QuestionnaireViewModel();

        private void ShowThreats() => CurrentViewModel = new ThreatsListViewModel();

        private void UpdateDatabase() => CurrentViewModel = new UpdateViewModel();

        private void ShowSettings() => CurrentViewModel = new SettingsViewModel();
    }
}
