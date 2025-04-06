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
using Game.Objects.Items.WeaponsAsItems;
using Game.Objects.Weapons.PlayerWeapons;
using System.Reflection;

namespace Game.Objects.Other.Shops.WeaponShop
{
    internal class WeaponShop : Shop
    {
        private struct ProductAndPrice
        {
            public Type type;
            public int price;

            public ProductAndPrice(Type type, int price)
            {
                this.price = price;
                this.type = type;
            }

            public Gun CreateNewProduct()
            {
                object obj = Activator.CreateInstance(type);
                return obj as Gun;
            }


        }

        private List<ProductAndPrice> products;
        private int productNumber;
        private Random random;
        public WeaponShop(Point position, Canvas gameBoard, List<GameObject> gameObjects) : base(position, gameBoard, gameObjects)
        {
            this.random = new Random();

            this.products = new List<ProductAndPrice>
            {
                new ProductAndPrice(typeof(PlayerGun) , 400)
            };

            CreateNewProduct();
        }

        public override void CreateNewProduct()
        {
            this.productNumber = random.Next(0,this.products.Count);
            this.price = this.products[this.productNumber].price;

            this.product = new WeaponAsItem(position, gameBoard, gameObjects, this.products[this.productNumber].CreateNewProduct());
            
            
            this.product.СanRaised = false;
            this.InteractionLabel.Content = KeysBinds.Interaction + " to buy for " + price;
        }
    }
}
