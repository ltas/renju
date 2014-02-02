using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renju.Net
{
    /// <summary>
    /// Клас для робот з фігурами дошки.
    /// </summary>
    class Chess
    {
        public enum Color { Black, White, Blue, Green };

        public Color ChessColor { get; set; }

        public int XLoc { get; set; }

        public int YLoc { get; set; }

        /// <summary>
        /// Перехід до наступного кольору.
        /// </summary>
        /// <param name="currentColor">Поточний колір.</param>
        /// <param name="quantity">Кількість гравців.</param>
        /// <returns>Колір поточного ходу.</returns>
        public static Color Switch(Color currentColor, int quantity)
        {
            switch (currentColor)
            {
                case Color.Black:
                    return Color.White;
                case Color.White:
                    if (quantity > 2)
                        return Color.Blue;
                    else
                        return Color.Black;
                case Color.Blue:
                    if (quantity == 4)
                        return Color.Green;
                    else
                        return Color.Black;
                case Color.Green:
                    return Color.Black;
                default:
                    return Color.Black;
            }       
        }
    }
}
