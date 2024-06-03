using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    internal class Player
    {
        //время движения игрока
        public int MovingTime { get; private set; }
        //движется ли игрок
        public bool Moving;
        //очки игрока
        public int Score { get; private set; }
        //количество шагов игрока, которое нужно пройти для перемещения
        public int Steps { get; private set; }
        //координаты x и y игрока
        public int X, Y;
        //поменяны ли местами персонажи
        public bool Switch;
        //определяет, на каком кадре находится анимация переключения
        public int SwitchFrame { get; private set; }
        //текущая позиция игрока
        Positions Position;

        // обнуляет количество шагов
        public void ResetSteps()
        {
            Steps = 0;
        }

        //увеличивает время передвижения на 1
        public void IncereaseMovingTime()
        {
            MovingTime += 1;
        }

        //увеличивает очки на 1
        public void IncreaseScore()
        {
            Score += 1;
        }

        //сбрасывает время передвижения до нуля
        public void ResetMovingTime()
        {
            MovingTime = 0;
        }

        //сбрасывает номер кадра переключения анимации
        public void ResetSwithFrame()
        {
            SwitchFrame = 0;
        }

        //увеличивает номер кадра анимации
        public void IncreaseSwitchFrame()
        {
            SwitchFrame += 1;
        }

        //сбрасывает номер кадра анимации до дефолтного (-1)
        public void DecreaseSwitchFrame()
        {
            SwitchFrame = -1;
        }

        /// <summary>
        /// инициализирует игрока
        /// </summary>
        public Player()
        {
            Score = 0;
            Steps = 0;
            Switch = false;
            SwitchFrame = -1;
            Position = Positions.Center;
        }

        /// <summary>
        /// устанавливает стандартные значения переменных для игрока
        /// </summary>
        public void SetDefault()
        {
            Score = 0;
            Steps = 0;
            Switch = false;
            SwitchFrame = -1;
            Position = Positions.Center;
        }

        //значения хитпоинтов игрока
        public int Hp1 { get; set; }
        public int Hp2 { get; set; }

        /// <summary>
        /// осуществляет вычисление количества шагов, необходимых для перемещения в заданную позицию
        /// </summary>
        /// <param name="direction"></param>
        public void Move (Directions direction)
        {
            if (direction == Directions.Up && Position == Positions.Bottom)
            {
                Position = Positions.Center;
                Steps = -80;
            }
            else if (direction == Directions.Up && Position == Positions.Center)
            {
                Position = Positions.Top;
                Steps = -80;
            }
            else if (direction == Directions.Down && Position == Positions.Top)
            {
                Position = Positions.Center;
                Steps = 80;
            }
            else if (direction == Directions.Down && Position == Positions.Center)
            {
                Position = Positions.Bottom;
                Steps = 80;
            }
            else Steps = 0;
        }

    }
}
