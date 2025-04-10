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
using Game.Objects;
using Game.Objects.Items.WeaponsAsItems;
using Game.Creatures.Enemies;
using Game.Objects.Walls;
using Game.Objects.Other.Shops;
using Game.Objects.Items;
using System.Numerics;
using Game.GameSystem;

namespace Game.Creatures.Players
{
    internal class Player: Creature
    {
        public class WeaponSlot
        {
            public Rectangle Slot { get; set; }
            public Gun gun { get; set; }

            public WeaponSlot(Point position, Canvas gameInterfase)
            {
                Slot = new Rectangle
                {
                    Width = 40,
                    Height = 40,
                    Fill = new SolidColorBrush(Colors.Gray),
                    Opacity = 0.80,
                    Stroke = new SolidColorBrush(Colors.White),
                    StrokeThickness = 0
                };
                Canvas.SetLeft(this.Slot, position.X);
                Canvas.SetTop(this.Slot, position.Y);
                gameInterfase.Children.Add(Slot);
            }
        }
        public Point MousePosition {  get; set; }// regarding Canvas
        private Label ammoCounter;
        private Label healthText;
        private Label moneyLabel;

        public Window window; // to close the window in PlayerDeath
        
        public bool Interaction {  get; set; }
        private bool moveUp;
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;
        private bool shot;
        private bool fastReload;

        private List<WeaponSlot> weapons;
        public int Money {  get; set; }

        private int selectedSlot;

        public Player(int boardWhidth, int boardHeight, Canvas GameBoard, Canvas Interfase, Window window)
        {
            this.window = window;

            //Player body
            {
                this.body.Fill = new SolidColorBrush(Colors.White);
                this.body.Name = "player";
                GameBoard.Children.Add(this.body);
                Canvas.SetLeft(this.body, boardWhidth / 10 * 10);
                Canvas.SetTop(this.body, boardHeight / 10 * 10);
                hitBox = new Rect(Canvas.GetLeft(this.body) + 1, Canvas.GetTop(this.body)+ 1, body.Width-2, body.Height-2);
            }

            //Player stats
            {
                this.creatureSpeed = 10;
                this.Health = 100;
                this.Money = 10000;
            }

            //Map stats
            {
                this.BoardWhidth = boardWhidth;
                this.BoardHeight = boardHeight;
            }

            //Player control
            {
                this.Interaction = false;
                this.moveUp = false;
                this.moveDown = false;
                this.moveLeft = false;
                this.moveRight = false;
                this.shot = false;
                this.fastReload = false;
            }
           
            //Player weapon
            {
                weapons = new List<WeaponSlot>
                {
                new WeaponSlot(new Point(0,0), Interfase),
                new WeaponSlot(new Point(50,0), Interfase),
                new WeaponSlot(new Point(100, 0), Interfase)
                };
                this.selectedSlot = 0;
                weapons[0].gun = new PlayerGun();
            }

            //Player Interfase
            {
                this.ammoCounter = new Label
                {
                    Name = "AmmoCounter",
                    Content = "Ammo: ",
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.White),
                    HorizontalContentAlignment = HorizontalAlignment.Left
                };
                Interfase.Children.Add(this.ammoCounter);
                Canvas.SetLeft(this.ammoCounter, 0);
                Canvas.SetTop(this.ammoCounter, this.weapons[0].Slot.Height);

                this.healthText = new Label
                {
                    Name = "HealthText",
                    Height = 30,
                    Width = 150,
                    Content = " ",
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.White),
                    HorizontalContentAlignment = HorizontalAlignment.Right
                };
                Interfase.Children.Add(this.healthText);
                Canvas.SetLeft(this.healthText, Interfase.Width - this.healthText.Width);
                Canvas.SetTop(this.healthText, 0);

