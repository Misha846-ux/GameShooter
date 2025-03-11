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
        List<Bullet> bullets;
        private void GameLoop(object sender, EventArgs e)
        {
            player.BoardWhidth = (int)Application.Current.MainWindow.Width;
            player.BoardHeight = (int)Application.Current.MainWindow.Height;
            player.ShowInterface(MyCanvas);
            player.PlayerMove();
            player.Fire(bullets, MyCanvas);
            foreach (var item in bullets)
            {
                item.BoardWhidth = (int)Application.Current.MainWindow.Width;
                item.BoardHeight = (int)Application.Current.MainWindow.Height;
                item.BulletMove(MyCanvas);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += GameLoop;
            timer.Start();

            MyCanvas.Focus();
            player = new Player((int)Application.Current.MainWindow.Width, (int)Application.Current.MainWindow.Height);
            bullets = new List<Bullet>();
            MyCanvas.Children.Add(player.GetBody());
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
            player.MousePosition = e.GetPosition(MyCanvas);
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