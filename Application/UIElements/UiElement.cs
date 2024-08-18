using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.UIElements
{
    public class UiElement
    {
        /// <summary>
        /// Название элемента.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Путь к BMP файлу, который используется для поиска элемента на экране.
        /// </summary>
        public string BmpPath { get; set; }

        /// <summary>
        /// Флаг, указывающий, требуется ли анализ текста на найденной области.
        /// </summary>
        public bool RequiresTextAnalysis { get; set; }

        /// <summary>
        /// Смещение по оси X для области, в которой будет выполняться анализ текста.
        /// </summary>
        public int OffsetX { get; set; }

        /// <summary>
        /// Смещение по оси Y для области, в которой будет выполняться анализ текста.
        /// </summary>
        public int OffsetY { get; set; }

        /// <summary>
        /// Конструктор для инициализации элемента интерфейса.
        /// <param name="name">Название элемента.</param>
        /// <param name="bmpPath">Путь к BMP файлу.</param>
        /// <param name="requiresTextAnalysis">Флаг, указывающий на необходимость анализа текста.</param>
        /// <param name="offsetX">Смещение по оси X.</param>
        /// <param name="offsetY">Смещение по оси Y.</param>
        /// </summary>
        public UiElement(string name, string bmpPath, bool requiresTextAnalysis = false, int offsetX = 0, int offsetY = 0)
        {
            Name = name;
            BmpPath = bmpPath;
            RequiresTextAnalysis = requiresTextAnalysis;
            OffsetX = offsetX;
            OffsetY = offsetY;
        }
    }
}
