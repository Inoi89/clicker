using System.Drawing;
using Core.Interfaces;

namespace Application.Services
{
    public class ScreenScanner
    {
        private readonly IAutomationService _automationService;
        private readonly IImageProcessingService _imageProcessingService;

        public ScreenScanner(IAutomationService automationService, IImageProcessingService imageProcessingService)
        {
            _automationService = automationService;
            _imageProcessingService = imageProcessingService;
        }

        public Rectangle? FindElementOnScreen(string templatePath)
        {
            if (!File.Exists(templatePath))
            {
                Console.WriteLine($"Файл {templatePath} не найден.");
                return null;
            }
            // Загружаем изображение шаблона как Bitmap
            using var templateBitmap = new Bitmap(templatePath);

            // Захватываем экран как Bitmap
            using var screenBitmap = _automationService.CaptureScreenAsBitmap();

            // Ищем шаблон на экране и получаем область совпадения
            return _imageProcessingService.FindImage(screenBitmap, templateBitmap);
        }
    }
}
