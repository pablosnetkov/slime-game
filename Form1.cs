using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.Reflection.Emit;


namespace game
{
    public partial class Form1 : Form
    {


        //private readonly Bitmap playerBitmap = new("/");
        //private readonly Bitmap bossBitmap = new("/");
        private readonly Bitmap fireballBitmap = new("D:/game_project/game/game/UI/images/fireball.png");
        private readonly Bitmap switchBitmap = new("D:/game_project/game/game/UI/images/switch.png");
        private readonly Bitmap backgroundBitmap = new("D:/game_project/game/game/UI/images/spritesheet.png");
        private readonly Bitmap playerMoveBitmap = new("D:/game_project/game/game/UI/images/switch.png");
        private readonly Bitmap slimeDownBitmap = new("D:/game_project/game/game/UI/images/slimeDown.png");
        private readonly Bitmap slimeUpBitmap = new("D:/game_project/game/game/UI/images/slimeUp.png");
        private readonly Bitmap slimeDownGreenBitmap = new("D:/game_project/game/game/UI/images/slimeDownGreen.png");
        private readonly Bitmap slimeUpGreenBitmap = new("D:/game_project/game/game/UI/images/slimeUpGreen.png");
        private readonly Bitmap attackSprite = new("D:/game_project/game/game/UI/images/attackSprite.png");
        private readonly Bitmap portalSprite = new("D:/game_project/game/game/UI/images/portalSprite.png");


        static List<Projectile> Projectiles = new List<Projectile>();

        public class GameModel
        {
            public int Difficulty = 5;
            public bool InGame = false;
        }


