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

namespace Game.Objects
{
    internal abstract class GameObject
    {
        protected Rectangle body;
        public Rect hitBox;
        public int Health { get; set; }

        public GameObject(Point position, Canvas GameBoard, List<GameObject> gameObjects)
        {
            gameObjects.Add(this);
            this.body = new Rectangle
            {
                Height = 50,
                Width = 50,
            };
            Canvas.SetLeft(this.body, position.X);
            Canvas.SetTop(this.body, position.Y);
            this.hitBox = new Rect(position.X, position.Y, body.Width, body.Height);
            GameBoard.Children.Add(this.body);
        }

        public virtual void CheckDeath(MemoryCleaner memoryCleaner, Canvas GameBoard)
        {
            if (Health <= 0)
            {
                memoryCleaner.AddObject(this);
                GameBoard.Children.Remove(this.body);

            }
        }
    }
}
