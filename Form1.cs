using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.Reflection.Emit;
using System;
using System.Security.Cryptography.X509Certificates;

namespace game
{
    internal class GameModel
    {
        public int Difficulty = 5;
        public bool InGame = false;
        public Button EasyDifficultyButton = new Button();
        public Button MediumDifficultyButton = new Button();
        public Button HardDifficultyButton = new Button();
        public List<Projectile> Projectiles = new List<Projectile>();
        public Player Player = new Player();
        public int AttackTime = 0;
        public int Dx = -150;
        public int FrameTime = 0;
        public int Frame = 0;


        public GameModel()
        {
            SetPlayer();
            SetProjectiles();
        }


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

        public Button GetEasyButton(int x, int y)
        {
            MakeButton(x, y, EasyDifficultyButton);
            return EasyDifficultyButton;
        }

        public Button GetMediumButton(int x, int y)
        {
            MakeButton(x, y, MediumDifficultyButton);
            return MediumDifficultyButton;
        }

        public Button GetHardButton(int x, int y)
        {
            MakeButton(x, y, HardDifficultyButton);
            return HardDifficultyButton;
        }

        public void SetProjectiles()
        {
            for (int i = 0; i < 4; i++)
            {
                var proj = new Projectile();
                proj.Number = i;
                proj.Attack = true;
                proj.X = 1900 + 480 * i;
                proj.SetHeight();
                proj.SetValue(0);
                Projectiles.Add(proj);
            }
        }

        public void SetPlayer()
        {
            var random = new Random();
            Player.Hp1 = random.Next(-100, 100);
            Player.Hp2 = random.Next(-100, 100);
            Player.Moving = false;
            Player.X = 200;
            Player.Y = 480;
        }

        public void MakeNewGame()
        {
            SetPlayer();
            SetProjectiles();
            InGame = false;
        }
    }


    public partial class Form1 : Form
    {
        private readonly Bitmap backgroundBitmap = new("D:/game_project/game/game/UI/images/spritesheet.png");
        private readonly Bitmap slimeDownBitmap = new("D:/game_project/game/game/UI/images/slimeDown.png");
        private readonly Bitmap slimeUpBitmap = new("D:/game_project/game/game/UI/images/slimeUp.png");
        private readonly Bitmap slimeDownGreenBitmap = new("D:/game_project/game/game/UI/images/slimeDownGreen.png");
        private readonly Bitmap slimeUpGreenBitmap = new("D:/game_project/game/game/UI/images/slimeUpGreen.png");
        private readonly Bitmap attackSprite = new("D:/game_project/game/game/UI/images/attackSprite.png");
        private readonly Bitmap portalSprite = new("D:/game_project/game/game/UI/images/portalSprite.png");

        GameModel GModel = new GameModel();

