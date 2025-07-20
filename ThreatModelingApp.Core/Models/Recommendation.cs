namespace ThreatModelingApp.Core.Models
{
    /// <summary>
    /// Рекомендация по защите от конкретной угрозы
    /// </summary>
    public class Recommendation
    {
        /// <summary>
        /// Уникальный идентификатор рекомендации
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст рекомендации
        /// Пример: "Реализуйте двухфакторную аутентификацию"
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Подробное руководство по реализации
        /// Пример: "1. Установите модуль 2FA... 2. Настройте политики..."
        /// </summary>
        public string ImplementationGuide { get; set; }

        /// <summary>
        /// Ссылка на стандарт/нормативный документ (опционально)
        /// Пример: "СТБ 34.101.47-2023, п.5.2.1"
        /// </summary>
        public string ComplianceStandard { get; set; }

        /// <summary>
        /// Сложность реализации (Low/Medium/High)
        /// </summary>
        public ImplementationComplexity Complexity { get; set; }

        /// <summary>
        /// Ориентировочная стоимость реализации
        /// </summary>
        public string EstimatedCost { get; set; }
    }

    /// <summary>
    /// Уровень сложности реализации рекомендации
    /// </summary>
    public enum ImplementationComplexity
    {
        Low,        // Простые организационные меры
        Medium,     // Требуется настройка систем
        High        // Требуются разработка или закупка решений
    }
}