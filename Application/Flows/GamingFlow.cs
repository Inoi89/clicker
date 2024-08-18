using Application.Services;
using Application.Steps;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.UIElements;

namespace Application.Flows
{
    public class GamingFlow : BaseFlow
    {
        public GamingFlow(IAutomationService automationService, ScreenScanner screenScanner, IDecisionService decisionService, UiElementManager uiElementManager,
            ILogger<GamingFlow> logger,  // Логгер для потока
            ILogger<ClickUiElementStep> stepLogger) // И для шага ещё
        {
            // Dev: находим элемент "arena_init"
            // Эту хуету конечно нужно поумнее написать
            // Мб перебором шагов, заготовленных в конфиге?
            var arenaButtonElement = uiElementManager.GetElementByName("arena_init");
            var forest_button = uiElementManager.GetElementByName("forest_button");
            var bg_button = uiElementManager.GetElementByName("bg_button");
            var bg_open_button = uiElementManager.GetElementByName("bg_open_button");
            var bg_init_button = uiElementManager.GetElementByName("bg_init_button");

            // Шаги в потоке
            Steps.Add(new CheckUiElementStep(automationService, arenaButtonElement, screenScanner, stepLogger));
            Steps.Add(new ClickUiElementStep(automationService, forest_button, screenScanner, decisionService, stepLogger));
            Steps.Add(new ClickUiElementStep(automationService, bg_button, screenScanner, decisionService, stepLogger));
            Steps.Add(new ClickUiElementStep(automationService, bg_open_button, screenScanner, decisionService, stepLogger));
            Steps.Add(new ClickUiElementStep(automationService, bg_init_button, screenScanner, decisionService, stepLogger));
        }
    }
}
