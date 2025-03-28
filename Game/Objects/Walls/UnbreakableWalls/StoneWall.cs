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

namespace Game.Objects.Walls.UnbreakableWalls
{
    internal class StoneWall: GameObject
    {
        public StoneWall(Point position, Canvas GameBoard, List<GameObject> gameObjects)
        {
            gameObjects.Add(this);
            ImageBrush stoneWallTexture = new ImageBrush();
            stoneWallTexture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Textures/StoneWall.png"));
            this.body.Fill = stoneWallTexture;
            this.body.Tag = "stoneWall";
            Canvas.SetLeft(this.body, position.X);
            Canvas.SetTop(this.body, position.Y);
            this.hitBox = new Rect(position.X, position.Y, body.Width, body.Height);
            GameBoard.Children.Add(this.body);

        }

        public override void CheckDeath(MemoryCleaner memoryCleaner, Canvas GameBoard)
        {

        }
    }
}
