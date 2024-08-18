using Tesseract;
using System;
using System.Drawing;
using Core.Interfaces;
using System.Text.RegularExpressions;

public class TesseractOcrService : IOcrService
{
    public string RecognizeText(Bitmap image)
    {
        // Логгирование получения изображения
        Console.WriteLine("Image received for OCR processing.");

        string tessDataPath = @"./tessdata";

        try
        {
            // Преобразуем Bitmap в Pix
            using var pixImage = PixConverter.ToPix(image);
            Console.WriteLine("Image successfully converted to Pix format.");

            // Инициализируем движок Tesseract
            using var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default);
            Console.WriteLine("Tesseract engine initialized.");

            // Запускаем распознавание текста
            using var page = engine.Process(pixImage);
            Console.WriteLine("Text recognition started.");

            string recognizedText = page.GetText();

            // Очищаем текст от говна перед возвратом
            string cleanedText = CleanText(recognizedText);

            Console.WriteLine($"Recognition completed. Recognized text: '{cleanedText}'");
            return cleanedText;
        }
        catch (Exception ex)
        {
            // Логгирование ошибок
            Console.WriteLine($"Error during OCR processing: {ex.Message}");
            return string.Empty;
        }
    }

    public string CaptureAndRecognizeText(IAutomationService automationService, Rectangle? foundArea, int offsetX, int offsetY)
    {
        if (!foundArea.HasValue)
        {
            Console.WriteLine("Шаблон не найден, распознавание текста не выполнено.");
            return string.Empty;
        }

        // Захватываем экран как Bitmap
        using var screenBitmap = automationService.CaptureScreenAsBitmap();

        // Смещаем область на заданные значения
        Rectangle adjustedArea = new Rectangle(
            foundArea.Value.X + offsetX,
            foundArea.Value.Y + offsetY,
            foundArea.Value.Width,
            foundArea.Value.Height
        );

        // Вырезаем область совпадения из полного скриншота
        using var foundAreaBitmap = CropBitmap(screenBitmap, adjustedArea);

        // Выполняем OCR на найденной области
        return RecognizeText(foundAreaBitmap);
    }

    private string CleanText(string text)
    {
        // Убираем пробелы, переносы строк и специальные символы
        text = Regex.Replace(text, @"[\r\n\t]", ""); // Убираем переносы строк и табуляции
        text = text.Replace(" ", ""); // Убираем пробелы
        text = Regex.Replace(text, @"[^\w\d]", ""); // Убираем все, кроме букв и цифр

        return text;
    }

    private Bitmap CropBitmap(Bitmap source, Rectangle section)
    {
        // Вырезаем указанную область из полного изображения
        Bitmap croppedBitmap = new Bitmap(section.Width, section.Height);
        using (Graphics g = Graphics.FromImage(croppedBitmap))
        {
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
        }
        return croppedBitmap;
    }

}
