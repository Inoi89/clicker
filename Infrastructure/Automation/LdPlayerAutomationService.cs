using Core.Interfaces;
using System;
using System.Drawing;

namespace Infrastructure.Automation
{
    // Изначально задумывал как подкласс для LD, но как будто бы пока просто юзаю фулскрин и похуй
    public class LdPlayerAutomationService : BaseAutomationService
    {
        public override Bitmap CaptureScreenAsBitmap()
        {
            // Логика захвата экрана в LDPlayer
            Console.WriteLine("LDPlayer Screen captured.");
            // Верни Bitmap с экрана LDPlayer (пока заглушка)
            return new Bitmap("path_to_ldplayer_screenshot.bmp");
        }
    }
}
