using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Text.Json;
using ThreatModelingApp.Core.Models;
using System;

namespace ThreatModelingApp.ViewModels
{
    public partial class OrganizationInfoViewModel : ObservableObject
    {
        private readonly Action<ObservableObject> navigateTo;

        [ObservableProperty]
        private OrganizationInfo organizationInfo = new();

        public OrganizationInfoViewModel(Action<ObservableObject> navigateTo)
        {
            this.navigateTo = navigateTo;
            Load();
        }

        [RelayCommand]
        private void Save()
        {
            var json = JsonSerializer.Serialize(OrganizationInfo, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("organization_info.json", json);

            // После сохранения перейти к следующему экрану (например, анкетированию)
            navigateTo(new QuestionnaireViewModel(navigateTo));
        }

        private void Load()
        {
            if (File.Exists("organization_info.json"))
            {
                var json = File.ReadAllText("organization_info.json");
                var data = JsonSerializer.Deserialize<OrganizationInfo>(json);
                if (data != null)
                    OrganizationInfo = data;
            }
        }
    }
}
