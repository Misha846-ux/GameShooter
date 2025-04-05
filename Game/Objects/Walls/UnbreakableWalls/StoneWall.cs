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
using Game.GameSystem;

namespace Game.Objects.Walls.UnbreakableWalls
{
    internal class StoneWall : GameObject, Wall
    {
        public StoneWall(Point position, Canvas GameBoard, List<GameObject> gameObjects): base(position, GameBoard, gameObjects)
        {
            ImageBrush stoneWallTexture = new ImageBrush();
            stoneWallTexture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Textures/StoneWall.png"));
            this.body.Fill = stoneWallTexture;
            this.body.Tag = "stoneWall";
            

        }

        public override void CheckDeath(MemoryCleaner memoryCleaner, Canvas GameBoard)
        {

        }
    }
}