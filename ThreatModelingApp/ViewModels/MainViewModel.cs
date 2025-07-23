using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ThreatModelingApp.Core.Models;
using System;

namespace ThreatModelingApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject currentViewModel;

        [ObservableProperty]
        private bool isSurveyComplete;

        public RelayCommand StartQuestionnaireCommand { get; }
        public RelayCommand ShowThreatsCommand { get; }
        public RelayCommand UpdateDatabaseCommand { get; }
        public RelayCommand ShowSettingsCommand { get; }
        public RelayCommand ShowInfoCommand { get; }
        public RelayCommand ShowOrganizationInfoCommand { get; }

        // Храним ViewModel, чтобы не создавать заново каждый раз
        private OrganizationInfoViewModel organizationInfoViewModel;
        private QuestionnaireViewModel questionnaireViewModel;
        private ThreatsListViewModel threatsListViewModel;
        private UpdateViewModel updateViewModel;
        private SettingsViewModel settingsViewModel;
        private InfoViewModel infoViewModel;

        public MainViewModel()
        {
            // Инициализируем ViewModel, передавая навигацию в конструктор
            organizationInfoViewModel = new OrganizationInfoViewModel(NavigateTo);
            questionnaireViewModel = new QuestionnaireViewModel(NavigateTo);
            threatsListViewModel = new ThreatsListViewModel();
            updateViewModel = new UpdateViewModel();
            settingsViewModel = new SettingsViewModel();
            infoViewModel = new InfoViewModel();

            StartQuestionnaireCommand = new RelayCommand(StartQuestionnaire);
            ShowThreatsCommand = new RelayCommand(ShowThreats);
            UpdateDatabaseCommand = new RelayCommand(UpdateDatabase);
            ShowSettingsCommand = new RelayCommand(ShowSettings);
            ShowInfoCommand = new RelayCommand(ShowInfo);
            ShowOrganizationInfoCommand = new RelayCommand(ShowOrganizationInfo);

            // При запуске показываем организацию (как первый экран)
            ShowOrganizationInfo();
        }

        public void NavigateTo(ObservableObject viewModel)
        {
            CurrentViewModel = viewModel;

            // Если перешли на список угроз, считаем опрос завершённым
            if (viewModel is ThreatsListViewModel)
                IsSurveyComplete = true;
        }

        private void StartQuestionnaire()
        {
            CurrentViewModel = questionnaireViewModel;
        }

        private void ShowThreats()
        {
            CurrentViewModel = threatsListViewModel;
        }

        private void UpdateDatabase()
        {
            CurrentViewModel = updateViewModel;
        }

        private void ShowSettings()
        {
            CurrentViewModel = settingsViewModel;
        }

        private void ShowInfo()
        {
            CurrentViewModel = infoViewModel;
        }

        private void ShowOrganizationInfo()
        {
            CurrentViewModel = organizationInfoViewModel;
        }
    }
}
