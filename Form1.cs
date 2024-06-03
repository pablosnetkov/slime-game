using System;
using System.Drawing;


namespace game
{
    /// <summary>
    /// реализует модель игры
    /// </summary>
    internal class GameModel
    {
        //сложность игры
        public int Difficulty { get; private set; }
        //находится ли пользователь в игре
        public bool InGame { get; private set; }
        //кнопка минимальной сложности
        public Button EasyDifficultyButton { get; private set; }
        //кнопка средней сложности
        public Button MediumDifficultyButton { get; private set; }
        //кнопка минимальной сложности
        public Button HardDifficultyButton { get; private set; }
        //снаряды
        public List<Projectile> Projectiles { get; private set; }
        //персонаж (игрок)
        public Player Plr { get; private set; }
        //смещение по X у изображений игровых персонажей (предназначено для обмена их местами)
        public int Dx { get; private set; }
        //время между кадрами
        public int FrameTime { get; private set; }
        //номер кадра
        public int Frame { get; private set; }
        //буффер нажатых кнопок
        public Keys KeysBuffer;

        /// <summary>
        /// инициализирует модель игры
        /// </summary>
        public GameModel()
        {
            EasyDifficultyButton = new Button();
            MediumDifficultyButton = new Button();
            HardDifficultyButton = new Button();
            Plr = new Player();
            Projectiles = new List<Projectile>();
            MakeNewGame();
            SetPlayer();
            SetProjectiles();
            UpdateProjectiles();
        }

        /// <summary>
        /// сбрасывает таймер для кадров анимаций
        /// </summary>
        public void ResetFrameTimer()
        {
            FrameTime = 0;
        }

        /// <summary>
        /// увеличивает на один таймер для кадров анимаций
        /// </summary>
        public void IncreaseFrameTimer()
        {
            FrameTime++;
        }

        /// <summary>
        /// сбрасывает номер текущего кадра
        /// </summary>
        public void ResetFrames()
        {
            Frame = 0;
        }

        /// <summary>
        /// увеличиваеn номер текущего кадра
        /// </summary>
        public void IncreaseFrames()
        {
            Frame++;
        }

        /// <summary>
        /// задает значение "в игре"
        /// </summary>
        public void SetInGame() 
        { 
            InGame = true;
        }

        /// <summary>
        /// выключает значение "в игре"
        /// </summary>
        public void UnsetInGame() 
        {
            InGame = false;
        }

        /// <summary>
        /// устанавливает минимальную сложность
        /// </summary>
        public void SetEasyDifficulty()
        {
            Difficulty = 5;
        }

        /// <summary>
        /// устанавливает среднюю сложность
        /// </summary>
        public void SetMediumDifficulty()
        {
            Difficulty = 10;
        }

        /// <summary>
        /// устанавливает максимальную сложность
        /// </summary>
        public void SetHardDifficulty()
        {
            Difficulty = 15;
        }

        /// <summary>
        /// меняет местами персонажей
        /// </summary>
        public void SwitchCharacter()
        {
            if (Plr.Switch) Dx = 0;
            else Dx = -150;
        }

        /// <summary>
        /// создает кнопку
        /// </summary>
        /// <param name="x">координата x кнопки</param>
        /// <param name="y">координата y кнопки</param>
        /// <param name="button">кнопка</param>
        private void MakeButton(int x, int y, Button button)
        {
            button.Location = new Point(x, y);
            button.Size = new Size(1000, 100);
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Color.Transparent;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.FlatAppearance.BorderSize = 0;
        }

        /// <summary>
        /// создает кнопку минимальной сложности
        /// </summary>
        /// <param name="x">координата x кнопки</param>
        /// <param name="y">координата y кнопки</param>
        /// <returns>кнопка минимальной сложности</returns>
        public Button GetEasyButton(int x, int y)
        {
            MakeButton(x, y, EasyDifficultyButton);
            return EasyDifficultyButton;
        }

