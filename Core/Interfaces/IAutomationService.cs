using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAutomationService
    {
        /// <summary>
        /// Выполняет клик в указанной точке на экране
        /// </summary>
        /// <param name="x">Координата X на экране</param>
        /// <param name="y">Координата Y на экране</param>
        void ClickAt(int x, int y);

        /// <summary>
        /// Захватывает текущий экран в пикчу
        /// </summary>
        Bitmap CaptureScreenAsBitmap();
        /// <summary>
        /// Рандомный клик в области
        /// </summary>
        void ClickAtRandomInArea(Rectangle area);
        /// <summary>
        /// Случайный таймер
        /// </summary>
        void WaitRandomTime(int minSeconds, int maxSeconds);
    }
}
