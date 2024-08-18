using Application.Services;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Drawing;
using UI.UIElements;

namespace Application.Steps
{
    public class ClickUiElementStep : FindUiElementStep
    {
        private readonly IDecisionService _decisionService;

        public ClickUiElementStep(IAutomationService automationService, UiElement uiElement, ScreenScanner screenScanner, IDecisionService decisionService, ILogger<FindUiElementStep> logger)
            : base(automationService, uiElement, screenScanner, logger)
        {
            _decisionService = decisionService;
        }

        protected override void OnElementFound(Rectangle foundArea)
        {
            _logger.LogInformation($"Выполняю клик на {_uiElement.Name}.");
            _decisionService.ClickOn(foundArea);
        }
    }
}
