using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreatModelingApp.Core.Models;
using ThreatModelingApp.Core.Enums;
using ThreatModelingApp.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;

namespace ThreatModelingApp.Data.Repositories
{
    public class ThreatRepository : IThreatRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly string _localDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ThreatModelingApp", "threats.json");

        public ThreatRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Получение всех угроз из БД
        public async Task<IEnumerable<Threat>> GetAllThreatsAsync()
        {
            try
            {
                return await _dbContext.Threats
                    .Include(t => t.Recommendations)
                    .OrderBy(t => t.Category)
                    .ThenBy(t => t.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Если нет доступа к БД, загружаем из локального хранилища
                return await LoadFromLocalStorageAsync();
            }
        }

        // Получение угроз по категории
        public async Task<IEnumerable<Threat>> GetThreatsByCategoryAsync(ThreatCategory category)
        {
            var threats = await GetAllThreatsAsync();
            return threats.Where(t => t.Category == category);
        }

        // Фильтрация угроз на основе ответов из вопросника
        public async Task<IEnumerable<Threat>> FilterThreatsAsync(Dictionary<int, Answer> answers)
        {
            var allThreats = await GetAllThreatsAsync();
            var filteredThreats = new List<Threat>();

            foreach (var threat in allThreats)
            {
                // Если у угрозы нет условий или все условия выполнены
                if (!threat.Conditions.Any() || 
                    threat.Conditions.All(condition => 
                        answers.TryGetValue(condition.QuestionId, out var answer) && 
                        condition.IsSatisfied(answer)))
                {
                    filteredThreats.Add(threat);
                }
            }

            return filteredThreats;
        }

        // Получение угрозы по ID
        public async Task<Threat> GetThreatByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Threats
                    .Include(t => t.Recommendations)
                    .FirstOrDefaultAsync(t => t.Id == id);
            }
            catch
            {
                var threats = await LoadFromLocalStorageAsync();
                return threats.FirstOrDefault(t => t.Id == id);
            }
        }

        // Добавление пользовательской угрозы
        public async Task AddCustomThreatAsync(Threat threat)
        {
            threat.IsCustom = true;
            
            try
            {
                await _dbContext.Threats.AddAsync(threat);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                // Если БД недоступна, сохраняем локально
                await SaveCustomThreatLocallyAsync(threat);
            }
        }

        // Обновление данных угроз из сервера
        public async Task UpdateFromServerAsync()
        {
            try
            {
                var serverThreats = await _dbContext.Threats
                    .Where(t => !t.IsCustom)
                    .ToListAsync();

                await SaveToLocalStorageAsync(serverThreats);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update threats from server", ex);
            }
        }

        // Сохранение в локальное хранилище
        private async Task SaveToLocalStorageAsync(IEnumerable<Threat> threats)
        {
            try
            {
                var directory = Path.GetDirectoryName(_localDataPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var json = JsonConvert.SerializeObject(threats, Formatting.Indented);
                await File.WriteAllTextAsync(_localDataPath, json);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save threats to local storage", ex);
            }
        }

        // Загрузка из локального хранилища
        private async Task<List<Threat>> LoadFromLocalStorageAsync()
        {
            if (!File.Exists(_localDataPath))
            {
                return new List<Threat>();
            }

            try
            {
                var json = await File.ReadAllTextAsync(_localDataPath);
                return JsonConvert.DeserializeObject<List<Threat>>(json) ?? new List<Threat>();
            }
            catch
            {
                return new List<Threat>();
            }
        }

        // Сохранение пользовательской угрозы локально
        private async Task SaveCustomThreatLocallyAsync(Threat threat)
        {
            var threats = await LoadFromLocalStorageAsync();
            threat.Id = threats.Any() ? threats.Max(t => t.Id) + 1 : 1;
            threats.Add(threat);
            await SaveToLocalStorageAsync(threats);
        }
    }

    public interface IThreatRepository
    {
        Task<IEnumerable<Threat>> GetAllThreatsAsync();
        Task<IEnumerable<Threat>> GetThreatsByCategoryAsync(ThreatCategory category);
        Task<IEnumerable<Threat>> FilterThreatsAsync(Dictionary<int, Answer> answers);
        Task<Threat> GetThreatByIdAsync(int id);
        Task AddCustomThreatAsync(Threat threat);
        Task UpdateFromServerAsync();
    }
}