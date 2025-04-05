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
    internal class WoodenWall: GameObject, Wall
    {
        public WoodenWall(Point position, Canvas GameBoard, List<GameObject> gameObjects):base(position, GameBoard, gameObjects) 
        {
            ImageBrush WoodenWallTexture = new ImageBrush();
            WoodenWallTexture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Textures/WoodenWall.png"));
            this.body.Fill = WoodenWallTexture;
            this.body.Tag = "WoodenWall";
            this.Health = 100;

        }
        public void ReduceHealth(int damage)
        {
            Health = Health - damage;
        }
    }
}
