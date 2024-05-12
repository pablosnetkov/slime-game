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
        public int MovingTime;
        public bool Moving;
        public int Score = 0;
        public int Steps = 0;
        public int X, Y;
        public bool Switch = false;
        public int SwitchFrame = -1;
        Positions Position = Positions.Center;
        public int Hp1 { get; set; }
        public int Hp2 { get; set; }

        public void GetDamage(int damage)
        {
            Hp1 -= damage;
            if (Hp1 < 0) Hp1 = 0;
        }
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
