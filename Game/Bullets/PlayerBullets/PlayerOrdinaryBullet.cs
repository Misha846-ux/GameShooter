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
using Game.Objects.Items;
using Game.Objects;
using Game.Objects.Walls.UnbreakableWalls;

namespace Game.Bullets.PlayerBullets
{
    internal class PlayerOrdinaryBullet : Bullet
    {
        public PlayerOrdinaryBullet(Point startPosition, Point mousePosition, int damage, List<PlayerOrdinaryBullet> bullets) : base(startPosition, mousePosition, damage)
        {
            bullets.Add(this);
            this.bullet.Fill = new SolidColorBrush(Colors.Blue);
        }

        public void CheckCollision( List<Enemy> enemies, MemoryCleaner memoryCleaner, Canvas GameBoard, List<GameObject> gameObjects)
        {
            foreach (var item in enemies)
            {
                if(this.hitBox.IntersectsWith(item.hitBox))
                {
                    //there is already an empty method in the pool for game objects. When game objects have the ability to take damage, fill this method
                    OnBulletHit(item, memoryCleaner, GameBoard);
                }
            }
            foreach (var item in gameObjects)
            {
                if (item is WoodenWall)
                {
                    if (this.hitBox.IntersectsWith(item.hitBox))
                    {
                        item.Health -= this.Damage;
                        item.CheckDeath(memoryCleaner, GameBoard);
                        memoryCleaner.AddObject(this);
                        GameBoard.Children.Remove(this.bullet);
                    }
                }
                else if(item is StoneWall)
                {
                    if (this.hitBox.IntersectsWith(item.hitBox))
                    {
                        memoryCleaner.AddObject(this);
                        GameBoard.Children.Remove(this.bullet);
                    }
                }
            }
        }

        //It is needed so that later it would be easier to create bullets with different effects.
        //For example, so that it would be easier to add the effect of an explosion or a piercing through.
        protected void OnBulletHit(Enemy enemy, MemoryCleaner memoryCleaner, Canvas GameBoard)
        {
            enemy.ReduceHealth(this.Damage);
            memoryCleaner.AddObject(this);
            GameBoard.Children.Remove(this.bullet);
            enemy.CheckDeath(memoryCleaner, GameBoard);

        }
       
     
    }
}