                this.moneyLabel = new Label
                {
                    Name = "MoneyLabel",
                    Width = 150,
                    Content = " ",
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.White),
                    HorizontalContentAlignment = HorizontalAlignment.Right
                };
                Interfase.Children.Add(this.moneyLabel);
                Canvas.SetLeft(this.moneyLabel, Interfase.Width - this.moneyLabel.Width);
                Canvas.SetTop(this.moneyLabel, this.healthText.Height);
            }
            

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
            else if (e.Key == KeysBinds.Slot1)
            {
                this.selectedSlot = 0;
            }
            else if (e.Key == KeysBinds.Slot2)
            {
                this.selectedSlot = 1;
            }
            else if (e.Key == KeysBinds.Slot3)
            {
                this.selectedSlot = 2;
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
        public void MouseDownRead(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == KeysBinds.Shot)
            {
                this.shot = true;
            }

        }
        public void MouseUpRead(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == KeysBinds.Shot)
            {
                this.shot = false;
            }
        }

        public void ShowInterface(Canvas MyCanvas, Window window)
        {
            //if (Health <= 0)
            //{
            //    window.Close();
            //}
            this.healthText.Content = "Health: " + Health;
            this.moneyLabel.Content = "Money: " + this.Money;
            //Slots
            {
                this.weapons[this.selectedSlot].Slot.StrokeThickness = 5;
                for (int i = 0; i< this.weapons.Count; i++)
                {
                    if(i != this.selectedSlot)
                    {
                        this.weapons[i].Slot.StrokeThickness = 0;
                    }
                }
            }

            //display of gun
            {
                if (this.weapons[this.selectedSlot].gun != null)
                {
                    this.ammoCounter.Content = "Ammo: " + this.weapons[this.selectedSlot].gun.Ammo + " / " + this.weapons[this.selectedSlot].gun.MaxAmmo;
                }
                else
                {
                    this.ammoCounter.Content = null;
                }
            }
        }
        
        public bool CheckForCollision(List<Enemy> enemies, List<GameObject> gameObjects, string direction)
        {
            switch (direction)
            {
                case "Up":
                    this.hitBox.Y -= this.creatureSpeed;
                    break;
                case "Down":
                    this.hitBox.Y += this.creatureSpeed;
                    break;
                case "Left":
                    this.hitBox.X -= this.creatureSpeed;
                    break;
                case "Right":
                    this.hitBox.X += this.creatureSpeed;
                    break;
                default: 
                    break;
            }
            foreach (var item in gameObjects)
            {
                if (!(item is Item) && this.hitBox.IntersectsWith(item.hitBox))
                {
                    return false;
                }
            }
            foreach (var item in enemies)
            {
                if (this.hitBox.IntersectsWith(item.hitBox))
                {
                    return false;
                }
            }
            return true;
        }

        public void PlayerMove(Canvas GameBoard, List<Enemy> enemies, List<GameObject> gameObjects)
        {
            if (this.moveUp && Canvas.GetTop(this.body) > 0 && CheckForCollision(enemies, gameObjects, "Up"))
            {
                Canvas.SetTop(this.body, Canvas.GetTop(this.body) - this.creatureSpeed);
                Canvas.SetTop(GameBoard, Canvas.GetTop(GameBoard) + this.creatureSpeed);
            }
            else if (this.moveDown && Canvas.GetTop(this.body)+50 < this.BoardHeight && CheckForCollision(enemies, gameObjects, "Down"))
            {
                Canvas.SetTop(this.body, Canvas.GetTop(this.body) + this.creatureSpeed);
                Canvas.SetTop(GameBoard, Canvas.GetTop(GameBoard) - this.creatureSpeed);
            }
            hitBox.X = Canvas.GetLeft(this.body) + 1;
            hitBox.Y = Canvas.GetTop(this.body) + 1;
            if (this.moveRight && Canvas.GetLeft(this.body) + 50 < this.BoardWhidth && CheckForCollision(enemies, gameObjects, "Right"))
            {
                Canvas.SetLeft(this.body, Canvas.GetLeft(this.body) + this.creatureSpeed);
                Canvas.SetLeft(GameBoard, Canvas.GetLeft(GameBoard) - this.creatureSpeed);
            }
            else if (this.moveLeft && Canvas.GetLeft(this.body) > 0 && CheckForCollision(enemies, gameObjects, "Left"))
            {
                Canvas.SetLeft(this.body, Canvas.GetLeft(this.body) - this.creatureSpeed);
                Canvas.SetLeft(GameBoard, Canvas.GetLeft(GameBoard) + this.creatureSpeed);
            }
            hitBox.X = Canvas.GetLeft(this.body) + 1;
            hitBox.Y = Canvas.GetTop(this.body) + 1;
            if (this.weapons[this.selectedSlot].gun != null)
            {
                this.weapons[this.selectedSlot].gun.PlayerPosition.X = Canvas.GetLeft(this.body);
                this.weapons[this.selectedSlot].gun.PlayerPosition.Y = Canvas.GetTop(this.body);
            }
        }

        public void SetSelectedSlot(Gun weapon, Canvas gameBoard, List<GameObject> gameObjects)
        {
            if (this.weapons[this.selectedSlot].gun == null)
            {
                this.weapons[this.selectedSlot].gun = weapon;
                this.weapons[this.selectedSlot].gun.PlayerPosition = GetPosition();
            }
            else
            {
                new WeaponAsItem(GetPosition(), gameBoard, gameObjects, this.weapons[this.selectedSlot].gun);
                this.weapons[this.selectedSlot].gun = weapon;
                this.weapons[this.selectedSlot].gun.PlayerPosition = GetPosition();
            }
        }

        public void Fire(List<PlayerOrdinaryBullet> bullets, Canvas MyCanvas)
        {
            if (this.fastReload &&  this.weapons[this.selectedSlot].gun != null)
            {
                this.weapons[this.selectedSlot].gun.FastReload();
                this.fastReload = false;
            }
            foreach (var item in weapons)
            {
                if(item.gun != null)
                {
                    item.gun.GunReload();
                }
            }
            if (shot &&  this.weapons[this.selectedSlot].gun != null)
            {
                this.weapons[this.selectedSlot].gun.Shot(this.MousePosition, bullets, MyCanvas);
            }

        }

    }
    
}
