using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;
using Game.Creatures;
using Game.Creatures.Players;
using Game.Creatures.Enemies;
using Game.Bullets;
using Game.Objects;
using Game.Objects.Walls.UnbreakableWalls;
using Game.Objects.Walls.BreakableWalls;
using Game.Objects.Items;
using Game.Objects.Items.FiniteItems;
using Game.Objects.Items.WeaponsAsItems;
using Game.Objects.Other;
using Game.GameSystem;
using Game.Bullets.EnemyBullets;
using Game.Bullets.PlayerBullets;
using System;
using Game.Objects.Weapons.PlayerWeapons;
using Game.Objects.Other.Shops.StaticShop;
using Game.Objects.Other.Shops;
using Game.Objects.Other.Shops.WeaponShop;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private MenuWindow menu;

        private Player player;
        private List<EnemyOrdinaryBullet> enemyBullets;
        private List<PlayerOrdinaryBullet> playerBullets;
        private List<Enemy> enemies;
        private List<GameObject> gameObjects;
        private MemoryCleaner memoryCleaner;

        private async void GameLoop(object sender, EventArgs e)
        {
            player.BoardWhidth = (int)GameBoard.Width;
            player.BoardHeight = (int)GameBoard.Height;
            player.ShowInterface(Interface);
            player.PlayerMove(GameBoard, enemies, gameObjects);
            player.Fire(playerBullets, GameBoard);
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                if (gameObjects[i] is Item)
                {
                    ((Item)gameObjects[i]).InteractionWithObject(player, GameBoard, Interface, memoryCleaner);
                }
                else if (gameObjects[i] is Shop)
                {
                    ((Shop)gameObjects[i]).InteractionWithObject(player, GameBoard, Interface);
                }
                else if (gameObjects[i] is WoodenWall)
                {
                    gameObjects[i].CheckDeath(memoryCleaner, GameBoard);
                }
                else if (gameObjects[i] is EnemySummoningPoint)
                {
                    ((EnemySummoningPoint)gameObjects[i]).SummonEnemy(GameBoard, enemies);
                }
                else if (gameObjects[i] is ResourceDrill)
                {
                    ((ResourceDrill)gameObjects[i]).DrillWorking(player);
                }

            }
            foreach (var item in enemies)
            {
                item.Shot(player.GetBody(), enemyBullets, GameBoard);
                item.move(player.GetPosition(), GameBoard, player, enemies, gameObjects);
            }
            foreach (var item in playerBullets)
            {
                 item.BulletMove(memoryCleaner, GameBoard);
                 item.CheckCollisionWihtEnemy(enemies, memoryCleaner, GameBoard);


            }
            foreach (var item in enemyBullets)
            {
                item.BulletMove(memoryCleaner, GameBoard);
                item.CheckCollisionWihtPlayer(player, memoryCleaner, GameBoard);
            }
            memoryCleaner.Clean(playerBullets, enemyBullets, gameObjects, enemies);
        }
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += GameLoop;
            timer.Start();

            GameBoard.Focus();
            GameBoard.Width = 3070;
            GameBoard.Height = 1720;
            Canvas.SetLeft(GameBoard, -System.Windows.SystemParameters.PrimaryScreenWidth / 2);
            Canvas.SetTop(GameBoard, -System.Windows.SystemParameters.PrimaryScreenHeight / 2);

            //Adding BackGround for GameBoard
            {
                ImageBrush backGroundImage = new ImageBrush();
                backGroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Textures/GrassBackGround.png"));
                backGroundImage.TileMode = TileMode.Tile;
                backGroundImage.Viewport = new Rect(0, 0, 0.15, 0.15);
                backGroundImage.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
                GameBoard.Background = backGroundImage;
            }

            Interface.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            Interface.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            playerBullets = new List<PlayerOrdinaryBullet>();
            enemyBullets = new List<EnemyOrdinaryBullet>();
            enemies = new List<Enemy>();
            gameObjects = new List<GameObject>();
            memoryCleaner = new MemoryCleaner();
            menu = new MenuWindow(this, Interface, GameBoard, timer);

            player = new Player((int)System.Windows.SystemParameters.PrimaryScreenWidth, (int)System.Windows.SystemParameters.PrimaryScreenHeight, GameBoard, Interface);

            GameObject testobject = new StoneWall(new Point(200, 100), GameBoard, gameObjects);
            new EnemySummoningPoint(new Point(0, 0), GameBoard, gameObjects);
            //new EnemySummoningPoint(new Point(500, 0), GameBoard, gameObjects);
            //new EnemySummoningPoint(new Point(1000, 0), GameBoard, gameObjects);
            //new EnemySummoningPoint(new Point(1500, 0), GameBoard, gameObjects);
            //new EnemySummoningPoint(new Point(2000, 0), GameBoard, gameObjects);
            WoodenWall woodenWall = new WoodenWall(new Point(200,150), GameBoard, gameObjects);
            new HealingPotionShop(new Point(400, 150), GameBoard, gameObjects);
            new WeaponShop(new Point(400, 250), GameBoard, gameObjects);
            new ResourceDrill(
                new Point(player.GetPosition().X - (120 - player.GetBody().Width), player.GetPosition().Y + player.GetBody().Height - 400),
                GameBoard, gameObjects
            );
        }

        private void OnKeyDown1(object sender, KeyEventArgs e)
        {
            player.KeyDownRead(e);
            if(e.Key == KeysBinds.OpenMenu)
            {
                this.menu.OpenWindow();
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            player.KeyUpRead(e);
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            player.MousePosition = e.GetPosition(GameBoard);
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            player.MouseDownRead(e);
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            player.MouseUpRead(e);
        }
    }
}