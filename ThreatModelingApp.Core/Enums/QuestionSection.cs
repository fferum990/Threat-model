namespace ThreatModelingApp.Core.Enums
{
    /// <summary>
    /// Раздел вопросника
    /// </summary>
    public enum QuestionSection
    {
        GeneralInfo,            // Общая информация
        SystemArchitecture,     // Архитектура системы
        DataFlows,             // Потоки данных
        Authentication,        // Аутентификация
        Authorization,         // Авторизация
        DataProtection,        // Защита данных
        Compliance,            // Соответствие требованиям
        ThirdPartyIntegrations // Интеграции с третьими сторонами
    }
}