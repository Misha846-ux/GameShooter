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
using Game.Creatures.Enemies;
using Game.GameSystem;

namespace Game.Objects.Other
{
    internal class EnemySummoningPoint: GameObject
    {
        private readonly int _summonTime;
        private int _summonTimer;
        private readonly Point _spawnPosition;
        public EnemySummoningPoint(Point position, Canvas GameBoard, List<GameObject> gameObjects)
        {
            gameObjects.Add(this);
            this.body.Fill = new SolidColorBrush(Colors.Gray);
            Canvas.SetLeft(this.body, position.X);
            Canvas.SetTop(this.body, position.Y);
            this.hitBox = new Rect(position.X, position.Y, body.Width, body.Height);
            GameBoard.Children.Add(this.body);
            this._summonTime = 500;
            this._summonTimer = 500;
            this._spawnPosition = new Point(position.X, position.Y);
        }

        public void SummonEnemy(Canvas GameBoard, List<Enemy> enemies)
        {
            if(this._summonTimer >= this._summonTime)
            {
                Enemy newEnemy = new Enemy(this._spawnPosition, GameBoard, enemies);
                this._summonTimer = 0;
            }
            else
            {
                this._summonTimer += 1;
            }
        }

        public override void CheckDeath(MemoryCleaner memoryCleaner, Canvas GameBoard) { }
    }
}
