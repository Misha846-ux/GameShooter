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
using Game.GameSystem;
using Game.Creatures.Enemies;
using Game.Objects.Walls;

namespace Game.Bullets.EnemyBullets
{
    internal class EnemyOrdinaryBullet : Bullet
    {
        public EnemyOrdinaryBullet(Point startPosition, Point mousePosition, int damage, List<EnemyOrdinaryBullet> bullets) : base(startPosition, mousePosition, damage) 
        {
            bullets.Add(this);
        }
        public void CheckCollision(Player player, MemoryCleaner memoryCleaner, Canvas GameBoard, List<GameObject> gameObjects)
        {
            if (this.hitBox.IntersectsWith(player.hitBox))
            {
                //there is already an empty method in the pool for game objects. When game objects have the ability to take damage, fill this method
                OnBulletHit(player, memoryCleaner, GameBoard);
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
            }
        }

        //It is needed so that later it would be easier to create bullets with different effects.
        //For example, so that it would be easier to add the effect of an explosion or a piercing through.
        protected void OnBulletHit(Player player, MemoryCleaner memoryCleaner, Canvas GameBoard)
        {
            player.ReduceHealth(this.Damage);
            memoryCleaner.AddObject(this);
            GameBoard.Children.Remove(this.bullet);
        }
    }
}
