using System;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ThreatModelingTool.Core.Models;

namespace ThreatModelingTool.Core.Services
{
    public class DocumentGenerationService
    {
        /// <summary>
        /// Генерация документа на основе шаблона
        /// </summary>
        public void GenerateDocument(List<Threat> threats, string templatePath, string outputPath)
        {
            if (!File.Exists(templatePath))
                throw new FileNotFoundException("Template file not found", templatePath);

            // Копируем шаблон в новое место
            File.Copy(templatePath, outputPath, true);

            // Открываем документ для редактирования
            using var wordDocument = WordprocessingDocument.Open(outputPath, true);
            var body = wordDocument.MainDocumentPart?.Document.Body;

            if (body == null)
                throw new InvalidOperationException("Invalid Word document template");

            // Заменяем плейсхолдеры
            ReplacePlaceholders(body, threats);

            // Добавляем таблицу с угрозами
            InsertThreatTable(body, threats);

            // Сохраняем изменения
            wordDocument.Save();
        }

        private void ReplacePlaceholders(Body body, List<Threat> threats)
        {
            var placeholders = new Dictionary<string, string>
            {
                {"{DATE}", DateTime.Now.ToString("dd.MM.yyyy")},
                {"{THREAT_COUNT}", threats.Count.ToString()},
                {"{HIGH_RISK_COUNT}", threats.Count(t => t.RiskLevel >= RiskLevel.High).ToString()}
            };

            foreach (var paragraph in body.Elements<Paragraph>())
            {
                foreach (var run in paragraph.Elements<Run>())
                {
                    foreach (var text in run.Elements<Text>())
                    {
                        foreach (var ph in placeholders)
                        {
                            if (text.Text.Contains(ph.Key))
                            {
                                text.Text = text.Text.Replace(ph.Key, ph.Value);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Вставка таблицы с угрозами в документ
        /// </summary>
        public void InsertThreatTable(Body body, List<Threat> threats)
        {
            // Создаем таблицу
            var table = new Table();
            
            // Стили таблицы
            var tableProperties = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = BorderValues.Single, Size = 4 },
                    new BottomBorder { Val = BorderValues.Single, Size = 4 },
                    new LeftBorder { Val = BorderValues.Single, Size = 4 },
                    new RightBorder { Val = BorderValues.Single, Size = 4 },
                    new InsideHorizontalBorder { Val = BorderValues.Single, Size = 4 },
                    new InsideVerticalBorder { Val = BorderValues.Single, Size = 4 }
                )
            );
            table.AppendChild(tableProperties);

            // Заголовки таблицы
            var headerRow = new TableRow();
            headerRow.Append(
                CreateCell("ID", true),
                CreateCell("Угроза", true),
                CreateCell("Уровень риска", true),
                CreateCell("Рекомендации", true)
            );
            table.AppendChild(headerRow);

            // Данные угроз
            foreach (var threat in threats)
            {
                var row = new TableRow();
                row.Append(
                    CreateCell(threat.Id.ToString()),
                    CreateCell(threat.Name),
                    CreateCell(threat.RiskLevel.ToString()),
                    CreateCell(string.Join("\n", threat.Mitigations.Select(m => m.Description)))
                );
                table.AppendChild(row);
            }

            // Вставляем таблицу в документ
            body.AppendChild(table);
        }

        private TableCell CreateCell(string text, bool isHeader = false)
        {
            var cell = new TableCell();
            var paragraph = new Paragraph();
            var run = new Run();
            
            if (isHeader)
            {
                run.AppendChild(new RunProperties(new Bold()));
            }
            
            run.AppendChild(new Text(text));
            paragraph.AppendChild(run);
            cell.AppendChild(paragraph);
            return cell;
        }
    }
}