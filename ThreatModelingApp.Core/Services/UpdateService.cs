using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ThreatModelingTool.Core.Models;

namespace ThreatModelingTool.Core.Services
{
    public class UpdateService
    {
        private readonly string _localDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private readonly IThreatRepository _remoteThreatRepository;
        private readonly IQuestionRepository _remoteQuestionRepository;

        public UpdateService(
            IThreatRepository remoteThreatRepository,
            IQuestionRepository remoteQuestionRepository)
        {
            _remoteThreatRepository = remoteThreatRepository;
            _remoteQuestionRepository = remoteQuestionRepository;
            
            // Создаем папку для данных, если ее нет
            Directory.CreateDirectory(_localDataPath);
        }

        /// <summary>
        /// Проверка доступности обновлений
        /// </summary>
        public async Task<bool> CheckForUpdatesAsync()
        {
            try
            {
                var remoteThreats = await _remoteThreatRepository.GetAllAsync();
                var localThreats = LoadLocalData<List<Threat>>("threats.json");

                // Сравниваем количество записей (можно добавить сравнение по дате изменения)
                return remoteThreats.Count != localThreats?.Count;
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Console.WriteLine($"Error checking for updates: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Загрузка обновлений
        /// </summary>
        public async Task DownloadUpdatesAsync()
        {
            try
            {
                var threats = await _remoteThreatRepository.GetAllAsync();
                var questions = await _remoteQuestionRepository.GetAllAsync();

                SaveLocalData("threats.json", threats);
                SaveLocalData("questions.json", questions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading updates: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Сохранение данных локально в JSON
        /// </summary>
        public void SaveLocalData<T>(string fileName, T data)
        {
            var path = Path.Combine(_localDataPath, fileName);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Загрузка локальных данных из JSON
        /// </summary>
        public T LoadLocalData<T>(string fileName)
        {
            var path = Path.Combine(_localDataPath, fileName);
            
            if (!File.Exists(path))
                return default;

            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}