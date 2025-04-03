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
using Game.Objects.Weapons;
using Game.Objects.Weapons.PlayerWeapons;
using System.Xml.Linq;
using Game.Bullets.PlayerBullets;

namespace Game.Creatures.Players
{
    internal class Player: Creature
    {
        public Point MousePosition {  get; set; }// regarding Canvas
        private Gun gun;
        private Label ammoCounter;
        private Label healthText;
        public bool Interaction {  get; set; }
        private bool moveUp;
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;
        private bool shot;
        private bool fastReload;

        public Player(int boardWhidth, int boardHeight, Canvas GameBoard, Canvas Interfase)
        {
            this.body.Fill = new SolidColorBrush(Colors.White);
            this.body.Name = "player";
            GameBoard.Children.Add(this.body);
            this.BoardWhidth = boardWhidth;
            this.BoardHeight = boardHeight;
            this.creatureSpeed = 10;
            Canvas.SetLeft(this.body, boardWhidth / 10 * 10);
            Canvas.SetTop(this.body, boardHeight / 10 * 10 );
            this.Interaction = false;
            this.moveUp = false;
            this.moveDown = false;
            this.moveLeft = false;
            this.moveRight = false;
            this.shot = false;
            this.fastReload = false;
            this.Health = 100;

            gun = new PlayerGun();
            gun.PlayerPosition = new Point(Canvas.GetLeft(this.body), Canvas.GetTop(this.body));

            this.ammoCounter = new Label
            {
                Name = "AmmoCounter",
                Content = "Ammo: ",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White)
            };

            this.healthText = new Label
            {
                Name = "HealthText",
                Content = " ",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White)
            };

            Canvas.SetLeft(this.ammoCounter, 0);
            Canvas.SetTop(this.ammoCounter, 0);
            Interfase.Children.Add(this.ammoCounter);

            Canvas.SetLeft(this.healthText, 0);
            Canvas.SetTop(this.healthText, 25);
            Interfase.Children.Add(this.healthText);

            hitBox = new Rect(Canvas.GetLeft(this.body), Canvas.GetTop(this.body), body.Width, body.Height);
        }

        public void KeyDownRead(KeyEventArgs e)
        {
            if (e.Key == KeysBinds.MoveUp)
            {
                this.moveUp = true;
            }
            else if (e.Key == KeysBinds.MoveDown)
            {
                this.moveDown = true;
            }
            else if (e.Key == KeysBinds.MoveRight)
            {
                this.moveRight = true;
            }
            else if (e.Key == KeysBinds.MoveLeft)
            {
                this.moveLeft = true;
            }
            else if (e.Key == KeysBinds.FastReload)
            {
                this.fastReload = true;
            }
            else if (e.Key == KeysBinds.Interaction)
            {
                this.Interaction = true;
            }
        }
        public void KeyUpRead(KeyEventArgs e)
        {
            if (e.Key == KeysBinds.MoveUp)
            {
                this.moveUp = false;
            }
            else if (e.Key == KeysBinds.MoveDown)
            {
                this.moveDown = false;
            }
            else if (e.Key == KeysBinds.MoveRight)
            {
                this.moveRight = false;
            }
            else if (e.Key == KeysBinds.MoveLeft)
            {
                this.moveLeft = false;
            }
            else if (e.Key == KeysBinds.Interaction)
            {
                this.Interaction = false;
            }
        }
        public void ShowInterface(Canvas MyCanvas)
        {
            if(this.ammoCounter == null)
            {
                
            }
            this.ammoCounter.Content = "Ammo: " + gun.Ammo + " / " + gun.MaxAmmo;
            this.healthText.Content = "Health: " + Health;
        }
        public void MouseDownRead(MouseButtonEventArgs e)
        {
            if(e.ChangedButton == KeysBinds.Shot)
            {
                this.shot = true;
            }
            
        }

        public void MouseUpRead(MouseButtonEventArgs e)
        {
            if(e.ChangedButton == KeysBinds.Shot)
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
                hitBox = new Rect(Canvas.GetLeft(this.body), Canvas.GetTop(this.body), hitBox.Width, hitBox.Height);
            }
            else if (this.moveDown && Canvas.GetTop(this.body)+50 < this.BoardHeight)
            {
                Canvas.SetTop(this.body, Canvas.GetTop(this.body) + this.creatureSpeed);
                Canvas.SetTop(GameBoard, Canvas.GetTop(GameBoard) - this.creatureSpeed);
                hitBox = new Rect(Canvas.GetLeft(this.body), Canvas.GetTop(this.body), hitBox.Width, hitBox.Height);
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
                hitBox = new Rect(Canvas.GetLeft(this.body), Canvas.GetTop(this.body), hitBox.Width, hitBox.Height);
            }
            gun.PlayerPosition.X = Canvas.GetLeft(this.body);
            gun.PlayerPosition.Y = Canvas.GetTop(this.body);
            hitBox.X = Canvas.GetLeft(this.body);
            hitBox.Y = Canvas.GetTop(this.body);
        }

        public void Fire(List<PlayerOrdinaryBullet> bullets, Canvas MyCanvas)
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
