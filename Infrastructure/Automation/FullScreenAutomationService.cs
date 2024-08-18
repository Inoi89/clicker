using System.Drawing;
using System.Runtime.InteropServices;

public class FullScreenAutomationService : BaseAutomationService
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetDesktopWindow();

    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
                                      IntPtr hdcSrc, int nXSrc, int nYSrc, CopyPixelOperation dwRop);

    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);

    [DllImport("user32.dll")]
    private static extern bool ReleaseDC(IntPtr hWnd, IntPtr hdc);

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    private const int SM_XVIRTUALSCREEN = 76;
    private const int SM_YVIRTUALSCREEN = 77;
    private const int SM_CXVIRTUALSCREEN = 78;
    private const int SM_CYVIRTUALSCREEN = 79;

    public override Bitmap CaptureScreenAsBitmap()
    {
        // Получаем размеры виртуального рабочего стола, который охватывает все мониторы
        Rectangle bounds = GetVirtualScreenBounds();

        IntPtr hWnd = GetDesktopWindow();
        IntPtr hdcSrc = GetWindowDC(hWnd);
        IntPtr hdcDest = CreateCompatibleDC(hdcSrc);

        IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, bounds.Width, bounds.Height);
        IntPtr hOld = SelectObject(hdcDest, hBitmap);
        BitBlt(hdcDest, 0, 0, bounds.Width, bounds.Height, hdcSrc, bounds.Left, bounds.Top, CopyPixelOperation.SourceCopy);
        Bitmap bitmap = Image.FromHbitmap(hBitmap);
        SelectObject(hdcDest, hOld);
        DeleteObject(hBitmap);
        ReleaseDC(hWnd, hdcSrc);

        return bitmap;
    }

    private Rectangle GetVirtualScreenBounds()
    {
        // Используем GetSystemMetrics для получения размеров виртуального экрана
        int x = GetSystemMetrics(SM_XVIRTUALSCREEN);
        int y = GetSystemMetrics(SM_YVIRTUALSCREEN);
        int width = GetSystemMetrics(SM_CXVIRTUALSCREEN);
        int height = GetSystemMetrics(SM_CYVIRTUALSCREEN);

        return new Rectangle(x, y, width, height);
    }
}
