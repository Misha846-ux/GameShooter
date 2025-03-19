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

using Game;
using Game.Creatures;
using Game.Creatures.Players;
using Game.Creatures.Enemies;
using Game.Bullets;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        Player player;
        Enemy enemy;
        List<Bullet> bullets;
        List<Enemy> enemies;
        private void GameLoop(object sender, EventArgs e)
        {
            player.BoardWhidth = (int)GameBoard.Width;
            player.BoardHeight = (int)GameBoard.Height;
            player.ShowInterface(Interface);
            player.PlayerMove(GameBoard);
            player.Fire(bullets, GameBoard);
            foreach (var item in bullets)
            {
                item.BoardWhidth = (int)GameBoard.Width;
                item.BoardHeight = (int)GameBoard.Height;
                item.BulletMove(GameBoard);
            }
            foreach (var item in enemies)
            {
                item.Shot(player.GetBody(), bullets, GameBoard);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += GameLoop;
            timer.Start();

            GameBoard.Focus();
            GameBoard.Width = ((int)(System.Windows.SystemParameters.PrimaryScreenWidth * 2) / 10) * 10;
            GameBoard.Height = ((int)(System.Windows.SystemParameters.PrimaryScreenHeight * 2) / 10) * 10;
            Canvas.SetLeft(GameBoard, -System.Windows.SystemParameters.PrimaryScreenWidth / 2);
            Canvas.SetTop(GameBoard, -System.Windows.SystemParameters.PrimaryScreenHeight / 2);
            Interface.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            Interface.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            bullets = new List<Bullet>();
            enemies = new List<Enemy>();
            enemy = new Enemy(new Point(0,0), GameBoard, enemies);
            player = new Player((int)System.Windows.SystemParameters.PrimaryScreenWidth, (int)System.Windows.SystemParameters.PrimaryScreenHeight, GameBoard, Interface);
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