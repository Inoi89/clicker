using Application.Services;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.UIElements;

namespace Application.Steps
{
    public class OcrAnalysisStep : BaseStep
    {
        private readonly IOcrService _ocrService;
        private readonly IAutomationService _automationService;
        private readonly UiElement _uiElement;
        private readonly ScreenScanner _screenScanner;

        public OcrAnalysisStep(IOcrService ocrService, IAutomationService automationService, UiElement uiElement, ScreenScanner screenScanner)
        {
            _ocrService = ocrService;
            _automationService = automationService;
            _uiElement = uiElement;
            _screenScanner = screenScanner;
        }

        public override void Execute()
        {
            // Захват экрана и поиск элемента
            Rectangle? foundArea = _screenScanner.FindElementOnScreen(_uiElement.BmpPath);

            if (foundArea.HasValue)
            {
                // Выполняем анализ текста на найденной области
                var recognizedText = _ocrService.CaptureAndRecognizeText(_automationService, foundArea, _uiElement.OffsetX, _uiElement.OffsetY);
                Console.WriteLine($"Распознанный текст: {recognizedText}");
                IsCompleted = true;
            }
            else
            {
                Console.WriteLine($"{_uiElement.Name} не найдено.");
            }
        }
    }
}
