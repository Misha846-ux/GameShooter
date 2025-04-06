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
using Game.Objects.Weapons;
using Game.Objects.Items.FiniteItems;

namespace Game.Objects.Other.Shops.StaticShop
{
    internal class HealingPotionShop : Shop
    {
        public HealingPotionShop(Point position, Canvas gameBoard, List<GameObject> gameObjects) : base(position, gameBoard, gameObjects)
        {
            this.price = 100;
            CreateNewProduct();
        }

        public override void CreateNewProduct()
        {
            this.product = new HealingPotion(position, gameBoard, gameObjects);
            this.product.СanRaised = false;
            this.InteractionLabel.Content = KeysBinds.Interaction + " to buy for " + price;
        }
    }
}