        /// <summary>
        /// создает кнопку средней сложности
        /// </summary>
        /// <param name="x">координата x кнопки</param>
        /// <param name="y">координата y кнопки</param>
        /// <returns>кнопка средней сложности</returns>
        public Button GetMediumButton(int x, int y)
        {
            MakeButton(x, y, MediumDifficultyButton);
            return MediumDifficultyButton;
        }

        /// <summary>
        /// создает кнопку максимальной сложности
        /// </summary>
        /// <param name="x">координата x кнопки</param>
        /// <param name="y">координата y кнопки</param>
        /// <returns>кнопка максимальной сложности</returns>
        public Button GetHardButton(int x, int y)
        {
            MakeButton(x, y, HardDifficultyButton);
            return HardDifficultyButton;
        }

        /// <summary>
        /// обновляет снаряды
        /// </summary>
        public void UpdateProjectiles()
        {
            var i = 0;
            foreach (var proj in Projectiles)
            {
                proj.Number = i;
                proj.X = 1900 + 480 * i;
                proj.SetHeight();
                proj.SetValue(0);
                i++;
            }
        }

        /// <summary>
        /// создает снаряды
        /// </summary>
        public void SetProjectiles()
        {
            for (int i = 0; i < 4; i++)
            {
                var proj = new Projectile();
                Projectiles.Add(proj);
            }
        }

        /// <summary>
        /// создает игрока
        /// </summary>
        public void SetPlayer()
        {
            Plr.SetDefault();
            var random = new Random();
            Plr.Hp1 = random.Next(-99, 99);
            Plr.Hp2 = random.Next(-99, 99);
            Plr.Moving = false;
            Plr.X = 200;
            Plr.Y = 480;
        }

        /// <summary>
        /// обнуляет значения моделей игры
        /// </summary>
        public void MakeNewGame()
        {
            InGame = false;
            Dx = -150;
            FrameTime = 0;
            Frame = 0;
            SetPlayer();
            UpdateProjectiles();
            InGame = false;
        }
    }

    /// <summary>
    /// реализует отображение окна с игрой
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly Bitmap backgroundBitmap = new("UI/images/spritesheet.png");
        private readonly Bitmap slimeDownBitmap = new("UI/images/slimeDown.png");
        private readonly Bitmap slimeUpBitmap = new("UI/images/slimeUp.png");
        private readonly Bitmap slimeDownGreenBitmap = new("UI/images/slimeDownGreen.png");
        private readonly Bitmap slimeUpGreenBitmap = new("UI/images/slimeUpGreen.png");
        private readonly Bitmap attackSprite = new("UI/images/attackSprite.png");
        private readonly Bitmap portalSprite = new("UI/images/portalSprite.png");

        GameModel GModel = new GameModel();
        
