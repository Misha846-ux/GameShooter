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

namespace Game.Bullets.EnemyBullets
{
    internal class EnemOrdinaryBullet : Bullet
    {
        public EnemOrdinaryBullet(Point startPosition, Point mousePosition, int damage, List<EnemOrdinaryBullet> bullets) : base(startPosition, mousePosition, damage) 
        {
            bullets.Add(this);
        }
        public void CheckCollisionWihtPlayer(Player player, MemoryCleaner memoryCleaner, Canvas GameBoard)
        {
            if (this.hitBox.IntersectsWith(player.hitBox))
            {
                memoryCleaner.AddObject(this);
                GameBoard.Children.Remove(this.bullet);
                player.ReduceHealth(this.Damage);
            }
        }
       
        
    }
}
