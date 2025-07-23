using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExcelDataReader;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows; // для Dispatcher
using ThreatModelingApp.Core.Models;

namespace ThreatModelingApp.ViewModels
{
    public partial class UpdateViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Threat> threats = new();

        [ObservableProperty]
        private string statusMessage = "Готов к обновлению базы угроз";

        [RelayCommand]
        public async Task DownloadThreatsAsync()
        {
            StatusMessage = "Скачиваю и обрабатываю базу угроз...";
            try
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using var client = new HttpClient(handler);

                var bytes = await client.GetByteArrayAsync("https://bdu.fstec.ru/files/documents/thrlist.xlsx");

                using var stream = new MemoryStream(bytes);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                using var reader = ExcelReaderFactory.CreateReader(stream);

                var dataSet = reader.AsDataSet();

                var table = dataSet.Tables[0];

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Threats.Clear();

                    for (int i = 1; i < table.Rows.Count; i++)
                    {
                        var row = table.Rows[i];
                        var idRaw = row[0]?.ToString();
                        var nameRaw = row[1]?.ToString();

                        if (string.IsNullOrWhiteSpace(nameRaw)) continue;
                        if (nameRaw.Trim().ToLower() == "наименование") continue;
                        if (!int.TryParse(idRaw, out int id)) continue;
                        if (id == 0) continue;

                        var threat = new Threat
                        {
                            ID = id,
                            Name = nameRaw.Trim(),
                            Description = row[2]?.ToString()?.Trim() ?? ""
                        };
                        Threats.Add(threat);
                    }
                });

                StatusMessage = $"Загрузка завершена. Всего угроз: {Threats.Count}";
            }
            catch (HttpRequestException ex)
            {
                StatusMessage = "Ошибка загрузки: " + ex.Message;
            }
            catch (Exception ex)
            {
                StatusMessage = "Ошибка: " + ex.Message + "\n" + ex.StackTrace;
            }
        }
    }
}
