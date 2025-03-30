using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OurGame
{
    public partial class TentacleBossFightForm : Form
    {
        private Player player;
        private List<Tentacle> tentacles;
        private List<EnergyPulse> pulses;
        private List<DangerZone> dangerZones;
        private System.Windows.Forms.Timer gameTimer;
        private Random rand;
        private int playerHealth;
        private const int maxPlayerHealth = 5;
        private double timeLeft;
        private const double maxTime = 90.0; // 90 секунд
        private bool isGameOver;
        private bool isSecondPhase;
        private double attackCooldown;
        private const double attackCooldownTime = 2.0; // 2 секунды перезарядки атаки

        public event EventHandler PuzzleSolved; // Событие, которое сработает при победе

        public TentacleBossFightForm()
        {
            this.Text = "Сражение с боссом-щупальцами";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.DoubleBuffered = true;

            player = new Player(this.ClientSize.Width / 2, this.ClientSize.Height - 50);
            tentacles = new List<Tentacle>();
            pulses = new List<EnergyPulse>();
            dangerZones = new List<DangerZone>();
            rand = new Random();
            playerHealth = maxPlayerHealth;
            timeLeft = maxTime;
            isGameOver = false;
            isSecondPhase = false;
            attackCooldown = 0;

            // Создаём щупальца
            for (int i = 0; i < 5; i++)
            {
                tentacles.Add(new Tentacle(100 + i * 150, 0, rand, i % 2 == 0));
            }

            // Настраиваем таймер для обновления игры
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 20; // 50 FPS
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.KeyDown += TentacleBossFightForm_KeyDown;
            this.KeyUp += TentacleBossFightForm_KeyUp;
            this.MouseClick += TentacleBossFightForm_MouseClick;
            this.Paint += TentacleBossFightForm_Paint;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (isGameOver)
                return;

            // Обновляем время
            timeLeft -= gameTimer.Interval / 1000.0;
            if (timeLeft <= 0)
            {
                isGameOver = true;
                gameTimer.Stop();
                MessageBox.Show("Время истекло! Босс-щупальца победил!", "Поражение");
                this.Close();
                return;
            }

            // Обновляем перезарядку атаки
            if (attackCooldown > 0)
            {
                attackCooldown -= gameTimer.Interval / 1000.0;
            }

            // Обновляем игрока
            player.Update(this.ClientSize);

            // Список для уничтоженных щупалец
            List<Tentacle> tentaclesToRemove = new List<Tentacle>();

            // Обновляем щупальца
            foreach (var tentacle in tentacles)
            {
                if (isGameOver) break; // Прерываем цикл, если игра завершена

                tentacle.Update(this.ClientSize, player, pulses, isSecondPhase, gameTimer.Interval);
                // Проверяем столкновение игрока с щупальцем
                if (tentacle.IsCollidingWithPlayer(player))
                {
                    playerHealth -= tentacle.IsFast ? 1 : 2;
                    if (playerHealth <= 0)
                    {
                        isGameOver = true;
                        gameTimer.Stop();
                        MessageBox.Show("Вы проиграли! Босс-щупальца победил.", "Поражение");
                        this.Close();
                        return;
                    }
                }

                // Создаём опасные зоны
                if (rand.NextDouble() < 0.005)
                {
                    dangerZones.Add(new DangerZone(tentacle.TipX, tentacle.TipY, gameTimer.Interval));
                }

                // Если щупальце уничтожено, добавляем его в список для удаления
                if (tentacle.IsDestroyed)
                {
                    tentaclesToRemove.Add(tentacle);
                }
            }

            // Удаляем уничтоженные щупальца после цикла
            foreach (var tentacle in tentaclesToRemove)
            {
                tentacles.Remove(tentacle);
            }

            // Обновляем импульсы
            foreach (var pulse in pulses)
            {
                if (isGameOver) break;

                pulse.Update(player);
                if (pulse.IsCollidingWithPlayer(player))
                {
                    playerHealth--;
                    if (playerHealth <= 0)
                    {
                        isGameOver = true;
                        gameTimer.Stop();
                        MessageBox.Show("Вы проиграли! Босс-щупальца победил.", "Поражение");
                        this.Close();
                        return;
                    }
                    pulse.IsActive = false;
                }
            }
            pulses.RemoveAll(p => !p.IsActive);

            // Обновляем опасные зоны
            foreach (var zone in dangerZones)
            {
                if (isGameOver) break;

                zone.Update();
                if (zone.IsCollidingWithPlayer(player))
                {
                    playerHealth--;
                    if (playerHealth <= 0)
                    {
                        isGameOver = true;
                        gameTimer.Stop();
                        MessageBox.Show("Вы проиграли! Босс-щупальца победил.", "Поражение");
                        this.Close();
                        return;
                    }
                    zone.IsActive = false;
                }
            }
            dangerZones.RemoveAll(z => !z.IsActive);

            // Проверяем переход во вторую фазу
            if (!isSecondPhase && tentacles.Count <= 2)
            {
                isSecondPhase = true;
                foreach (var tentacle in tentacles)
                {
                    tentacle.EnterSecondPhase();
                }
            }

            // Проверяем, все ли щупальца уничтожены
            if (tentacles.Count == 0)
            {
                isGameOver = true;
                gameTimer.Stop();
                MessageBox.Show("Поздравляем! Вы победили босса-щупальца!", "Победа!");
                PuzzleSolved?.Invoke(this, EventArgs.Empty);
                this.Close();
                return;
            }

            this.Invalidate();
        }

        private void TentacleBossFightForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) player.IsMovingUp = true;
            if (e.KeyCode == Keys.S) player.IsMovingDown = true;
            if (e.KeyCode == Keys.A) player.IsMovingLeft = true;
            if (e.KeyCode == Keys.D) player.IsMovingRight = true;
        }

        private void TentacleBossFightForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) player.IsMovingUp = false;
            if (e.KeyCode == Keys.S) player.IsMovingDown = false;
            if (e.KeyCode == Keys.A) player.IsMovingLeft = false;
            if (e.KeyCode == Keys.D) player.IsMovingRight = false;
        }

        private void TentacleBossFightForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (isGameOver || attackCooldown > 0)
                return;

            foreach (var tentacle in tentacles)
            {
                if (tentacle.IsHit(e.X, e.Y))
                {
                    tentacle.TakeDamage();
                    attackCooldown = attackCooldownTime;
                    break;
                }
            }
        }

        private void TentacleBossFightForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Рисуем фон
            g.Clear(Color.Black);

            // Рисуем время
            using (Font font = new Font("Arial", 12))
            {
                g.DrawString($"Время: {(int)timeLeft} сек", font, Brushes.White, 10, 60);
                g.DrawString($"Фаза: {(isSecondPhase ? "Вторая" : "Первая")}", font, Brushes.White, 10, 100);
            }

            // Рисуем здоровье игрока
            for (int i = 0; i < maxPlayerHealth; i++)
            {
                g.FillRectangle(i < playerHealth ? Brushes.Red : Brushes.Gray,
                    10 + i * 30, 10, 20, 20);
            }

            // Рисуем перезарядку атаки
            float cooldownWidth = (float)(50 * (1 - attackCooldown / attackCooldownTime));
            g.FillRectangle(Brushes.Yellow, 10, 40, cooldownWidth, 10);

            // Рисуем опасные зоны
            foreach (var zone in dangerZones)
            {
                zone.Draw(g);
            }

            // Рисуем импульсы
            foreach (var pulse in pulses)
            {
                pulse.Draw(g);
            }

            // Рисуем игрока
            player.Draw(g);

            // Рисуем щупальца
            foreach (var tentacle in tentacles)
            {
                tentacle.Draw(g);
            }

            // Рисуем инструкции
            using (Font font = new Font("Arial", 12))
            {
                g.DrawString("WASD - двигаться, Клик - атаковать (перезарядка 2 сек)", font, Brushes.White, 10, 80);
            }
        }
    }

    // Класс игрока
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Radius { get; } = 20;
        public int Speed { get; } = 5;
        public bool IsMovingUp { get; set; }
        public bool IsMovingDown { get; set; }
        public bool IsMovingLeft { get; set; }
        public bool IsMovingRight { get; set; }

        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Update(Size clientSize)
        {
            if (IsMovingUp) Y -= Speed;
            if (IsMovingDown) Y += Speed;
            if (IsMovingLeft) X -= Speed;
            if (IsMovingRight) X += Speed;

            // Ограничиваем движение игрока границами формы
            X = Math.Max(Radius, Math.Min(clientSize.Width - Radius, X));
            Y = Math.Max(Radius, Math.Min(clientSize.Height - Radius, Y));
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Cyan, X - Radius, Y - Radius, Radius * 2, Radius * 2);
        }
    }

    // Класс щупальца
    public class Tentacle
    {
        private int baseX;
        private int baseY;
        private int length;
        private float angle;
        private float angleSpeed;
        private int tipX;
        private int tipY;
        private int health;
        private const int maxHealth = 5;
        private Random rand;
        private const int tipRadius = 15;
        private bool isFast;
        private double pulseCooldown;
        private double pulseCooldownTime = 3.0; // Импульсы каждые 3 секунды

        public int TipX => tipX;
        public int TipY => tipY;
        public bool IsFast => isFast;
        public bool IsDestroyed => health <= 0;
        public float AngleSpeed => angleSpeed; // Для отладки

        public Tentacle(int x, int y, Random rand, bool isFast)
        {
            this.baseX = x;
            this.baseY = y;
            this.length = 200;
            this.angle = (float)(rand.NextDouble() * Math.PI * 2);
            this.angleSpeed = isFast ? (float)(rand.NextDouble() * 0.07 - 0.035) : (float)(rand.NextDouble() * 0.03 - 0.015);
            this.health = maxHealth;
            this.rand = rand;
            this.isFast = isFast;
            this.pulseCooldown = 0;
            UpdateTipPosition(new Size(800, 600)); // Начальная инициализация с размером формы
        }

        public void Update(Size clientSize, Player player, List<EnergyPulse> pulses, bool isSecondPhase, int timerInterval)
        {
            angle += angleSpeed;
            if (rand.NextDouble() < 0.02)
            {
                angleSpeed = isFast ? (float)(rand.NextDouble() * 0.07 - 0.035) : (float)(rand.NextDouble() * 0.03 - 0.015);
            }

            UpdateTipPosition(clientSize);

            // Создаём импульсы
            if (pulseCooldown <= 0)
            {
                pulses.Add(new EnergyPulse(tipX, tipY, player));
                pulseCooldown = isSecondPhase ? pulseCooldownTime * 0.5 : pulseCooldownTime;
            }
            else
            {
                pulseCooldown -= timerInterval / 1000.0;
            }
        }

        public void EnterSecondPhase()
        {
            angleSpeed *= 1.3f;
            length += 50;
        }

        private void UpdateTipPosition(Size clientSize)
        {
            tipX = baseX + (int)(Math.Cos(angle) * length);
            tipY = baseY + (int)(Math.Sin(angle) * length);

            // Проверяем, что clientSize не null и ограничиваем движение конца щупальца
            if (clientSize != null && clientSize.Width > 0 && clientSize.Height > 0)
            {
                tipX = Math.Max(tipRadius, Math.Min(clientSize.Width - tipRadius, tipX));
                tipY = Math.Max(tipRadius, Math.Min(clientSize.Height - tipRadius, tipY));
            }
        }

        public bool IsCollidingWithPlayer(Player player)
        {
            float dist = DistanceToLine(player.X, player.Y, baseX, baseY, tipX, tipY);
            return dist < player.Radius;
        }

        public bool IsHit(int mouseX, int mouseY)
        {
            float dist = (float)Math.Sqrt(Math.Pow(mouseX - tipX, 2) + Math.Pow(mouseY - tipY, 2));
            return dist < tipRadius;
        }

        public void TakeDamage()
        {
            health--;
        }

        public void Draw(Graphics g)
        {
            using (Pen pen = new Pen(isFast ? Color.Purple : Color.DarkRed, 5))
            {
                g.DrawLine(pen, baseX, baseY, tipX, tipY);
            }

            g.FillEllipse(Brushes.Red, tipX - tipRadius, tipY - tipRadius, tipRadius * 2, tipRadius * 2);

            for (int i = 0; i < maxHealth; i++)
            {
                g.FillRectangle(i < health ? Brushes.Green : Brushes.Gray,
                    baseX - 15 + i * 10, baseY - 10, 8, 8);
            }

            // Отладочная информация: показываем скорость щупальца
            using (Font font = new Font("Arial", 8))
            {
                g.DrawString($"Speed: {angleSpeed:F3}", font, Brushes.White, baseX - 20, baseY + 20);
            }
        }

        private float DistanceToLine(float px, float py, float x1, float y1, float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            float t = ((px - x1) * dx + (py - y1) * dy) / (dx * dx + dy * dy);
            t = Math.Max(0, Math.Min(1, t));
            float closestX = x1 + t * dx;
            float closestY = y1 + t * dy;
            return (float)Math.Sqrt(Math.Pow(px - closestX, 2) + Math.Pow(py - closestY, 2));
        }
    }

    // Класс энергетического импульса
    public class EnergyPulse
    {
        private float x;
        private float y;
        private float dx;
        private float dy;
        private const int radius = 10;
        private const float speed = 3;
        public bool IsActive { get; set; }

        public EnergyPulse(int startX, int startY, Player player)
        {
            x = startX;
            y = startY;
            float dist = (float)Math.Sqrt(Math.Pow(player.X - x, 2) + Math.Pow(player.Y - y, 2));
            dx = (player.X - x) / dist * speed;
            dy = (player.Y - y) / dist * speed;
            IsActive = true;
        }

        public void Update(Player player)
        {
            x += dx;
            y += dy;
        }

        public bool IsCollidingWithPlayer(Player player)
        {
            float dist = (float)Math.Sqrt(Math.Pow(player.X - x, 2) + Math.Pow(player.Y - y, 2));
            return dist < player.Radius + radius;
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Orange, x - radius, y - radius, radius * 2, radius * 2);
        }
    }

    // Класс опасной зоны
    public class DangerZone
    {
        private int x;
        private int y;
        private const int radius = 30;
        private double lifetime;
        private const double maxLifetime = 5.0; // 5 секунд
        private readonly int timerInterval;
        public bool IsActive { get; set; }

        public DangerZone(int x, int y, int timerInterval)
        {
            this.x = x;
            this.y = y;
            this.timerInterval = timerInterval;
            lifetime = maxLifetime;
            IsActive = true;
        }

        public void Update()
        {
            lifetime -= timerInterval / 1000.0;
            if (lifetime <= 0)
            {
                IsActive = false;
            }
        }

        public bool IsCollidingWithPlayer(Player player)
        {
            float dist = (float)Math.Sqrt(Math.Pow(player.X - x, 2) + Math.Pow(player.Y - y, 2));
            return dist < player.Radius + radius;
        }

        public void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color.Red, 2))
            {
                g.DrawEllipse(pen, x - radius, y - radius, radius * 2, radius * 2);
            }
        }
    }
}