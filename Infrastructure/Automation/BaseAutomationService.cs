using Core.Interfaces;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

public abstract class BaseAutomationService : IAutomationService
{
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;

    public virtual void ClickAt(int x, int y)
    {
        // Общая логика для клика по экрану
        // Перемещаем курсор на указанные координаты
        SetCursorPos(x, y);
        // Выполняем клик левой кнопкой мыши
        mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, UIntPtr.Zero);
        mouse_event(MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, UIntPtr.Zero);

        Console.WriteLine($"Clicked at coordinates ({x}, {y})");
    }
    public abstract Bitmap CaptureScreenAsBitmap();

    public void ClickAtRandomInArea(Rectangle area)
    {
        // Вычисляем центр области
        int centerX = area.Left + area.Width / 2;
        int centerY = area.Top + area.Height / 2;

        // Добавляем случайный сдвиг
        Random random = new Random();
        int randomOffsetX = random.Next(-2, 3); // Сдвиг от -2 до 2 пикселей
        int randomOffsetY = random.Next(-2, 3); // Сдвиг от -2 до 2 пикселей

        int finalX = centerX + randomOffsetX;
        int finalY = centerY + randomOffsetY;

        // Выполняем клик в рассчитанных координатах
        ClickAt(finalX, finalY);
    }

    public void WaitRandomTime(int minSeconds, int maxSeconds)
    {
        Random random = new Random();
        int waitTime = random.Next(minSeconds * 1000, maxSeconds * 1000); // В миллисекундах
        Console.WriteLine($"Waiting for {waitTime / 1000.0} seconds.");
        Thread.Sleep(waitTime);
    }
}
