using Core.Interfaces;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;
using System.Drawing.Imaging;

public class EmguCvImageProcessingService : IImageProcessingService
{
    public Rectangle? FindImage(Bitmap screenBitmap, Bitmap templateBitmap)
    {
        try
        {
            // Преобразуем Bitmap в Mat
            using Mat screenMat = BitmapToMat(screenBitmap);
            using Mat templateMat = BitmapToMat(templateBitmap);

            // Используем метод матчинг шаблона
            using var result = new Mat();
            CvInvoke.MatchTemplate(screenMat, templateMat, result, TemplateMatchingType.CcoeffNormed);

            // Находим максимальное значение и его позицию
            double minVal = 0, maxVal = 0;
            Point minLoc = new Point(), maxLoc = new Point();
            CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

            // Если совпадение достаточно высоко, возвращаем область совпадения
            if (maxVal > 0.8) // 0.8 - пороговое значение, можно настроить
            {
                // Возвращаем область, в которой найдено совпадение
                return new Rectangle(maxLoc, new Size(templateBitmap.Width, templateBitmap.Height));
            }

            return null; // Если совпадение не найдено
        }
        finally
        {
            screenBitmap.Dispose();
            templateBitmap.Dispose();
        }
    }

    private Mat BitmapToMat(Bitmap bitmap)
    {
        // Преобразование Bitmap в Mat
        // Господи благослови chatGPT
        BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                 ImageLockMode.ReadOnly,
                                                 PixelFormat.Format24bppRgb);

        // Создаем Mat с аналогичными размерами и типом данных
        Mat mat = new Mat(bitmap.Height, bitmap.Width, DepthType.Cv8U, 3);

        // Копируем данные из Bitmap в Mat
        using (Image<Bgr, byte> image = new Image<Bgr, byte>(bitmapData.Width, bitmapData.Height, bitmapData.Stride, bitmapData.Scan0))
        {
            mat = image.Mat.Clone();  // Клонируем Mat для использования
        }

        bitmap.UnlockBits(bitmapData);
        return mat;
    }
}
