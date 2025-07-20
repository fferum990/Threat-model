namespace ThreatModelingApp.Core.Enums
{
    /// <summary>
    /// Тип вопроса в вопроснике
    /// </summary>
    public enum QuestionType
    {
        Boolean,        // Да/Нет
        SingleChoice,   // Один вариант из многих
        MultipleChoice, // Несколько вариантов
        Numeric,        // Числовой ответ
        Text,           // Текстовый ответ
        Scale           // Шкала (1-5, 1-10 и т.д.)
    }
}