using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ThreatModelingApp.Core.Models;

namespace ThreatModelingApp.Data.Repositories
{
    public class LocalDataRepository : IDataRepository
    {
        private readonly string _appDataPath;
        private const string ThreatsFileName = "threats.json";
        private const string QuestionsFileName = "questions.json";
        private const string AnswersFileName = "answers.json";
        private const string TemplatesFileName = "templates.json";

        public LocalDataRepository()
        {
            _appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ThreatModelingApp");

            EnsureDirectoryExists();
        }

        #region Threats

        public async Task<IEnumerable<Threat>> GetAllThreatsAsync()
        {
            return await LoadFromFileAsync<List<Threat>>(ThreatsFileName) ?? new List<Threat>();
        }

        public async Task SaveThreatsAsync(IEnumerable<Threat> threats)
        {
            await SaveToFileAsync(ThreatsFileName, threats.ToList());
        }

        public async Task AddOrUpdateThreatAsync(Threat threat)
        {
            var threats = (await GetAllThreatsAsync()).ToList();
            var existing = threats.FirstOrDefault(t => t.Id == threat.Id);

            if (existing != null)
            {
                threats.Remove(existing);
            }

            threats.Add(threat);
            await SaveThreatsAsync(threats);
        }

        #endregion

        #region Questions

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await LoadFromFileAsync<List<Question>>(QuestionsFileName) ?? new List<Question>();
        }

        public async Task SaveQuestionsAsync(IEnumerable<Question> questions)
        {
            await SaveToFileAsync(QuestionsFileName, questions.ToList());
        }

        #endregion

        #region Answers

        public async Task<Dictionary<int, Answer>> GetAnswersAsync()
        {
            var answers = await LoadFromFileAsync<List<Answer>>(AnswersFileName) ?? new List<Answer>();
            return answers.ToDictionary(a => a.QuestionId, a => a);
        }

        public async Task SaveAnswersAsync(Dictionary<int, Answer> answers)
        {
            await SaveToFileAsync(AnswersFileName, answers.Values.ToList());
        }

        #endregion

        #region Templates

        public async Task<IEnumerable<DocumentTemplate>> GetTemplatesAsync()
        {
            return await LoadFromFileAsync<List<DocumentTemplate>>(TemplatesFileName) ?? new List<DocumentTemplate>();
        }

        public async Task SaveTemplatesAsync(IEnumerable<DocumentTemplate> templates)
        {
            await SaveToFileAsync(TemplatesFileName, templates.ToList());
        }

        #endregion

        #region Private Helpers

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(_appDataPath))
            {
                Directory.CreateDirectory(_appDataPath);
            }
        }

        private async Task<T> LoadFromFileAsync<T>(string fileName) where T : class
        {
            var filePath = Path.Combine(_appDataPath, fileName);

            if (!File.Exists(filePath))
            {
                return null;
            }

            try
            {
                var json = await File.ReadAllTextAsync(filePath);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error loading file {fileName}: {ex.Message}");
                return null;
            }
        }

        private async Task SaveToFileAsync<T>(string fileName, T data)
        {
            var filePath = Path.Combine(_appDataPath, fileName);

            try
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error saving file {fileName}: {ex.Message}");
                throw;
            }
        }

        #endregion
    }

    public interface IDataRepository
    {
        // Threats
        Task<IEnumerable<Threat>> GetAllThreatsAsync();
        Task SaveThreatsAsync(IEnumerable<Threat> threats);
        Task AddOrUpdateThreatAsync(Threat threat);

        // Questions
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task SaveQuestionsAsync(IEnumerable<Question> questions);

        // Answers
        Task<Dictionary<int, Answer>> GetAnswersAsync();
        Task SaveAnswersAsync(Dictionary<int, Answer> answers);

        // Templates
        Task<IEnumerable<DocumentTemplate>> GetTemplatesAsync();
        Task SaveTemplatesAsync(IEnumerable<DocumentTemplate> templates);
    }
}