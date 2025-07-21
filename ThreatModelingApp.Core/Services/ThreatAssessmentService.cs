using System;
using System.Collections.Generic;
using System.Linq;
using ThreatModelingTool.Core.Models;

namespace ThreatModelingTool.Core.Services
{
    /*
    public class ThreatAssessmentService
    {
        private readonly IThreatRepository _threatRepository;

        public ThreatAssessmentService(IThreatRepository threatRepository)
        {
            _threatRepository = threatRepository;
        }

        /// <summary>
        /// Фильтрация угроз на основе ответов пользователя
        /// </summary>
        public List<Threat> FilterThreats(List<Answer> userAnswers)
        {
            var allThreats = _threatRepository.GetAll();
            var relevantThreats = new List<Threat>();

            foreach (var threat in allThreats)
            {
                // Проверяем, соответствует ли угроза ответам пользователя
                if (IsThreatRelevant(threat, userAnswers))
                {
                    relevantThreats.Add(threat);
                }
            }

            // Сортируем по уровню риска (от высокого к низкому)
            return relevantThreats
                .OrderByDescending(t => t.RiskLevel)
                .ToList();
        }

        /// <summary>
        /// Получение детальной информации об угрозе
        /// </summary>
        public Threat GetThreatDetails(int threatId)
        {
            return _threatRepository.GetById(threatId) ?? 
                   throw new ArgumentException($"Threat with ID {threatId} not found");
        }

        /// <summary>
        /// Проверка релевантности угрозы на основе ответов
        /// </summary>
        private bool IsThreatRelevant(Threat threat, List<Answer> answers)
        {
            // Здесь должна быть логика сопоставления ответов с условиями угрозы
            // Например, если в ответах указано, что используется веб-приложение,
            // то включаем угрозы, связанные с веб-уязвимостями
            
            // Временная реализация - возвращаем все угрозы
            return true;
        }

        /// <summary>
        /// Корректировка уровня риска на основе дополнительных факторов
        /// </summary>
        public void ApplyRiskLevelAdjustments(List<Threat> threats)
        {
            foreach (var threat in threats)
            {
                // Пример корректировки: если угроза имеет категорию "Injection",
                // повышаем уровень риска на один уровень
                if (threat.Category == ThreatCategory.Injection && 
                    threat.RiskLevel < RiskLevel.Critical)
                {
                    threat.RiskLevel++;
                }
            }
        }
    }
    */
}