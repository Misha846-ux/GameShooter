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
using Game.Bullets;
using Game.Weapons;
using Game.Weapons.PlayerWeapons;

namespace Game.Creatures.Players
{
    internal class Player: Creature
    {
        public Point MousePosition {  get; set; }// regarding Canvas
        private Gun gun;
        private Label ammoCounter;
        private bool moveUp;
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;
        private bool shot;
        private bool fastReload;

        public Player(int boardWhidth, int boardHeight, Canvas GameBoard, Canvas Interfase)
        {
            this.body.Fill = new SolidColorBrush(Colors.White);
            GameBoard.Children.Add(this.body);
            this.BoardWhidth = boardWhidth;
            this.BoardHeight = boardHeight;
            this.creatureSpeed = 10;
            Canvas.SetLeft(this.body, boardWhidth / 10 * 10);
            Canvas.SetTop(this.body, boardHeight / 10 * 10 );
            this.moveUp = false;
            this.moveDown = false;
            this.moveLeft = false;
            this.moveRight = false;
            this.shot = false;
            this.fastReload = false;

            gun = new TestGun();
            gun.PlayerPosition = new Point(Canvas.GetLeft(this.body), Canvas.GetTop(this.body));

            this.ammoCounter = new Label
            {
                Name = "AmmoCounter",
                Content = "Ammo: ",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White)
            };
            Canvas.SetLeft(this.ammoCounter, 0);
            Canvas.SetTop(this.ammoCounter, 0);
            Interfase.Children.Add(this.ammoCounter);
        }

        public void KeyDownRead(KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                this.moveUp = true;
            }
            else if (e.Key == Key.S)
            {
                this.moveDown = true;
            }
            else if (e.Key == Key.D)
            {
                this.moveRight = true;
            }
            else if (e.Key == Key.A)
            {
                this.moveLeft = true;
            }
            else if (e.Key == Key.R)
            {
                this.fastReload = true;
            }
        }
        public void KeyUpRead(KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                this.moveUp = false;
            }
            else if (e.Key == Key.S)
            {
                this.moveDown = false;
            }
            else if (e.Key == Key.D)
            {
                this.moveRight = false;
            }
            else if (e.Key == Key.A)
            {
                this.moveLeft = false;
            }
        }
        public void ShowInterface(Canvas MyCanvas)
        {
            if(this.ammoCounter == null)
            {
                
            }
            this.ammoCounter.Content = "Ammo: " + gun.Ammo + " / " + gun.MaxAmmo;
        }
        public void MouseDownRead(MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                this.shot = true;
            }
        }

        public void MouseUpRead(MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Released)
            {
                this.shot = false;
            }
        }

        public void PlayerMove(Canvas GameBoard)
        {
            if (this.moveUp && Canvas.GetTop(this.body) > 0)
            {
                Canvas.SetTop(this.body, Canvas.GetTop(this.body) - this.creatureSpeed);
                Canvas.SetTop(GameBoard, Canvas.GetTop(GameBoard) + this.creatureSpeed);
            }
            else if (this.moveDown && Canvas.GetTop(this.body)+50 < this.BoardHeight)
            {
                Canvas.SetTop(this.body, Canvas.GetTop(this.body) + this.creatureSpeed);
                Canvas.SetTop(GameBoard, Canvas.GetTop(GameBoard) - this.creatureSpeed);
            }
            if (this.moveRight && Canvas.GetLeft(this.body) + 50 < this.BoardWhidth)
            {
                Canvas.SetLeft(this.body, Canvas.GetLeft(this.body) + this.creatureSpeed);
                Canvas.SetLeft(GameBoard, Canvas.GetLeft(GameBoard) - this.creatureSpeed);
            }
            else if (this.moveLeft && Canvas.GetLeft(this.body) > 0)
            {
                Canvas.SetLeft(this.body, Canvas.GetLeft(this.body) - this.creatureSpeed);
                Canvas.SetLeft(GameBoard, Canvas.GetLeft(GameBoard) + this.creatureSpeed);
            }
            gun.PlayerPosition = new Point(Canvas.GetLeft(this.body), Canvas.GetTop(this.body));
        }

        public void Fire(List<Bullet> bullets, Canvas MyCanvas)
        {
            if (this.fastReload)
            {
                gun.FastReload();
                this.fastReload = false;
            }
            gun.GunReload();
            if (shot)
            {
                gun.Shot(this.MousePosition, bullets, MyCanvas);
            }

        }
    }
}
