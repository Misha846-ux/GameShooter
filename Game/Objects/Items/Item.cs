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

namespace Game.Objects.Items
{
    internal abstract class Item: GameObject
    {
        public bool СanRaised {  get; set; }
        public Item()
        {
            this.body.Width = 20;
            this.body.Height = 20;
            this.СanRaised = true;
        }

        public void InteractionWithObject(Player player, Canvas gameInterface)
        {
            
        }
    }
}