        /// <summary>
        /// окно с игрой
        /// </summary>
        public Form1()
        {
            var PauseFlag = false;

            Brush brush1;
            Brush brush2;
            DoubleBuffered = true;
            ClientSize = new Size(1920, 1080);
            System.Drawing.Text.PrivateFontCollection f = new System.Drawing.Text.PrivateFontCollection();
            f.AddFontFile("UI/EpilepsySans.ttf");
            var newFont = new Font(f.Families[0], 50);
            var menuFont = new Font(f.Families[0], 30);
            var nameFont = new Font(f.Families[0], 100);
            var instructionsFont = new Font(f.Families[0], 20);
            var projectileFont = new Font(f.Families[0], 40);
            var scoreFont = new Font(f.Families[0], 50);
            var attentionFont = new Font(f.Families[0], 30);

            var frameTimer = new System.Windows.Forms.Timer();
            frameTimer.Interval = 1;
            var menuFrameTimer = new System.Windows.Forms.Timer();
            menuFrameTimer.Interval = 500;

            menuFrameTimer.Start();

            menuFrameTimer.Tick += (sender, args) =>
            {
                GModel.IncreaseFrames();
                if (GModel.Frame == 9)
                {
                    GModel.ResetFrames();
                }
                Invalidate();
            };

            //срабатывание анимации смены переменных игрока
            frameTimer.Tick += (sender, args) =>
            {
                GModel.IncreaseFrameTimer();
                if (GModel.FrameTime == 15)
                {
                    GModel.IncreaseFrames();
                    if (GModel.Frame == 48) GModel.ResetFrames();
                    GModel.ResetFrameTimer();
                }

                if (GModel.Plr.SwitchFrame == 7)
                {
                    //player.Switch = false;
                    GModel.Plr.DecreaseSwitchFrame();
                }
                else if (GModel.Plr.SwitchFrame >= 0)
                    GModel.Plr.IncreaseSwitchFrame();
                
                if (GModel.Plr.MovingTime == 5)
                {
                    GModel.Plr.ResetSteps();
                    GModel.Plr.Moving = false;
                }

                GModel.Plr.Y += GModel.Plr.Steps;
                GModel.Plr.IncereaseMovingTime();


                for (var i = 0; i < 4; i ++)
                {
                    GModel.Projectiles[i].X -= GModel.Difficulty;
                    if (GModel.Projectiles[i].X <= 0)
                    {
                        frameTimer.Stop();
                        GModel.MakeNewGame();
                        MessageBox.Show("Вы проиграли!");
                        menuFrameTimer.Start();
                        Invalidate();
                    }
                    if (GModel.Projectiles[i].X < GModel.Plr.X + 20 && GModel.Projectiles[i].Y == GModel.Plr.Y)
                    {
                        GModel.Plr.IncreaseScore();
                        GModel.Plr.Hp1 += GModel.Projectiles[i].Value;
                        if (GModel.Plr.Hp1 > 100 || GModel.Plr.Hp1 < -100)
                        {
                            frameTimer.Stop();
                            GModel.MakeNewGame();
                            MessageBox.Show("Вы проиграли!");
                            menuFrameTimer.Start();
                            Invalidate();
                            break;
                        }
                        
                        foreach (var item in GModel.Projectiles)
                        {
                            if (item.Number == 3) GModel.Projectiles[i].X = item.X + 480;
                        }
                        GModel.Projectiles[i].SetHeight();
                        
                        GModel.Projectiles[i].Number = 4;
                        foreach (var p in GModel.Projectiles)
                        {
                            p.Number--;
                        }

                        GModel.Projectiles[i].SetValue(GModel.Plr.Hp1, GModel.Plr.Hp2, GModel.Projectiles);

                    }
                }

                //вызывает проверку буфера
                CheckBuffer();
                //обновляет графику
                Invalidate();
            };


            //отрисовка объектов
            Paint += (sender, args) =>
            {
                //отрисовывет игру, если игрок находится в ней
                if (GModel.InGame)
                {
                    //перемещение значений hp и изменение их цвета
                    if (GModel.Plr.Switch)
                    {
                        GModel.SwitchCharacter();
                        brush1 = Brushes.Purple;
                        brush2 = Brushes.MediumBlue;
                    }
                    else
                    {
                        brush2 = Brushes.Purple;
                        brush1 = Brushes.MediumBlue;
                        GModel.SwitchCharacter();
                    }

                    //рисует фон
                    args.Graphics.DrawImage(backgroundBitmap, 0, 0, new Rectangle(GModel.Frame % 9 * 1920, 0, 1920, 1080), GraphicsUnit.Pixel);

                    //отрисовка снарядов
                    foreach (var projectile in GModel.Projectiles)
                    {
                        args.Graphics.DrawImage(attackSprite, projectile.X - 15, projectile.Y - 60, new Rectangle(GModel.Frame % 6 * 200, 0, 200, 170), GraphicsUnit.Pixel);

                        args.Graphics.DrawString(projectile.Value.ToString(), projectileFont, Brushes.Red, projectile.X, projectile.Y);

                    }

                    //отрисовка слаймов с анимацией
                    if (GModel.Frame % 2 == 0)
                    {
                        args.Graphics.DrawImage(slimeUpBitmap, GModel.Plr.X - 25 + GModel.Dx, GModel.Plr.Y - 25);
                        args.Graphics.DrawImage(slimeDownGreenBitmap, GModel.Plr.X - 175 - GModel.Dx, GModel.Plr.Y - 25);
                    }
                    else
                    {
                        args.Graphics.DrawImage(slimeDownBitmap, GModel.Plr.X - 25 + GModel.Dx, GModel.Plr.Y - 25);
                        args.Graphics.DrawImage(slimeUpGreenBitmap, GModel.Plr.X - 175 - GModel.Dx, GModel.Plr.Y - 25);
                    }

                    //вывод значений хитпоинтов игрока
                    args.Graphics.DrawString(GModel.Plr.Hp2.ToString(), projectileFont, brush1, GModel.Plr.X - 150, GModel.Plr.Y);
                    args.Graphics.DrawString(GModel.Plr.Hp1.ToString(), projectileFont, brush2, GModel.Plr.X, GModel.Plr.Y);

                    //вывод количество очков игрока
                    args.Graphics.DrawString("Ваши очки: " + GModel.Plr.Score.ToString(), scoreFont, Brushes.Gold, 0, 0);

                    //окно с объявлением
                    //args.Graphics.DrawString("Ваши числа должны быть больше -100, но меньше 100!", newFont, Brushes.Gold, 350, 0);

                    //анимация портала
                    if (GModel.Plr.SwitchFrame >= 0)
                    {
                        args.Graphics.DrawImage(portalSprite, GModel.Plr.X - 40, GModel.Plr.Y - 40, new Rectangle(GModel.Plr.SwitchFrame % 6 * 150, 0, 150, 150), GraphicsUnit.Pixel);
                        args.Graphics.DrawImage(portalSprite, GModel.Plr.X - 190, GModel.Plr.Y - 40, new Rectangle(GModel.Plr.SwitchFrame % 6 * 150, 0, 150, 150), GraphicsUnit.Pixel);
                    }
                }
                else
                {
                    //отрисовка меню
                    args.Graphics.DrawImage(backgroundBitmap, 0, 0, new Rectangle(GModel.Frame % 9 * 1920, 0, 1920, 1080), GraphicsUnit.Pixel);
                    args.Graphics.DrawString("SLIME BROTHERS", nameFont, Brushes.DeepPink, -15, 65);
                    args.Graphics.DrawString("W - сдвинуться вверх\nS - сдвинуться вниз\nE - поменять слизней местами\nP - пауза\nM - меню\nESC - выйти в меню, если в игре,\nвыйти из программы, если в меню\n\nВ вашу сторону летят снаряды\nс некоторыми значениями.\n" +
                        "Они либо прибавляются,\nлибо вычитаются из значения\nближайшего к снаряду слизня.\n" +
                        "Если значение одного из ваших слизней\nстанет больше 100\nили меньше -100, -вы проиграете.\nЕсли вы пропустите снаряд, вы проиграете.\nВ игре не может быть ситуаций," +
                        "\nкогда вы проигрываете независимо\nот ваших действий.\nЧтобы не проиграть, \nвам нужно рассчитывать значения заранее.\n\nЧтобы начать игру,\nнажмите на один из вариантов слева.", menuFont, Brushes.Cyan, 1100, 85);
                    args.Graphics.DrawString("Начать на легкой сложности", newFont, Brushes.Gold, 0, 270);
                    args.Graphics.DrawString("Начать на средней сложности", newFont, Brushes.Gold, 0, 370);
                    args.Graphics.DrawString("Начать на максимальной сложности", newFont, Brushes.Gold, 0, 470);
                    
                    args.Graphics.DrawString("Разработка: Павел Снетков, АТ-24\n\n*Рекомендуется не менять размер окна", menuFont, Brushes.DeepPink, 5, 845);


                    GModel.GetEasyButton(0, 270);

                    Controls.Add(GModel.EasyDifficultyButton);

                    GModel.GetMediumButton(0, 370);

                    Controls.Add(GModel.MediumDifficultyButton);

                    GModel.GetHardButton(0, 470);

                    Controls.Add(GModel.HardDifficultyButton);

                    CheckButtons();
                }

            };

            //проверяет нажатие кнопок старта игры
            void CheckButtons()
            {

                GModel.EasyDifficultyButton.Click += (sender, e) => {
                    menuFrameTimer.Stop();
                    GModel.ResetFrames();
                    GModel.SetInGame();
                    frameTimer.Start();
                    GModel.SetEasyDifficulty();
                    Controls.Clear();
                };


                GModel.MediumDifficultyButton.Click += (sender, e) => {
                    menuFrameTimer.Stop();
                    GModel.ResetFrames();
                    GModel.SetInGame();
                    frameTimer.Start();
                    GModel.SetMediumDifficulty();
                    Controls.Clear();
                };


                GModel.HardDifficultyButton.Click += (sender, e) => {
                    menuFrameTimer.Stop();
                    GModel.ResetFrames();
                    GModel.SetInGame();
                    frameTimer.Start();
                    GModel.SetHardDifficulty();
                    Controls.Clear();
                };
            }

            //отслеживание нажатия клавиш
            KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (GModel.InGame)
                    {
                        menuFrameTimer.Start();
                        frameTimer.Stop();
                        GModel.MakeNewGame();
                        Invalidate();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }

                if (e.KeyCode == Keys.M)
                {
                    frameTimer.Stop();
                    GModel = new GameModel();
                    Invalidate();
                }

                if (e.KeyCode == Keys.E)
                {
                    var n = GModel.Plr.Hp2;
                    GModel.Plr.Hp2 = GModel.Plr.Hp1;
                    GModel.Plr.Hp1 = n;

                    if (GModel.Plr.Switch)
                    {
                        GModel.Plr.ResetSwithFrame();
                        GModel.Plr.Switch = false;
                    }
                    else
                    {
                        GModel.Plr.ResetSwithFrame();
                        GModel.Plr.Switch = true;
                    }
                }

                //записывает значения нажатых клавиш в буфер, если персонажи уже двигается
                if (e.KeyCode == Keys.W && GModel.Plr.Moving)
                    GModel.KeysBuffer = Keys.W;
                if (e.KeyCode == Keys.S && GModel.Plr.Moving)
                    GModel.KeysBuffer = Keys.S;


                if (e.KeyCode == Keys.P && GModel.InGame)
                {
                    if (!PauseFlag)
                    {
                        frameTimer.Stop();
                        PauseFlag = true;
                    }
                    else if (GModel.InGame)
                    {
                        frameTimer.Start();
                        PauseFlag = false;
                    }
                }
                if (!GModel.Plr.Moving)
                {
                    var flag = false;
                    if (e.KeyCode == Keys.W)
                    {
                        GModel.Plr.Move(Directions.Up);
                        flag = true;
                    }
                    if (e.KeyCode == Keys.S)
                    {
                        GModel.Plr.Move(Directions.Down);
                        flag = true;
                    }

                    if (flag)
                    {
                        GModel.Plr.ResetMovingTime();
                        GModel.Plr.Moving = true;
                    }

                }
            };

            //проверка буфера нажатых кнопок (необходимо, чтобы можно
            //было нажимать кнопку движения вверх или вниз два раза подряд, игра запоминает последнее нажатие)
            void CheckBuffer()
            {
                if (GModel.KeysBuffer == Keys.W && !GModel.Plr.Moving)
                {
                    GModel.Plr.Move(Directions.Up);
                    GModel.Plr.Moving = true;
                    GModel.Plr.ResetMovingTime();
                    GModel.KeysBuffer = Keys.None;
                }
                if (GModel.KeysBuffer == Keys.S && !GModel.Plr.Moving)
                {
                    GModel.Plr.Move(Directions.Down);
                    GModel.Plr.Moving = true;
                    GModel.Plr.ResetMovingTime();
                    GModel.KeysBuffer = Keys.None;
                }
            }

        }
    }
}