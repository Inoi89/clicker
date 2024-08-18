using System.Drawing;

namespace Core.Interfaces
{
    public interface IOcrService
    {
        string RecognizeText(Bitmap image);
        string CaptureAndRecognizeText(IAutomationService automationService, Rectangle? foundArea, int offsetX, int offsetY);
    }
}
