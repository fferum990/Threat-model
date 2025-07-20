namespace ThreatModelingTool.Core.Models
{
    public enum ThreatCategory
    {
        Authentication,     // Проблемы аутентификации
        DataLeakage,       // Утечки данных
        Dos,               // Отказ в обслуживании
        Injection,         // Инъекции (SQL, XSS и т.д.)
        Misconfiguration,  // Неправильные настройки
        AccessControl     // Проблемы контроля доступа
    }
}