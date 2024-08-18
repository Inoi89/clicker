using System;
using System.Drawing;
using Core.Interfaces;

namespace Application.Services
{
    public class BasicDecisionService : IDecisionService
    {
        private readonly IAutomationService _automationService;

        public BasicDecisionService(IAutomationService automationService)
        {
            _automationService = automationService;
        }

        public bool ShouldStartCombat(int heroPower)
        {
            // Заглушка
            return true;
        }

        public void ClickOn(Rectangle foundArea)
        {
            Console.WriteLine("Подготовка к клику по найденной области.");

            // Ждем случайное время перед кликом
            _automationService.WaitRandomTime(1, 3);

            // Кликаем в центр области с небольшим случайным сдвигом
            _automationService.ClickAtRandomInArea(foundArea);

            Console.WriteLine("Клик выполнен.");
        }
    }
}
