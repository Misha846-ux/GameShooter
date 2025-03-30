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

namespace Game.Objects
{
    internal abstract class GameObject
    {
        protected Rectangle body;
        public Rect hitBox { get; set; }
        public int Health { get; set; }

        public GameObject()
        {
            this.body = new Rectangle
            {
                Height = 50,
                Width = 50,
            };
            
        }

        public virtual void CheckDeath(List<GameObject> gameObjects, Canvas GameBoard)
        {
            if(Health <= 0)
            {
                gameObjects.Remove(this);
                GameBoard.Children.Remove(this.body);
                
            }
        }
    }
}
