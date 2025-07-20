namespace ThreatModelingApp.Core.Models
{
    /// <summary>
    /// Вариант ответа на вопрос
    /// </summary>
    public class AnswerOption
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Текст варианта ответа (то, что видит пользователь)
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Значение варианта ответа (для обработки логики)
        /// </summary>
        public object Value { get; set; }
        
        /// <summary>
        /// Подсказка/пояснение к варианту ответа (опционально)
        /// </summary>
        public string Hint { get; set; }
    }
}