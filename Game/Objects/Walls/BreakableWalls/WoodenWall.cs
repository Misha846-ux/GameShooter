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

namespace Game.Objects.Walls.BreakableWalls
{
    internal class WoodenWall: GameObject
    {
        public WoodenWall(Point position, Canvas GameBoard, List<GameObject> gameObjects)
        {
            gameObjects.Add(this);
            ImageBrush WoodenWallTexture = new ImageBrush();
            WoodenWallTexture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Textures/WoodenWall.png"));
            this.body.Fill = WoodenWallTexture;
            this.body.Tag = "WoodenWall";
            Canvas.SetLeft(this.body, position.X);
            Canvas.SetTop(this.body, position.Y);
            this.hitBox = new Rect(position.X, position.Y, body.Width, body.Height);
            GameBoard.Children.Add(this.body);
            this.Health = 100;
        }
        public void ReduceHealth(int damage)
        {
            Health = Health - damage;
        }
    }
}
