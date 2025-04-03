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

using Game;
using Game.Creatures;
using Game.Creatures.Players;
using Game.Creatures.Enemies;
using Game.Bullets;
using Game.Objects;
using Game.Objects.Walls.UnbreakableWalls;
using Game.Objects.Walls.BreakableWalls;
using Game.Objects.Other;
using Game.Bullets.PlayerBullets;
using Game.Bullets.EnemyBullets;

namespace Game.GameSystem
{
    internal class MemoryCleaner
    {

        private List<object> _memory;

        public MemoryCleaner()
        {
            _memory = new List<object>();
        }

        public void AddObject(object obj)
        {
            _memory.Add(obj);
        }

        public void Clean(List<PlayerOrdinaryBullet> playerBullets, List<EnemOrdinaryBullet> enemyBullets, List<GameObject> gameObjects, List<Enemy> enemies)
        {
            foreach (object obj in _memory)
            {
                if (obj is EnemOrdinaryBullet)
                {
                    enemyBullets.Remove((EnemOrdinaryBullet)obj);
                }
                else if (obj is PlayerOrdinaryBullet)
                {
                    playerBullets.Remove((PlayerOrdinaryBullet)obj);
                }
                else if (obj is Enemy)
                {
                    enemies.Remove((Enemy)obj);
                }
                else if (obj is GameObject)
                {
                    gameObjects.Remove((GameObject)obj);
                }

            }
            this._memory.Clear();
        }
    }
}