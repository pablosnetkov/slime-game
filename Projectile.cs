namespace game
{
    internal class Projectile : IComparable<Projectile>
    {

        private int number;
        public int Number { get { return number; } set { if (value >= 0 && value <= 4) number = value; } }

        public int X;
        public int Y { get; private set; }
        public int Value { get; private set; }
        public int Time { get; private set; }

        /// <summary>
        /// устанавливает порядковый номер снаряда
        /// </summary>
        /// <param name="i">номер снаряда</param>
        public void SetNumber(int i)
        {
            Number = i;
        }

        /// <summary>
        /// инициализация снаряда
        /// </summary>
        public Projectile() 
        {
            Number = -1;
            X = 1920;
            Y = 80;
            Value = 0;
            Time = 0;
        }

        /// <summary>
        /// устанавливает высоту, на которой находится снаряд
        /// </summary>
        public void SetHeight()
        {
            var random = new Random();
            Y = 80 + random.Next(0, 3) * 400;
        }

        //проверяет, не является ли текущая ситуация проигрышной
        public bool CheckIsLose(int[] hps)
        {
            if (hps[0] >= 100 || hps[1] >= 100 || hps[0] <= -100 || hps[1] <= -100) return true;
            return false;
        }

        /// <summary>
        /// устанавливает значение снаряда на основе следующих перед ним так,
        /// чтобы у игрока всегда было определенное количество победных стратегий
        /// </summary>
        /// <param name="h1">значение хитпоинтов первого персонажа</param>
        /// <param name="h2">значение хитпоинтов второго персонажа</param>
        /// <param name="allProjectiles">все снаряды</param>
        public void SetValue(int h1, int h2, List<Projectile> allProjectiles)
        {
            lock (this)
            {
                var flag = 0;

                var random = new Random();

                allProjectiles.Sort();

                var values = Enumerable.Range(-200, 401);

                var randomized = values.OrderBy(item => random.Next());

                foreach (var value in randomized)
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

                                    if (CheckIsLose(hps)) hps[0] = int.MaxValue;

                                    hps[j] += allProjectiles[1].Value;

                                    if (CheckIsLose(hps)) hps[0] = int.MaxValue;

                                    hps[k] += allProjectiles[2].Value;

                                    if (CheckIsLose(hps)) hps[0] = int.MaxValue;

                                    hps[l] += value;

                                    if (hps[1] > -100 && hps[0] < 100 && hps[1] < 100 && hps[0] > -100)
                                        flag += 1;
                                }
                            }

                        }
                    }
                    //проверка количества выигрышных стратегий
                    if (flag <= 10 && flag >= 7)
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
        }

        /// <summary>
        /// устанавливает значение снаряда
        /// </summary>
        /// <param name="value">значение снаряда</param>
        public void SetValue(int value)
        {
            Value = value;
        }

        /// <summary>
        /// реализует сравнение снарядов по порядковому номеру
        /// </summary>
        /// <param name="other">снаряд, с которым производится сравнение</param>
        /// <returns>результат сравнения</returns>
        public int CompareTo(Projectile? other)
        {
            if (other == null) return 1;
            if (other.Number > this.Number) return -1;
            else if (other.Number == this.Number) return 0;
            else return 1;
        }
    }
}
