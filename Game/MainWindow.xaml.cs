﻿using System.Text;
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

using Game;
using Game.Creatures;
using Game.Creatures.Players;
using Game.Creatures.Enemies;
using Game.Bullets;
using Game.Objects;
using Game.Objects.Walls.UnbreakableWalls;
using Game.Objects.Walls.BreakableWalls;
using Game.Objects.Other;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        Player player;
        List<Bullet> bullets;
        List<Enemy> enemies;
        List<GameObject> gameObjects;
        MemoryCleaner memoryCleaner;
        private void GameLoop(object sender, EventArgs e)
        {
            player.BoardWhidth = (int)GameBoard.Width;
            player.BoardHeight = (int)GameBoard.Height;
            player.ShowInterface(Interface);
            player.PlayerMove(GameBoard);
            player.Fire(bullets, GameBoard);
            foreach (var item in gameObjects)
            {
                if(item is WoodenWall)
                {
                    item.CheckDeath(memoryCleaner, GameBoard);
                }
                else if (item is EnemySummoningPoint)
                {
                    ((EnemySummoningPoint)item).SummonEnemy(GameBoard, enemies);
                }
            }
            foreach (var item in bullets)
            {
                item.BulletMove(memoryCleaner, GameBoard);
            }
            foreach (var item in enemies)
            {
                item.Shot(player.GetBody(), bullets, GameBoard);
                item.move(player.GetPosition(), GameBoard);
            }
            memoryCleaner.Clean(bullets, gameObjects, enemies);
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
            bullets = new List<Bullet>();
            enemies = new List<Enemy>();
            gameObjects = new List<GameObject>();
            memoryCleaner = new MemoryCleaner();

            player = new Player((int)System.Windows.SystemParameters.PrimaryScreenWidth, (int)System.Windows.SystemParameters.PrimaryScreenHeight, GameBoard, Interface);

            GameObject testobject = new StoneWall(new Point(200, 100), GameBoard, gameObjects);
            new EnemySummoningPoint(new Point(0,0), GameBoard, gameObjects);
            new EnemySummoningPoint(new Point(500, 0), GameBoard, gameObjects);
            new EnemySummoningPoint(new Point(1000, 0), GameBoard, gameObjects);
            new EnemySummoningPoint(new Point(1500, 0), GameBoard, gameObjects);
            new EnemySummoningPoint(new Point(2000, 0), GameBoard, gameObjects);
            WoodenWall woodenWall = new WoodenWall(new Point(200,150), GameBoard, gameObjects);

        }
        
        private void OnKeyDown1(object sender, KeyEventArgs e)
        {
            player.KeyDownRead(e);
            if(e.Key == Key.Escape)
            {
                this.Close();
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