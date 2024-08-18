using Application.Services;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Drawing;
using UI.UIElements;

namespace Application.Steps
{
    public class CheckUiElementStep : FindUiElementStep
    {
        public bool IsElementFound { get; private set; }

        public CheckUiElementStep(IAutomationService automationService, UiElement uiElement, ScreenScanner screenScanner, ILogger<FindUiElementStep> logger)
            : base(automationService, uiElement, screenScanner, logger)
        {
        }

        protected override void OnElementFound(Rectangle foundArea)
        {
            _logger.LogInformation($"{_uiElement.Name} найдено.");
            IsElementFound = true;
        }

        protected override void OnElementNotFound()
        {
            _logger.LogWarning($"{_uiElement.Name} не найдено.");
            IsElementFound = false;
        }
    }
}
