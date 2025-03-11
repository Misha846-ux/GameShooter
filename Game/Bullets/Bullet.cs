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


namespace Game.Bullets
{
    internal class Bullet
    {
        protected Rectangle bullet;
        protected Point startPosition; // stores the position the player is in when the bullet is launched
        protected Point mousePosition; // stores the position the mouse was in when the bullet was launched
        protected int bulletSpeed;
        public int BoardWhidth { get; set; }
        public int BoardHeight { get; set; }
        public Bullet(Point startPosition, Point mousePosition)
        {
            this.bullet = new Rectangle
            {
                Tag = "bullet",
                Width = 20,
                Height = 20,
                Fill = new SolidColorBrush(Colors.Red)
            };
            this.bulletSpeed = 10;
            this.startPosition = startPosition;
            this.mousePosition = mousePosition;
            Canvas.SetLeft(this.bullet, startPosition.X);
            Canvas.SetTop(this.bullet, startPosition.Y);
        }

        public Rectangle GetBullet()
        {
            return this.bullet;
        }

        public void BulletMove(Canvas MyCanvas)
        {
            Point nextPosition = new Point(this.mousePosition.X - this.startPosition.X, this.mousePosition.Y - this.startPosition.Y);
            while (Math.Abs(nextPosition.X) > this.bulletSpeed || Math.Abs(nextPosition.Y) > this.bulletSpeed)
            {
                nextPosition.X *= 0.5;
                nextPosition.Y *= 0.5;
            }
            Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) + nextPosition.X);
            Canvas.SetTop(bullet, Canvas.GetTop(bullet) + nextPosition.Y);
            if (Canvas.GetLeft(this.bullet) <= 0)
            {
                MyCanvas.Children.Remove(this.bullet);
            }
            else if(Canvas.GetLeft(this.bullet) > this.BoardWhidth)
            {
                MyCanvas.Children.Remove(this.bullet);
            }
            else if (Canvas.GetTop(this.bullet) <= 0)
            {
                MyCanvas.Children.Remove(this.bullet);
            }
            else if (Canvas.GetTop(this.bullet) > this.BoardHeight)
            {
                MyCanvas.Children.Remove(this.bullet);
            }
        }


    }
}
