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
using Game.GameSystem;
using Game.Objects.Items;
using Game.Objects.Weapons;

namespace Game.Objects.Other.Shops
{
    internal abstract class Shop : GameObject
    {
        protected int price;
        protected List<GameObject> gameObjects;

        protected Canvas gameBoard;
        protected Point position;
        protected Label InteractionLabel;

        protected Item product;
        protected Shop(Point position, Canvas gameBoard, List<GameObject> gameObjects) : base(position, gameBoard, gameObjects)
        {
            this.gameObjects = gameObjects;
            this.gameBoard = gameBoard;
            this.position = position;
            this.hitBox.X -= this.body.Width;
            this.hitBox.Y -= this.body.Height;
            this.hitBox.Width = this.body.Width * 3;
            this.hitBox.Height = this.body.Height * 3;

            this.body.Fill = new SolidColorBrush(Colors.Orange);

            this.InteractionLabel = new Label
            {
                Tag = "Off",
                Content = " ",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.Black),
                Height = 40,
            };
        }

        public virtual void InteractionWithObject(Player player, Canvas gameBoard, Canvas gameInterface)
        {
            if (this.hitBox.IntersectsWith(player.hitBox))
            {
                Canvas.SetLeft(this.InteractionLabel, gameInterface.Width / 2);
                Canvas.SetTop(this.InteractionLabel, gameInterface.Height - this.InteractionLabel.Height);
                if (this.InteractionLabel.Tag == "Off")
                {
                    gameInterface.Children.Add(this.InteractionLabel);
                    this.InteractionLabel.Tag = "On";
                }
                if (player.Interaction && this.price <= player.Money)
                {
                    player.Interaction = false;
                    ProductSold(player);
                    CreateNewProduct();
                }
            }
            else if (this.InteractionLabel.Tag == "On")
            {
                gameInterface.Children.Remove(this.InteractionLabel);
                this.InteractionLabel.Tag = "Off";
            }
        }
        public abstract void CreateNewProduct();

        public virtual void ProductSold(Player player)
        {
            player.Money -= this.price;
            this.product.ItemAction(player);
        }
    }
}
