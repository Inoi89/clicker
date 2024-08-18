using Application.Services;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using UI.UIElements;

namespace Application.Steps
{
    public abstract class FindUiElementStep : BaseStep
    {
        protected readonly IAutomationService _automationService;
        protected readonly UiElement _uiElement;
        protected readonly ScreenScanner _screenScanner;
        protected readonly ILogger<FindUiElementStep> _logger;

        protected FindUiElementStep(IAutomationService automationService, UiElement uiElement, ScreenScanner screenScanner, ILogger<FindUiElementStep> logger)
        {
            _automationService = automationService;
            _uiElement = uiElement;
            _screenScanner = screenScanner;
            _logger = logger;
        }

        public override void Execute()
        {
            // Если не паузить - слишком рано начинает искать следующий элемент в шагах, он просто не успеет прогрузиться
            // Хз тут паузить или между степами, разницы технически нет
            _automationService.WaitRandomTime(1, 2);

            // Общая логика поиска элемента
            Rectangle? foundArea = _screenScanner.FindElementOnScreen(_uiElement.BmpPath);

            if (foundArea.HasValue)
            {
                _logger.LogInformation($"{_uiElement.Name} найдено.");
                OnElementFound(foundArea.Value);
                IsCompleted = true;
            }
            else
            {
                _logger.LogWarning($"{_uiElement.Name} не найдено.");
                OnElementNotFound();
            }
        }

        // Абстрактный класс, потому что там один хуй почти одно и то же
        protected abstract void OnElementFound(Rectangle foundArea);
        protected virtual void OnElementNotFound() { }
    }
}
