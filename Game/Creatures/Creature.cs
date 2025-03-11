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
        protected Rectangle body;
        protected int creatureSpeed;
        public int BoardWhidth {  get; set; }
        public int BoardHeight { get; set; }

        public Creature()
        {
            body = new Rectangle
            {
                Name = "player",
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
    }
}
