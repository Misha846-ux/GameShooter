using Game.Creatures.Players;
using Game.Objects;
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
using System.Xml.Linq;
using Game.Bullets;
using System.Numerics;
using Game.Objects.Walls.BreakableWalls;

namespace Game.Bullets.EnemyBullets
{
    class EnemOrdinaryBullet : Bullet
    {
        public EnemOrdinaryBullet(Point startPosition, Point mousePosition, int damage) : base(startPosition, mousePosition, damage) { }
        public void CheckCollisionWihtPlayer(Player player, List<Bullet> gameObjects, Canvas GameBoard)
        {
            if (player.hitBox.IntersectsWith(this.hitBox))
            {
                this.Death(gameObjects, GameBoard);
                GameBoard.Children.Remove(this.bullet);
                player.ReduceHealth(this.Damage);
            }
        }
        public void BulletMove(Canvas MyCanvas, Player player, List<Bullet> gameObjects, WoodenWall wall)
        {
            
            Point nextPosition = new Point(this.mousePosition.X - this.startPosition.X, this.mousePosition.Y - this.startPosition.Y);
            while (Math.Abs(nextPosition.X) > this.bulletSpeed || Math.Abs(nextPosition.Y) > this.bulletSpeed)
            {
                nextPosition.X *= 0.5;
                nextPosition.Y *= 0.5;
            }
            Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) + nextPosition.X);
            Canvas.SetTop(bullet, Canvas.GetTop(bullet) + nextPosition.Y);
            hitBox = new Rect(nextPosition.X, nextPosition.Y, hitBox.Width, hitBox.Height);
            this.CheckCollisionWihtPlayer(player, gameObjects, MyCanvas);
            this.CheckCollisionWithWall(wall, gameObjects, MyCanvas);


            if (Canvas.GetLeft(this.bullet) <= -100)
            {
                MyCanvas.Children.Remove(this.bullet);
            }
            else if (Canvas.GetLeft(this.bullet) - 100 > this.BoardWhidth)
            {
                MyCanvas.Children.Remove(this.bullet);
            }
            else if (Canvas.GetTop(this.bullet) <= -100)
            {
                MyCanvas.Children.Remove(this.bullet);
            }
            else if (Canvas.GetTop(this.bullet) - 100 > this.BoardHeight)
            {
                MyCanvas.Children.Remove(this.bullet);
            }
        }
    }
}
