using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;

namespace Game.GameSystem
{
    class PlayerDeathWindow
    {
        private Window window;
        private Canvas menu;
        private Canvas gameInterface;
        private Canvas gameBoard;
        private DispatcherTimer timer;

        private SettingsMenu settings;



        public PlayerDeathWindow(Window window, Canvas gameInterface, Canvas gameBoard, DispatcherTimer timer)
        {
            this.window = window;
            this.gameInterface = gameInterface;
            this.gameBoard = gameBoard;
            this.timer = timer;

            this.menu = new Canvas
            {
                Width = System.Windows.SystemParameters.PrimaryScreenWidth,
                Height = System.Windows.SystemParameters.PrimaryScreenHeight,
                Opacity = 0.60,
                Background = new SolidColorBrush(Colors.Black)
            };
            settings = new SettingsMenu(gameInterface);

            {
                Button exit = new Button
                {
                    Content = "You lost, exit (",
                    FontSize = 20,
                    Foreground = new SolidColorBrush(Colors.White),
                    Width = 200,
                    Height = 50,
                    Background = new SolidColorBrush(Colors.Black)


                };
                exit.Background.Opacity = 0;
                Canvas.SetLeft(exit, this.gameInterface.Width / 2 - exit.Width / 2);
                Canvas.SetTop(exit, this.gameInterface.Height / 2 - exit.Height / 2 + 10);
                this.menu.Children.Add(exit);
                exit.Click += ExitClick;
            }

        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.window.Close();
        }

        public void OpenWindow()
        {
            bool isFinished = true;

            foreach (var item in gameInterface.Children)
            {
                if (item == this.menu)
                {
                    isFinished = false;
                    break;
                }
            }
            if (isFinished)
            {
                this.gameInterface.Children.Add(this.menu);
                this.timer.Stop();
                Keyboard.ClearFocus();
            }
        }
    }
}
