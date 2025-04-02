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
using Game.Creatures.Players;

namespace Game.Creatures
{
    
    internal abstract class Creature
    {
        public Rect hitBox { get; set; }
        public int Health { get; set; }
        protected Rectangle body;
        protected int creatureSpeed;
        public int BoardWhidth {  get; set; }
        public int BoardHeight { get; set; }

        public Creature()
        {
            body = new Rectangle
            {
                Width = 50,
                Height = 50,
            };

            BoardWhidth = 500;
            BoardHeight = 800;

        }

        public Rectangle GetBody()
        {
            return this.body;
        }
        public Point GetPosition()
        {
            return new Point(Canvas.GetLeft(this.body), Canvas.GetTop(this.body));
        }

        public void ReduceHealth(int damage)
        {
            this.Health -= damage;
        }
    }
}
