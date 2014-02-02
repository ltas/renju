using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renju.Net
{
    /// <summary>
    /// Клас, що реалізує логіку гри.
    /// </summary>
    class Game
    {
        #region Private Fields
        private List<Chess> _chesses = new List<Chess>();
        private const int LINE_COUNT = 15;
        public bool _isGameOver = false;        
        private int playersQuantity = 0;
        private Chess.Color _cuurentMove = Chess.Color.Black;

        #endregion        

        #region Private methods
        /// <summary>
        /// Перевірка можливості встановити мітку в заданій яйчейці.
        /// </summary>
        /// <param name="xLoc">x</param>
        /// <param name="yLoc">y</param>
        /// <returns>true || false</returns>
        private bool canPutChess(int xLoc, int yLoc)
        {            
            foreach (Chess ch in this._chesses)
            {
                if ((ch.XLoc - xLoc == 0) && (ch.YLoc - yLoc == 0))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Перевырка чи задана яйчейка належить заданому кольору.
        /// </summary>
        /// <param name="xLoc">x</param>
        /// <param name="yLoc">y</param>
        /// <param name="color">Color</param>
        /// <returns>true || false</returns>
        private bool isExists(int xLoc, int yLoc, Chess.Color color)
        {
            foreach (Chess ch in this._chesses)
            {
                if (ch.XLoc == xLoc && ch.YLoc == yLoc && ch.ChessColor == color)
                    return true;
            }
            return false;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Хід переходить до наступного кольору.
        /// </summary>
        public void swapColor()
        {
            _cuurentMove = Chess.Switch(_cuurentMove, playersQuantity);
        }

        /// <summary>
        /// Повертая поточні заняті яйчеки поля.
        /// </summary>
        /// <returns>Поточні заняті яйчеки поля</returns>
        public List<Chess> getChesses()
        {
            List<Chess> chesses = new List<Chess>();
            foreach (Chess ch in _chesses)
                chesses.Add(ch);
            return chesses;
        }

        /// <summary>
        /// Рестарт гри.
        /// </summary>
        public void reset(int playersCount)
        {
            this._chesses.Clear();            
            this._isGameOver = false;
            this.playersQuantity = playersCount;
        }

        /// <summary>
        /// Поставить фішку на поле.
        /// </summary>
        /// <param name="color">Колір</param>
        /// <param name="xLoc">Х</param>
        /// <param name="yLoc">У</param>
        public bool putChess(Chess.Color color, int xLoc, int yLoc)
        {
            if (color != _cuurentMove)
                return false;
            if (!canPutChess(xLoc, yLoc))
                return false;

            Chess chess = new Chess();
            chess.XLoc = xLoc;
            chess.YLoc = yLoc;
            chess.ChessColor = color;
            this._chesses.Add(chess);
            swapColor();
            _isGameOver = isGameOver();
            return true;
        }

        /// <summary>
        /// Перевірка чи закінчена вже гра
        /// </summary>
        /// <returns></returns>
        public bool isGameOver()
        {
            if (this._chesses.Count < 9)
                return false;

            Chess last = this._chesses[this._chesses.Count - 1];

            int xLoc = last.XLoc;
            int yLoc = last.YLoc;
            int count = 0;

            // vertical
            while (yLoc-- > 0)
            {
                if (isExists(xLoc, yLoc, last.ChessColor))
                {
                    count++;
                    if (4 == count)
                        return true;
                }
                else
                    break;
            }
            yLoc = last.YLoc;
            while (yLoc++ < LINE_COUNT)
            {
                if (isExists(xLoc, yLoc, last.ChessColor))
                {
                    count++;
                    if (4 == count)
                        return true;
                }
                else
                    break;
            }

            // horizontal
            count = 0;
            xLoc = last.XLoc;
            yLoc = last.YLoc;
            while (xLoc-- > 0)
            {
                if (isExists(xLoc, yLoc, last.ChessColor))
                {
                    count++;
                    if (4 == count)
                        return true;
                }
                else
                    break;
            }
            xLoc = last.XLoc;
            while (xLoc++ < LINE_COUNT)
            {
                if (isExists(xLoc, yLoc, last.ChessColor))
                {
                    count++;
                    if (4 == count)
                        return true;
                }
                else
                    break;
            }

            // 1-3 half splitter
            count = 0;
            xLoc = last.XLoc;
            yLoc = last.YLoc;
            while (xLoc++ < LINE_COUNT && yLoc-- > 0)
            {
                if (isExists(xLoc, yLoc, last.ChessColor))
                {
                    count++;
                    if (4 == count)
                        return true;
                }
                else
                    break;
            }
            xLoc = last.XLoc;
            yLoc = last.YLoc;
            while (xLoc-- > 0 && yLoc++ < LINE_COUNT)
            {
                if (isExists(xLoc, yLoc, last.ChessColor))
                {
                    count++;
                    if (4 == count)
                        return true;
                }
                else
                    break;
            }

            // 2-4 half splitter
            count = 0;
            xLoc = last.XLoc;
            yLoc = last.YLoc;
            while (xLoc-- > 0 && yLoc-- > 0)
            {
                if (isExists(xLoc, yLoc, last.ChessColor))
                {
                    count++;
                    if (4 == count)
                        return true;
                }
                else
                    break;
            }
            xLoc = last.XLoc;
            yLoc = last.YLoc;
            while (xLoc++ < LINE_COUNT && yLoc++ < LINE_COUNT)
            {
                if (isExists(xLoc, yLoc, last.ChessColor))
                {
                    count++;
                    if (4 == count)
                        return true;
                }
                else
                    break;
            }

            return false;
        }

        /// <summary>
        /// Збереження поточноъ гри.
        /// </summary>
        /// <param name="p">Шлях та назва гри.</param>
        internal void save(string p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Завантаження гри.
        /// </summary>
        /// <param name="p">Шлях та назва гри.</param>
        internal void load(string p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Відміна ходу.
        /// </summary>
        internal void undo()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Відміна відміни ходу.
        /// </summary>
        internal void redo()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
