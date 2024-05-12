using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    internal class Projectile : IComparable<Projectile>
    {
        public bool InProgress = true;
        public bool Lose = false;
        public int Number = -1;
        public int X = 1920; 
        public int Y = 80;
        public int Value = 0;
        public int Time = 0;
        public int AttackTimer;
        public bool Attack;

        public void SetHeight()
        {
            var random = new Random();
            Y = 80 + random.Next(0, 3) * 400;
        }

        public void SetAttackTimer() { AttackTimer = 100; }

        public void SetValue(int h1, int h2, List<Projectile> allProjectiles)
        {
            var mx = 0;
            var flag = 0;

            var random = new Random();

            allProjectiles.Sort();

            var values = Enumerable.Range(-200, 401);

            var randomized = values.OrderBy(item => random.Next());

            foreach(var value in randomized)
            {
                flag = 0;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            for (int l = 0; l < 2; l++)
                            {
                                var hps = new[] { h1, h2 };

                                hps[i] += allProjectiles[0].Value;

                                if (hps[0] >= 100 || hps[1] >= 100 || hps[0] <= -100 || hps[1] <= -100) hps[0] = 10000;

                                hps[j] += allProjectiles[1].Value;

                                if (hps[0] >= 100 || hps[1] >= 100 || hps[0] <= -100 || hps[1] <= -100) hps[0] = 10000;

                                hps[k] += allProjectiles[2].Value;

                                if (hps[0] >= 100 || hps[1] >= 100 || hps[0] <= -100 || hps[1] <= -100) hps[0] = 10000;

                                hps[l] += value;

                                if (hps[1] > -100 && hps[0] < 100 && hps[1] < 100 && hps[0] > -100)
                                    flag += 1;
                            }
                        }
                        
                    }
                }
                if (flag <= 8 && flag >= 5)
                {
                    Value = value;
                    break;
                }   
                if (flag > 0)
                {
                    Value = value;
                }
                flag = 0;
            }
        }

        public void SetValue(int value)
        {
            Value = value;
        }

        public int CompareTo(Projectile? other)
        {
            if (other.Number > this.Number) return -1;
            else if (other.Number == this.Number) return 0;
            else return 1;
        }
    }
}