        public Form1()
        {

            var PauseFlag = false;

            var GModel = new GameModel();

            Brush brush1;
            Brush brush2;
            var dx = -150;
            var projectile = new Projectile();
            DoubleBuffered = true;
            ClientSize = new Size(1920, 1080);
            System.Drawing.Text.PrivateFontCollection f = new System.Drawing.Text.PrivateFontCollection();
            f.AddFontFile("D:/game_project/game/game/EpilepsySans.ttf");
            var newFont = new Font(f.Families[0], 50);
            var projectileFont = new Font(f.Families[0], 40);
            var scoreFont = new Font(f.Families[0], 50);
            var attentionFont = new Font(f.Families[0], 30);
            var centerX = ClientSize.Width / 2;
            var centerY = ClientSize.Height / 2;
            var size = 100;
            var radius = Math.Min(ClientSize.Width, ClientSize.Height) / 3;
            var player = new Player();
            var random = new Random();
            player.Hp1 = random.Next(-100, 100);
            player.Hp2 = random.Next(-100, 100);
            player.Moving = false;

            var switchTime = 0;

            var frameTime = 0;
            var frame = 0;
            var frameTimer = new System.Windows.Forms.Timer();
            frameTimer.Interval = 1;

            player.X = 200;
            player.Y = 480;
            var time = 0;


            var attackTime = 0;


            var projectileTime = 0;

            var attackFlag = true;

            var proj = new Projectile();
            proj.Number = 0;
            proj.Attack = true;
            proj.X = 1900;
            proj.SetHeight();
            proj.SetValue(0);
            Projectiles.Add(proj);

            proj = new Projectile();
            proj.Number = 1;
            proj.Attack = true;
            proj.X = 1900 + 1920 / 4;
            proj.SetHeight();
            proj.SetValue(0);
            Projectiles.Add(proj);

            proj = new Projectile();
            proj.Number = 2;
            proj.Attack = true;
            proj.X = 1900 + 1920 / 4 * 2;
            proj.SetHeight();
            proj.SetValue(0);
            Projectiles.Add(proj);

            proj = new Projectile();
            proj.Number = 3;
            proj.Attack = true;
            proj.X = 1900 + 1920 / 4 * 3;
            proj.SetHeight();
            proj.SetValue(0);
            Projectiles.Add(proj);
            if (GModel.InGame)
                frameTimer.Start();
            //срабатывание анимации смены переменных игрока
            frameTimer.Tick += async (sender, args) =>
            {
                frameTime++;
                if (frameTime == 15)
                {
                    frame += 1;
                    if (frame == 48) frame = 0;
                    frameTime = 0;
                }

                if (player.SwitchFrame == 7)
                {
                    //player.Switch = false;
                    player.SwitchFrame = -1;
                }
                else if (player.SwitchFrame >= 0)
                    player.SwitchFrame += 1;

                //switchTime++;
                //if (switchTime == 10)
                //{
                //    player.Switch = false;
                //    switchTime = 0;
                //    switchTimer.Stop();
                //}

                
                if (player.MovingTime == 5)
                {
                    player.Steps = 0;
                    player.Moving = false;
                }  
                player.Y += player.Steps;
                player.MovingTime++;

                //attackTime++;
                //if (attackTime == 100)
                //{
                //    var proj = new Projectile();
                //    proj.X = 1920;
                //    proj.SetHeight();
                //    proj.SetValue(player.Hp1, player.Hp2);
                //    Projectiles.Add(proj);
                //    attackTime = 0;
                //}

                attackTime++;

                if (attackTime == 1601)
                    attackTime = 1;

                for (var i = 0; i < 4; i ++)
                {
                    Projectiles[i].AttackTimer--;
                    if (Projectiles[i].Attack)
                        Projectiles[i].X -= 12;
                    if (Projectiles[i].X <= 0)
                    {
                        frameTimer.Stop();
                        MessageBox.Show("Вы проиграли!");
                    }
                    if (Projectiles[i].X < player.X + 20 && Projectiles[i].Y == player.Y)
                    {
                        player.Score += 1;
                        player.Hp1 += Projectiles[i].Value;
                        if (player.Hp1 >= 100 || player.Hp1 <= -100)
                        {
                            projectileTime = 0;
                            frameTimer.Stop();
                            MessageBox.Show("Вы проиграли!");
                        }
                        
                        Projectiles[i].X = 1900;
                        Projectiles[i].SetHeight();
                        
                        Projectiles[i].SetAttackTimer();
                        Projectiles[i].Attack = false;
                        


                        Projectiles[i].Number = 4;
                        foreach (var p in Projectiles)
                        {
                            p.Number--;
                        }

                        Projectiles[i].SetValue(player.Hp1, player.Hp2, Projectiles);

                    }
                }

                //if ((player.Hp1 > 99 || player.Hp2 > 99 || player.Hp1 < -99 || player.Hp2 < -99))
                //{
                //    projectileTime = 0;
                //    frameTimer.Stop();
                //    MessageBox.Show("Вы проиграли!");
                //}


                foreach(var p in Projectiles)
                {
                    if (p.AttackTimer == 0)
                        p.Attack = true;
                }

                Invalidate();
            };






            //отрисовка объектов
            Paint += (sender, args) =>
            {

                //анимация файрбола (атаки)
                args.Graphics.DrawImageUnscaledAndClipped(fireballBitmap, new Rectangle(50, 50, 74, 74));

                //перемещение значений hp и изменение их цвета
                if (player.Switch)
                {
                    dx = -150;
                    brush1 = Brushes.Purple;
                    brush2 = Brushes.MediumBlue;
                }
                else
                {
                    brush2 = Brushes.Purple;
                    brush1 = Brushes.MediumBlue;
                    dx = 0;
                }



                //рисует фон
                args.Graphics.DrawImage(backgroundBitmap, 0, 0, new Rectangle(frame % 9 * 1920, 0, 1920, 1080), GraphicsUnit.Pixel);


                //отрисовка снарядов
                foreach (var projectile in Projectiles)
                {
                    args.Graphics.DrawImage(attackSprite, projectile.X - 20, projectile.Y - 60, new Rectangle(frame % 6 * 200, 0, 200, 172), GraphicsUnit.Pixel);
                    
                    args.Graphics.DrawString(projectile.Value.ToString(), projectileFont, Brushes.Red, projectile.X, projectile.Y);

                }


                //отрисовка слаймов с анимацией
                if (frame % 2 == 0)
                {
                    args.Graphics.DrawImage(slimeUpBitmap, player.X - 25 + dx, player.Y - 25);
                    args.Graphics.DrawImage(slimeDownGreenBitmap, player.X - 175 - dx, player.Y - 25);
                }
                else
                {
                    args.Graphics.DrawImage(slimeDownBitmap, player.X - 25 + dx, player.Y - 25);
                    args.Graphics.DrawImage(slimeUpGreenBitmap, player.X - 175 - dx, player.Y - 25);
                }

                //вывод значений хитпоинтов игрока
                args.Graphics.DrawString(player.Hp2.ToString(), projectileFont, brush1, player.X - 150, player.Y);
                args.Graphics.DrawString(player.Hp1.ToString(), projectileFont, brush2, player.X, player.Y);
                
                //вывод количество очков игрока
                args.Graphics.DrawString(player.Score.ToString(), scoreFont, Brushes.Gold, 0, 0);

                //окно с объявлением
                args.Graphics.DrawString("Ваши числа должны быть больше -100, но меньше 100!", newFont, Brushes.Gold, 350, 0);

                //анимация портала
                if (player.SwitchFrame >= 0)
                {
                    args.Graphics.DrawImage(portalSprite, player.X - 40, player.Y - 40, new Rectangle(player.SwitchFrame % 6 * 150, 0, 150, 150), GraphicsUnit.Pixel);
                    args.Graphics.DrawImage(portalSprite, player.X - 190, player.Y - 40, new Rectangle(player.SwitchFrame % 6 * 150, 0, 150, 150), GraphicsUnit.Pixel);
                }


            };


            FormClosing += (sender, eventArgs) =>
            {
                var result = MessageBox.Show("Действительно закрыть?", "", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    eventArgs.Cancel = true;
            };

            //отслеживание нажатия клавиш
            KeyDown += (sender, e) =>
            {
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
                if (!player.Moving)
                {
                    var flag = false;
                    if (e.KeyCode == Keys.W)
                    {
                        player.Move(Directions.Up);
                        flag = true;
                    }
                    if (e.KeyCode == Keys.S)
                    {
                        player.Move(Directions.Down);
                        flag = true;
                    }

                    if (e.KeyCode == Keys.E)
                    {
                        var n = player.Hp2;
                        player.Hp2 = player.Hp1;
                        player.Hp1 = n;

                        if (player.Switch)
                        {
                            player.SwitchFrame = 0;
                            player.Switch = false;
                        }

                        else
                        {
                            player.SwitchFrame = 0;
                            player.Switch = true;
                        }
                    }

                    if (flag )
                    {
                        player.MovingTime = 0;
                        player.Moving = true;
                    }
                }
            };

        }
    }
}