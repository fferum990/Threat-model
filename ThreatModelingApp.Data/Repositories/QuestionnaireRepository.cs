using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using ThreatModelingApp.Core.Models;
//using ThreatModelingApp.Core.Enums;
using ThreatModelingApp.Data.DbContext;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
using System.IO;

namespace ThreatModelingApp.Data.Repositories
{
    /*
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly string _localQuestionsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "ThreatModelingApp", 
            "questions.json");
        private readonly string _localAnswersPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ThreatModelingApp",
            "answers.json");

        public QuestionnaireRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Получение всех вопросов
        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            try
            {
                return await _dbContext.Questions
                    .Include(q => q.PossibleAnswers)
                    .OrderBy(q => q.Section)
                    .ThenBy(q => q.Order)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Если БД недоступна, загружаем из локального хранилища
                return await LoadQuestionsFromLocalStorageAsync();
            }
        }

        // Получение вопросов по разделу
        public async Task<IEnumerable<Question>> GetQuestionsBySectionAsync(QuestionSection section)
        {
            var questions = await GetAllQuestionsAsync();
            return questions.Where(q => q.Section == section);
        }

        // Получение вопроса по ID
        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Questions
                    .Include(q => q.PossibleAnswers)
                    .FirstOrDefaultAsync(q => q.Id == id);
            }
            catch
            {
                var questions = await LoadQuestionsFromLocalStorageAsync();
                return questions.FirstOrDefault(q => q.Id == id);
            }
        }

        // Сохранение ответов пользователя
        public async Task SaveAnswersAsync(Dictionary<int, Answer> answers)
        {
            try
            {
                // Для БД преобразуем в список
                var answerList = answers.Values.ToList();
                
                // Удаляем старые ответы
                var existingAnswers = await _dbContext.Answers.ToListAsync();
                _dbContext.Answers.RemoveRange(existingAnswers);
                
                // Добавляем новые
                await _dbContext.Answers.AddRangeAsync(answerList);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                // Если БД недоступна, сохраняем локально
                await SaveAnswersToLocalStorageAsync(answers);
            }
        }

        // Загрузка ответов пользователя
        public async Task<Dictionary<int, Answer>> LoadAnswersAsync()
        {
            try
            {
                var answers = await _dbContext.Answers.ToListAsync();
                return answers.ToDictionary(a => a.QuestionId, a => a);
            }
            catch
            {
                return await LoadAnswersFromLocalStorageAsync();
            }
        }

        // Обновление вопросов с сервера
        public async Task UpdateQuestionsFromServerAsync()
        {
            try
            {
                var serverQuestions = await _dbContext.Questions
                    .Include(q => q.PossibleAnswers)
                    .ToListAsync();

                await SaveQuestionsToLocalStorageAsync(serverQuestions);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update questions from server", ex);
            }
        }

        #region Private Methods

        private async Task<List<Question>> LoadQuestionsFromLocalStorageAsync()
        {
            if (!File.Exists(_localQuestionsPath))
            {
                return new List<Question>();
            }

            try
            {
                var json = await File.ReadAllTextAsync(_localQuestionsPath);
                return JsonConvert.DeserializeObject<List<Question>>(json) ?? new List<Question>();
            }
            catch
            {
                return new List<Question>();
            }
        }

        private async Task SaveQuestionsToLocalStorageAsync(IEnumerable<Question> questions)
        {
            try
            {
                var directory = Path.GetDirectoryName(_localQuestionsPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var json = JsonConvert.SerializeObject(questions, Formatting.Indented, 
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                await File.WriteAllTextAsync(_localQuestionsPath, json);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save questions to local storage", ex);
            }
        }

        private async Task<Dictionary<int, Answer>> LoadAnswersFromLocalStorageAsync()
        {
            if (!File.Exists(_localAnswersPath))
            {
                return new Dictionary<int, Answer>();
            }

            try
            {
                var json = await File.ReadAllTextAsync(_localAnswersPath);
                var answers = JsonConvert.DeserializeObject<List<Answer>>(json) ?? new List<Answer>();
                return answers.ToDictionary(a => a.QuestionId, a => a);
            }
            catch
            {
                return new Dictionary<int, Answer>();
            }
        }

        private async Task SaveAnswersToLocalStorageAsync(Dictionary<int, Answer> answers)
        {
            try
            {
                var directory = Path.GetDirectoryName(_localAnswersPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var json = JsonConvert.SerializeObject(answers.Values.ToList(), Formatting.Indented);
                await File.WriteAllTextAsync(_localAnswersPath, json);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save answers to local storage", ex);
            }
        }

        #endregion
    }

    public interface IQuestionnaireRepository
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<IEnumerable<Question>> GetQuestionsBySectionAsync(QuestionSection section);
        Task<Question> GetQuestionByIdAsync(int id);
        Task SaveAnswersAsync(Dictionary<int, Answer> answers);
        Task<Dictionary<int, Answer>> LoadAnswersAsync();
        Task UpdateQuestionsFromServerAsync();
    }
    */
}