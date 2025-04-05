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

namespace Game.Objects.Items.WeaponsAsItems
{
    internal class WeaponAsItem: Item
    {
        public Gun gun { get; set; }

        // Needed for cases when the slot is already occupied
        private List<GameObject> gameObjects;
        private Canvas gameBoard;
        //
        
        public WeaponAsItem(Point spawnPosition, Canvas gameBoard, List<GameObject> gameObjects, Gun gunType) : base(spawnPosition, gameBoard, gameObjects)
        {
            this.gun = gunType;
            this.gameObjects = gameObjects;
            this.gameBoard = gameBoard;
            this.body.Fill = new SolidColorBrush(Colors.Brown);
        }


        protected override void ItemAction(Player player)
        {
            player.SetSelectedSlot(this.gun, gameBoard, gameObjects);
        }
    }
}
