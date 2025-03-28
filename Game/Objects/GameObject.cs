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
        protected Rect hitBox;
        public int Health { get; set; }

        public GameObject()
        {
            this.body = new Rectangle
            {
                Height = 50,
                Width = 50,
            };
            
        }

        public virtual void CheckDeath(MemoryCleaner memoryCleaner, Canvas GameBoard)
        {
            if(Health <= 0)
            {
                memoryCleaner.AddObject(this);
                GameBoard.Children.Remove(this.body);
                
            }
        }
    }
}
