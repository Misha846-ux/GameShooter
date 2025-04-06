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

using Game;
using Game.Creatures;
using Game.Creatures.Players;
using Game.Creatures.Enemies;
using Game.Bullets;
using Game.Objects;
using Game.Objects.Walls.UnbreakableWalls;
using Game.Objects.Walls.BreakableWalls;
using Game.Objects.Other;
using Game.Bullets.PlayerBullets;
using Game.Bullets.EnemyBullets;

namespace Game.Objects.Other
{
    internal class ResourceDrill : GameObject
    {
        private int time;
        private int timer;
        public ResourceDrill(Point position, Canvas GameBoard, List<GameObject> gameObjects) : base(position, GameBoard, gameObjects)
        {
            this.body.Fill = new SolidColorBrush(Colors.Yellow);
            this.body.Width = 200;
            this.body.Height = 200;
            this.hitBox.Width = this.body.Width;
            this.hitBox.Height = this.body.Height;
            this.Health = 500;

            this.time = 10;
            this.timer = 0;
        }

        public void DrillWorking(Player player)
        {
            if(this.timer >= this.time)
            {
                this.timer = 0;
                player.Money += 1;
            } 
            else
            {
                this.timer += 1;
            }
        }
    }
}
