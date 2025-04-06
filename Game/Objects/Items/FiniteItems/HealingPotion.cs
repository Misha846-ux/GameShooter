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

namespace Game.Objects.Items.FiniteItems
{
    internal class HealingPotion: Item
    {
        public HealingPotion(Point spawnPosition, Canvas gameBoard, List<GameObject> gameObjects): base(spawnPosition, gameBoard, gameObjects)
        {
            this.body.Fill = new SolidColorBrush(Colors.Aqua);
        }

        public override void ItemAction(Player player)
        {
            player.Health += 100;
        }
    }
}
