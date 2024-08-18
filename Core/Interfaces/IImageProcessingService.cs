using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IImageProcessingService
    {
        /// <summary>
        /// Находит изображение-шаблон на экране.
        /// </summary>
        /// <param name="screen">Изображение экрана.</param>
        /// <param name="template">Изображение-шаблон для поиска.</param>
        /// <returns>Возвращает положение на экране пикчи или Null.</returns>
        Rectangle? FindImage(Bitmap screenBitmap, Bitmap templateBitmap);
    }
}
