using System.Collections.Generic;
//using ThreatModelingApp.Core.Enums;
//using ThreatModelingApp.Core.Models;

namespace ThreatModelingApp.Data.DbContext
{
    /*
    public static class SeedData
    {
        public static List<Threat> GetThreats()
        {
            return new List<Threat>
            {
                new Threat
                {
                    Id = 1,
                    Name = "Несанкционированный доступ",
                    Description = "Доступ к системе без соответствующих прав",
                    Category = ThreatCategory.AccessControl,
                    Level = ThreatLevel.High,
                    Conditions = new List<ThreatCondition>
                    {
                        new ThreatCondition { QuestionId = 1, ExpectedAnswer = "true" }
                    },
                    Recommendations = new List<Recommendation>
                    {
                        new Recommendation
                        {
                            Id = 1,
                            Text = "Реализовать многофакторную аутентификацию",
                            ImplementationGuide = "Использовать комбинацию пароля и SMS-кода",
                            ThreatId = 1
                        }
                    }
                }
                // Другие угрозы...
            };
        }

        public static List<Question> GetQuestions()
        {
            return new List<Question>
            {
                new Question
                {
                    Id = 1,
                    Text = "Система содержит конфиденциальные данные?",
                    Type = QuestionType.Boolean,
                    Section = QuestionSection.GeneralInfo,
                    Order = 1,
                    PossibleAnswers = new List<AnswerOption>
                    {
                        new AnswerOption { Id = 1, Text = "Да", Value = true },
                        new AnswerOption { Id = 2, Text = "Нет", Value = false }
                    }
                }
                // Другие вопросы...
            };
        }

        public static List<DocumentTemplate> GetTemplates()
        {
            return new List<DocumentTemplate>
            {
                new DocumentTemplate
                {
                    Id = 1,
                    Name = "Базовый шаблон",
                    Description = "Стандартный шаблон модели угроз",
                    FilePath = "Templates/base_template.dotx",
                    IsDefault = true
                }
                // Другие шаблоны...
            };
        }
    }
    */
}