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
        private TestGun gun;
        private Label ammoCounter;
        private bool moveUp;
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;
        private bool shot;

        public Player(int boardWhidth, int boardHeight)
        {
            this.body.Fill = new SolidColorBrush(Colors.White);
            this.BoardWhidth = boardWhidth;
            this.BoardHeight = boardHeight;
            this.creatureSpeed = 10;
            Canvas.SetLeft(this.body, boardWhidth / 2);
            Canvas.SetTop(this.body, boardHeight / 2);
            this.moveUp = false;
            this.moveDown = false;
            this.moveLeft = false;
            this.moveRight = false;
            this.shot = false;

            gun = new TestGun();
            gun.PlayerPosition = new Point(Canvas.GetLeft(this.body), Canvas.GetTop(this.body));
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
                MyCanvas.Children.Add(this.ammoCounter);
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

        public void PlayerMove()
        {
            if (this.moveUp && Canvas.GetTop(this.body) > 0)
            {
                Canvas.SetTop(this.body, Canvas.GetTop(this.body) - this.creatureSpeed);
            }
            else if (this.moveDown && Canvas.GetTop(this.body) + 90 < this.BoardHeight)
            {
                Canvas.SetTop(this.body, Canvas.GetTop(this.body) + this.creatureSpeed);
            }
            if (this.moveRight && Canvas.GetLeft(this.body) + 60 < this.BoardWhidth)
            {
                Canvas.SetLeft(this.body, Canvas.GetLeft(this.body) + this.creatureSpeed);
            }
            else if (this.moveLeft && Canvas.GetLeft(this.body) > 0)
            {
                Canvas.SetLeft(this.body, Canvas.GetLeft(this.body) - this.creatureSpeed);
            }
            gun.PlayerPosition = new Point(Canvas.GetLeft(this.body), Canvas.GetTop(this.body));
        }

        public void Fire(List<Bullet> bullets, Canvas MyCanvas)
        {
            gun.GunReload();
            if (shot)
            {
                gun.Shot(this.MousePosition, bullets, MyCanvas);
            }

        }
    }
}
