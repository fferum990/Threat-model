using System;
using System.Collections.Generic;
using System.Linq;
using ThreatModelingTool.Core.Models;

namespace ThreatModelingTool.Core.Services
{
    /*public class QuestionnaireService
    {
        private readonly IQuestionRepository _questionRepository;
        private List<Answer> _currentAnswers = new();

        public QuestionnaireService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        /// <summary>
        /// Загрузка всех вопросов
        /// </summary>
        public List<Question> LoadQuestions()
        {
            return _questionRepository.GetAll();
        }

        /// <summary>
        /// Сохранение ответов пользователя
        /// </summary>
        public void SaveAnswers(List<Answer> answers)
        {
            if (answers == null) throw new ArgumentNullException(nameof(answers));
            
            _currentAnswers = answers;
        }

        /// <summary>
        /// Валидация ответов
        /// </summary>
        public bool ValidateAnswers(List<Answer> answers)
        {
            if (answers == null) return false;
            
            // Проверяем, что на все обязательные вопросы даны ответы
            var requiredQuestions = _questionRepository.GetAll()
                .Where(q => q.IsRequired)
                .Select(q => q.Id)
                .ToList();

            var answeredQuestionIds = answers
                .Where(a => !string.IsNullOrWhiteSpace(a.Value))
                .Select(a => a.QuestionId)
                .ToList();

            return requiredQuestions.All(id => answeredQuestionIds.Contains(id));
        }

        /// <summary>
        /// Получение следующих вопросов на основе текущего шага
        /// </summary>
        public List<Question> GetNextQuestions(int currentStep)
        {
            var allQuestions = _questionRepository.GetAll();
            
            // Простая логика - возвращаем вопросы группами по 5
            return allQuestions
                .Skip(currentStep * 5)
                .Take(5)
                .ToList();
        }

        /// <summary>
        /// Получение текущих ответов
        /// </summary>
        public List<Answer> GetCurrentAnswers()
        {
            return _currentAnswers;
        }
    }
     */
}