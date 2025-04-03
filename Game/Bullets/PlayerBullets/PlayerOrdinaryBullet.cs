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
using System.Windows.Media;
using Game.GameSystem;

namespace Game.Bullets.PlayerBullets
{
    internal class PlayerOrdinaryBullet : Bullet
    {
        public PlayerOrdinaryBullet(Point startPosition, Point mousePosition, int damage, List<PlayerOrdinaryBullet> bullets) : base(startPosition, mousePosition, damage)
        {
            bullets.Add(this);
            this.bullet.Fill = new SolidColorBrush(Colors.Blue);
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
        public void CheckCollisionWihtEnemy( List<Enemy> gameObjects, MemoryCleaner memoryCleaner, Canvas GameBoard)
        {
            foreach (var item in gameObjects)
            {
                if(this.hitBox.IntersectsWith(item.hitBox))
                {
                    memoryCleaner.AddObject(this);
                    GameBoard.Children.Remove(this.bullet);
                    item.ReduceHealth(this.Damage);
                }
            }
        }
       
     
    }
}
