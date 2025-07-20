using System;

namespace ThreatModelingApp.Core.Models
{
    /// <summary>
    /// Условие для фильтрации угрозы на основе ответов из вопросника
    /// </summary>
    public class ThreatCondition
    {
        /// <summary>
        /// ID вопроса, на который ссылается условие
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Ожидаемый ответ для активации угрозы
        /// (может быть значением или диапазоном)
        /// </summary>
        public string ExpectedAnswer { get; set; }

        /// <summary>
        /// Проверяет, удовлетворяет ли ответ пользователя условию
        /// </summary>
        public bool IsSatisfied(Answer answer)
        {
            if (answer == null)
                return false;

            // Для булевых ответов
            if (bool.TryParse(ExpectedAnswer, out bool expectedBool))
            {
                return answer.Value.ToString().Equals(ExpectedAnswer, StringComparison.OrdinalIgnoreCase);
            }

            // Для числовых диапазонов (формат "min-max")
            if (ExpectedAnswer.Contains("-"))
            {
                var rangeParts = ExpectedAnswer.Split('-');
                if (rangeParts.Length == 2 && 
                    int.TryParse(rangeParts[0], out int min) && 
                    int.TryParse(rangeParts[1], out int max) &&
                    int.TryParse(answer.Value.ToString(), out int answerValue))
                {
                    return answerValue >= min && answerValue <= max;
                }
            }

            // Точное совпадение для строк
            return answer.Value.ToString().Equals(ExpectedAnswer, StringComparison.OrdinalIgnoreCase);
        }
    }
}