        public Form1()
        {
            //Invalidate();
            var PauseFlag = false;

            Brush brush1;
            Brush brush2;
            DoubleBuffered = true;
            ClientSize = new Size(1920, 1080);
            System.Drawing.Text.PrivateFontCollection f = new System.Drawing.Text.PrivateFontCollection();
            f.AddFontFile("D:/game_project/game/game/EpilepsySans.ttf");
            var newFont = new Font(f.Families[0], 50);
            var menuFont = new Font(f.Families[0], 30);
            var instructionsFont = new Font(f.Families[0], 20);
            var projectileFont = new Font(f.Families[0], 40);
            var scoreFont = new Font(f.Families[0], 50);
            var attentionFont = new Font(f.Families[0], 30);


            var frameTimer = new System.Windows.Forms.Timer();
            frameTimer.Interval = 1;


            //срабатывание анимации смены переменных игрока
            frameTimer.Tick += (sender, args) =>
            {
                GModel.FrameTime++;
                if (GModel.FrameTime == 15)
                {
                    GModel.Frame += 1;
                    if (GModel.Frame == 48) GModel.Frame = 0;
                    GModel.FrameTime = 0;
                }

                if (GModel.Player.SwitchFrame == 7)
                {
                    //player.Switch = false;
                    GModel.Player.SwitchFrame = -1;
                }
                else if (GModel.Player.SwitchFrame >= 0)
                    GModel.Player.SwitchFrame += 1;
                
                if (GModel.Player.MovingTime == 5)
                {
                    GModel.Player.Steps = 0;
                    GModel.Player.Moving = false;
                }  
                GModel.Player.Y += GModel.Player.Steps;
                GModel.Player.MovingTime++;

                GModel.AttackTime++;

                if (GModel.AttackTime == 1601)
                    GModel.AttackTime = 1;

                for (var i = 0; i < 4; i ++)
                {
                    GModel.Projectiles[i].AttackTimer--;
                    if (GModel.Projectiles[i].Attack)
                        GModel.Projectiles[i].X -= GModel.Difficulty;
                    if (GModel.Projectiles[i].X <= 0)
                    {
                        frameTimer.Stop();
                        GModel.MakeNewGame();
                        MessageBox.Show("Вы проиграли!");
                        Invalidate();
                    }
                    if (GModel.Projectiles[i].X < GModel.Player.X + 20 && GModel.Projectiles[i].Y == GModel.Player.Y)
                    {
                        GModel.Player.Score += 1;
                        GModel.Player.Hp1 += GModel.Projectiles[i].Value;
                        if (GModel.Player.Hp1 >= 100 || GModel.Player.Hp1 <= -100)
                        {
                            frameTimer.Stop();
                            GModel.MakeNewGame();
                            MessageBox.Show("Вы проиграли!");
                            Invalidate();
                        }
                        
                        GModel.Projectiles[i].X = 1940;
                        GModel.Projectiles[i].SetHeight();
                        
                        GModel.Projectiles[i].SetAttackTimer();
                        GModel.Projectiles[i].Attack = false;
                        


                        GModel.Projectiles[i].Number = 4;
                        foreach (var p in GModel.Projectiles)
                        {
                            p.Number--;
                        }

                        GModel.Projectiles[i].SetValue(GModel.Player.Hp1, GModel.Player.Hp2, GModel.Projectiles);

                    }
                }

                foreach(var p in GModel.Projectiles)
                {
                    if (p.AttackTimer == 0)
                        p.Attack = true;
                }

                Invalidate();
            };


            //отрисовка объектов
            Paint += (sender, args) =>
            {
                if (GModel.InGame)
                {
                    //анимация файрбола (атаки)
                    //args.Graphics.DrawImageUnscaledAndClipped(fireballBitmap, new Rectangle(50, 50, 74, 74));

                    //перемещение значений hp и изменение их цвета
                    if (GModel.Player.Switch)
                    {
                        GModel.Dx = -150;
                        brush1 = Brushes.Purple;
                        brush2 = Brushes.MediumBlue;
                    }
                    else
                    {
                        brush2 = Brushes.Purple;
                        brush1 = Brushes.MediumBlue;
                        GModel.Dx = 0;
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
                        args.Graphics.DrawImage(slimeUpBitmap, GModel.Player.X - 25 + GModel.Dx, GModel.Player.Y - 25);
                        args.Graphics.DrawImage(slimeDownGreenBitmap, GModel.Player.X - 175 - GModel.Dx, GModel.Player.Y - 25);
                    }
                    else
                    {
                        args.Graphics.DrawImage(slimeDownBitmap, GModel.Player.X - 25 + GModel.Dx, GModel.Player.Y - 25);
                        args.Graphics.DrawImage(slimeUpGreenBitmap, GModel.Player.X - 175 - GModel.Dx, GModel.Player.Y - 25);
                    }

                    //вывод значений хитпоинтов игрока
                    args.Graphics.DrawString(GModel.Player.Hp2.ToString(), projectileFont, brush1, GModel.Player.X - 150, GModel.Player.Y);
                    args.Graphics.DrawString(GModel.Player.Hp1.ToString(), projectileFont, brush2, GModel.Player.X, GModel.Player.Y);

                    //вывод количество очков игрока
                    args.Graphics.DrawString(GModel.Player.Score.ToString(), scoreFont, Brushes.Gold, 0, 0);

                    //окно с объявлением
                    //args.Graphics.DrawString("Ваши числа должны быть больше -100, но меньше 100!", newFont, Brushes.Gold, 350, 0);

                    //анимация портала
                    if (GModel.Player.SwitchFrame >= 0)
                    {
                        args.Graphics.DrawImage(portalSprite, GModel.Player.X - 40, GModel.Player.Y - 40, new Rectangle(GModel.Player.SwitchFrame % 6 * 150, 0, 150, 150), GraphicsUnit.Pixel);
                        args.Graphics.DrawImage(portalSprite, GModel.Player.X - 190, GModel.Player.Y - 40, new Rectangle(GModel.Player.SwitchFrame % 6 * 150, 0, 150, 150), GraphicsUnit.Pixel);
                    }
                }
                else
                {
                    args.Graphics.DrawImage(backgroundBitmap, 0, 0, new Rectangle(GModel.Frame % 9 * 1920, 0, 1920, 1080), GraphicsUnit.Pixel);
                    args.Graphics.DrawString("W - сдвинуться вверх\nS - сдвинуться вниз\nE - поменять слизней местами\nP - пауза\nM - меню\n\n\nВ вашу сторону летят снаряды\nс некоторыми значениями.\n" +
                        "Они либо прибавляются,\nлибо вычитаются из значения\nближайшего к снаряду слизня.\n" +
                        "Если значение одного из ваших слизней\nстанет больше 100\nили меньше -100, -вы проиграете.\nЕсли вы пропустите снаряд, вы проиграете.\nВ игре не может быть ситуаций,\nкогда вы проигрываете независимо\nот ваших действий.\nЧтобы не проиграть, \nвам нужно рассчитывать значения заранее.\n\nЧтобы начать игру,\nнажмите на один из вариантов слева.", menuFont, Brushes.Cyan, 1100, 115);
                    args.Graphics.DrawString("Начать на легкой сложности", newFont, Brushes.Gold, 0, 100);
                    args.Graphics.DrawString("Начать на средней сложности", newFont, Brushes.Gold, 0, 200);
                    args.Graphics.DrawString("Начать на максимальной сложности", newFont, Brushes.Gold, 0, 300);

                    GModel.GetEasyButton(0, 85);

                    Controls.Add(GModel.EasyDifficultyButton);

                    GModel.GetMediumButton(0, 185);

                    Controls.Add(GModel.MediumDifficultyButton);

                    GModel.GetHardButton(0, 285);

                    Controls.Add(GModel.HardDifficultyButton);

                    CheckButtons();
                }

            };


            void CheckButtons()
            {

                GModel.EasyDifficultyButton.Click += (sender, e) => {
                    GModel.InGame = true;
                    frameTimer.Start();
                    GModel.Difficulty = 5;
                    Controls.Clear();
                };


                GModel.MediumDifficultyButton.Click += (sender, e) => {
                    GModel.InGame = true;
                    frameTimer.Start();
                    GModel.Difficulty = 10;
                    Controls.Clear();
                };


                GModel.HardDifficultyButton.Click += (sender, e) => {
                    GModel.InGame = true;
                    frameTimer.Start();
                    GModel.Difficulty = 15;
                    Controls.Clear();
                };
            }


            //отслеживание нажатия клавиш
            KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.M)
                {
                    frameTimer.Stop();
                    GModel = new GameModel();
                    Invalidate();
                }

                if (e.KeyCode == Keys.E)
                {
                    var n = GModel.Player.Hp2;
                    GModel.Player.Hp2 = GModel.Player.Hp1;
                    GModel.Player.Hp1 = n;

                    if (GModel.Player.Switch)
                    {
                        GModel.Player.SwitchFrame = 0;
                        GModel.Player.Switch = false;
                    }
                    else
                    {
                        GModel.Player.SwitchFrame = 0;
                        GModel.Player.Switch = true;
                    }
                }

                if (e.KeyCode == Keys.P)
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
                if (!GModel.Player.Moving)
                {
                    var flag = false;
                    if (e.KeyCode == Keys.W)
                    {
                        GModel.Player.Move(Directions.Up);
                        flag = true;
                    }
                    if (e.KeyCode == Keys.S)
                    {
                        GModel.Player.Move(Directions.Down);
                        flag = true;
                    }

                    if (flag)
                    {
                        GModel.Player.MovingTime = 0;
                        GModel.Player.Moving = true;
                    }
                }
            };

        }
    }
}