using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDecisionService
    {
        /// <summary>
        /// Определяет, следует ли начинать бой, основываясь на силе героя
        /// </summary>
        /// <param name="heroPower">Сила героя.</param>
        /// <returns>Возвращает true, если бой следует начать, иначе false.</returns>
        bool ShouldStartCombat(int heroPower);
        /// <summary>
        /// Клик по заданной области с паузой и рандомным сдвигом
        /// </summary>
        void ClickOn(Rectangle foundArea);
    }
}

