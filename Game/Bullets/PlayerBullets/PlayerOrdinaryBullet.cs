using Game.Creatures.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Game.Creatures.Enemies;
using Game.Objects.Walls.BreakableWalls;
using static System.Net.Mime.MediaTypeNames;
using Game.Bullets.EnemyBullets;

namespace Game.Bullets.PlayerBullets
{
    internal class PlayerOrdinaryBullet : Bullet
    {
        public PlayerOrdinaryBullet(Point startPosition, Point mousePosition, int damage, List<PlayerOrdinaryBullet> bullets) : base(startPosition, mousePosition, damage)
        {
            bullets.Add(this);
        }
        //public void Death(List<PlayerOrdinaryBullet> gameObjects, Canvas GameBoard)
        //{
        //    gameObjects.Remove((PlayerOrdinaryBullet)this);
        //    GameBoard.Children.Remove(this.bullet);
        //    //GameBoard.Children.Remove(this.hitBox);
        //}
        //public void CheckCollisionWithWall(WoodenWall wall, List<PlayerOrdinaryBullet> gameObjects, Canvas GameBoard)
        //{
        //    if (wall.hitBox.IntersectsWith(this.hitBox))
        //    {
        //        this.Death(gameObjects, GameBoard);
        //        GameBoard.Children.Remove(bullet);
        //        wall.ReduceHealth(this.Damage);
        //    }
        //}
        public bool CheckCollisionWihtEnemy(Enemy enemy, List<PlayerOrdinaryBullet> gameObjects, Canvas GameBoard)
        {
            if (enemy.hitBox.IntersectsWith(this.hitBox))
                return true;
            else
                return false;
        }
        public void BulletMove(MemoryCleaner memoryCleaner, Canvas MyCanvas, List<PlayerOrdinaryBullet> gameObjects)
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
            //this.CheckCollisionWihtEnemy(enemy, gameObjects, MyCanvas);
            //this.CheckCollisionWithWall(wall, gameObjects, MyCanvas);


            if (Canvas.GetLeft(this.bullet) <= -100)
            {
                MyCanvas.Children.Remove(this.bullet);
                memoryCleaner.AddObject(this);
            }
            else if (Canvas.GetLeft(this.bullet) - 100 > this.BoardWhidth)
            {
                MyCanvas.Children.Remove(this.bullet);
                memoryCleaner.AddObject(this);
            }
            else if (Canvas.GetTop(this.bullet) <= -100)
            {
                MyCanvas.Children.Remove(this.bullet);
                memoryCleaner.AddObject(this);
            }
            else if (Canvas.GetTop(this.bullet) - 100 > this.BoardHeight)
            {
                MyCanvas.Children.Remove(this.bullet);
                memoryCleaner.AddObject(this);
            }
        }
    }
}
