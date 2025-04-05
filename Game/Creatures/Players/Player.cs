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
        private Label interactionLabel;
        
        public bool Interaction {  get; set; }
        private bool moveUp;
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;
        private bool shot;
        private bool fastReload;

        List<WeaponSlot> weapons;
        private int selectedSlot;

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
            hitBox = new Rect(Canvas.GetLeft(this.body), Canvas.GetTop(this.body), body.Width, body.Height);

            
            weapons = new List<WeaponSlot>
            {
                new WeaponSlot(new Point(0,0), Interfase),
                new WeaponSlot(new Point(50,0), Interfase),
                new WeaponSlot(new Point(100, 0), Interfase)
            };

            this.selectedSlot = 0;

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
                Width = 120,
                Content = " ",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White)
            };
            



            Interfase.Children.Add(this.ammoCounter);
            Canvas.SetLeft(this.ammoCounter, 0);
            Canvas.SetTop(this.ammoCounter, this.weapons[0].Slot.Height);

            Interfase.Children.Add(this.healthText);
            Canvas.SetLeft(this.healthText, Interfase.Width - this.healthText.Width);
            Canvas.SetTop(this.healthText, 0);

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
        public void ShowInterface(Canvas MyCanvas)
        {
            //display of gun
            {
                if(this.weapons[this.selectedSlot].gun != null)
                {
                    this.ammoCounter.Content = "Ammo: " + this.weapons[this.selectedSlot].gun.Ammo + " / " + this.weapons[this.selectedSlot].gun.MaxAmmo;
                }
                else
                {
                    this.ammoCounter.Content = null;
                }
            }
            this.healthText.Content = "Health: " + Health;
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
            if(this.weapons[this.selectedSlot].gun != null)
            {
                this.weapons[this.selectedSlot].gun.PlayerPosition.X = Canvas.GetLeft(this.body);
                this.weapons[this.selectedSlot].gun.PlayerPosition.Y = Canvas.GetTop(this.body);
            }
            hitBox.X = Canvas.GetLeft(this.body);
            hitBox.Y = Canvas.GetTop(this.body);
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
