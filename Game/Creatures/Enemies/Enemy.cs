using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Game.Creatures.Players;
using Game.Bullets;
using Game.Objects.Weapons;
using Game.Objects.Weapons.PlayerWeapons;
using Game.Bullets.EnemyBullets;
using Game.Bullets.PlayerBullets;
using Game.Objects.Weapons.EnemyWeapons;

namespace Game.Creatures.Enemies
{
    internal class Enemy: Creature
    {
        private EnemyGun gun;
        public Enemy(Point spawnPoint, Canvas MyCanvas, List<Enemy> enemies)
        {
            this.Health = 50;
            this.body.Fill = new SolidColorBrush(Colors.Blue);
            this.body.Name = "enemy";
            this.creatureSpeed = 10;
            MyCanvas.Children.Add(this.body);
            Canvas.SetLeft(this.body, spawnPoint.X);
            Canvas.SetTop(this.body, spawnPoint.Y);

            enemies.Add(this);

            gun = new EnemyGun();
            gun.PlayerPosition = spawnPoint;

            hitBox = new Rect(Canvas.GetLeft(this.body), Canvas.GetTop(this.body), body.Width, body.Height);
        }

        public void Shot(Rectangle player, List<EnemyOrdinaryBullet> bullets, Canvas MyCanvas)
        {
            gun.GunReload();
            gun.Shot(new Point(Canvas.GetLeft(player), Canvas.GetTop(player)), bullets, MyCanvas);
        }

        public void move(Point playerPosition, Canvas MyCanvas)
        {
            Point nextPosition = CalculatePathToPlayer(playerPosition);

            Canvas.SetLeft(this.body, nextPosition.X);
            Canvas.SetTop(this.body, nextPosition.Y);
            gun.PlayerPosition.X = nextPosition.X;
            gun.PlayerPosition.Y = nextPosition.Y;
            hitBox.X = nextPosition.X;
            hitBox.Y = nextPosition.Y;
        }

        protected Point CalculatePathToPlayer(Point playerPosition)
        {
            double angle = Math.Atan2(playerPosition.Y - GetPosition().Y, playerPosition.X - GetPosition().X);

            double vX = this.creatureSpeed * Math.Cos(angle);
            double vY = this.creatureSpeed * Math.Sin(angle);

            double x = GetPosition().X + vX;
            double y = GetPosition().Y + vY;

            return new Point(x, y);
        }
    }
}
