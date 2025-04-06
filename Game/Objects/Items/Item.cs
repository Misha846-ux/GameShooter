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

namespace Game.Objects.Items
{
    internal abstract class Item: GameObject
    {
        public bool СanRaised {  get; set; }
        protected Label InteractionLabel;
        protected Rect interactionZone;
        public Item(Point spawnPosition, Canvas gameBoard, List<GameObject> gameObjects): base(spawnPosition, gameBoard, gameObjects)
        {

            this.body.Width = 20;
            this.body.Height = 20;
            this.СanRaised = true;
            this.InteractionLabel = new Label
            {
                Tag = "Off",
                Content = KeysBinds.Interaction + " for interact",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.Black),
                Height = 40,
            };
            this.hitBox.Width = this.body.Width;
            this.hitBox.Height = this.body.Height;
            
            this.interactionZone = new Rect(spawnPosition.X - this.body.Width, spawnPosition.Y - this.body.Height, this.body.Width * 3, this.body.Height * 3);
        }

        public virtual void InteractionWithObject(Player player, Canvas gameBoard, Canvas gameInterface, MemoryCleaner memoryCleaner)
        {
            if (this.СanRaised && this.interactionZone.IntersectsWith(player.hitBox))
            {
                Canvas.SetLeft(this.InteractionLabel, gameInterface.Width / 2);
                Canvas.SetTop(this.InteractionLabel, gameInterface.Height - this.InteractionLabel.Height);
                if(this.InteractionLabel.Tag == "Off")
                {
                    gameInterface.Children.Add(this.InteractionLabel);
                    this.InteractionLabel.Tag = "On";
                }
                if (player.Interaction)
                {
                    gameInterface.Children.Remove(this.InteractionLabel);
                    gameBoard.Children.Remove(this.body);
                    memoryCleaner.AddObject(this);
                    player.Interaction = false;
                    ItemAction(player);
                }
            }
            else if(this.InteractionLabel.Tag == "On")
            {
                gameInterface.Children.Remove(this.InteractionLabel);
                this.InteractionLabel.Tag = "Off";
            }
        }

        public abstract void ItemAction(Player player);
    }
}
