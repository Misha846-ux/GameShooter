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
using Game.Creatures;
using Game.Creatures.Players;
using Game.Creatures.Enemies;
using Game.Bullets;
using Game.Objects;
using Game.Objects.Walls.UnbreakableWalls;
using Game.Objects.Walls.BreakableWalls;
using Game.Objects.Other;

namespace Game.GameSystem
{
    internal class MemoryCleaner
    {
        //Needed to remove non-existent objects from others lists.
        private List<object> _memory;

        public MemoryCleaner()
        {
            _memory = new List<object>();
        }

        public void AddObject(object obj)
        {
            _memory.Add(obj);
        }

        public void Clean(List<Bullet> bullets, List<GameObject> gameObjects, List<Enemy> enemies)
        {
            foreach (object obj in _memory)
            {
                if (obj is Bullet)
                {
                    bullets.Remove((Bullet)obj);
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
            _memory.Clear();
        }
    }
}
