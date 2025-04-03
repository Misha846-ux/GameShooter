using Game.Bullets.EnemyBullets;
using Game.Creatures.Players;
using Game.Objects;
using Game.Objects.Walls.BreakableWalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
using Game.GameSystem;
using System.Xml.Linq;


namespace Game.Bullets
{
    internal class Bullet
    {
        public int Damage { get; set; }
        public Rect hitBox; 
        protected Rectangle bullet;
        protected Point startPosition; // stores the position the player is in when the bullet is launched
        protected Point mousePosition; // stores the position the mouse was in when the bullet was launched
        protected double liveTime;
        protected int bulletSpeed;
        public int BoardWhidth { get; set; }
        public int BoardHeight { get; set; }
        public Bullet(Point startPosition, Point mousePosition, int damage)
        {
            this.bullet = new Rectangle
            {
                Tag = "bullet",
                Width = 20,
                Height = 20,
                Fill = new SolidColorBrush(Colors.Red)
            };
            this.bulletSpeed = 20;
            this.startPosition = startPosition;
            this.mousePosition = mousePosition;
            this.liveTime = 1;
            Canvas.SetLeft(this.bullet, startPosition.X);
            Canvas.SetTop(this.bullet, startPosition.Y);
            Damage = damage;
            hitBox = new Rect(Canvas.GetLeft(this.bullet), Canvas.GetTop(this.bullet), bullet.Width, bullet.Height);
        }
        //public Bullet(Point startPosition, Point mousePosition) : this(startPosition, mousePosition, 5) { }
        public Rectangle GetBullet()
        {
            return this.bullet;
        }
        public void BulletMove(MemoryCleaner memoryCleaner, Canvas MyCanvas)
        {

            Point nextPosition = CalculateTrajectory();
            Canvas.SetLeft(bullet, nextPosition.X);
            Canvas.SetTop(bullet, nextPosition.Y);
            this.hitBox.X = nextPosition.X ;
            this.hitBox.Y = nextPosition.Y;
            if (Canvas.GetLeft(this.bullet) <= -100)
            {
                MyCanvas.Children.Remove(this.bullet);
                memoryCleaner.AddObject(this);
            }
            else if (Canvas.GetLeft(this.bullet) - 100 > MyCanvas.Width)
            {
                MyCanvas.Children.Remove(this.bullet);
                memoryCleaner.AddObject(this);
            }
            else if (Canvas.GetTop(this.bullet) <= -100)
            {
                MyCanvas.Children.Remove(this.bullet);
                memoryCleaner.AddObject(this);
            }
            else if (Canvas.GetTop(this.bullet) - 100 > MyCanvas.Height)
            {
                MyCanvas.Children.Remove(this.bullet);
                memoryCleaner.AddObject(this);
            }
        }

        protected Point CalculateTrajectory()
        {
            
            double angle = Math.Atan2(this.mousePosition.Y - this.startPosition.Y, this.mousePosition.X - this.startPosition.X);

            double vX = bulletSpeed * Math.Cos(angle);
            double vY = bulletSpeed * Math.Sin(angle);

            double x = this.startPosition.X + vX * liveTime;
            double y = this.startPosition.Y + vY * liveTime;

            this.liveTime += 1;

            return new Point(x, y);
        }


    }
    //hitBox = new Rect(nextPosition.X, nextPosition.Y, hitBox.Width, hitBox.Height);
    //this.CheckCollisionWithWall(wall, gameObjects, MyCanvas);
}
