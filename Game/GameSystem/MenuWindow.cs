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
using Game.Creatures;
using Game.Creatures.Players;
using Game.Creatures.Enemies;
using Game.Bullets;
using Game.Objects;
using Game.Objects.Walls.UnbreakableWalls;
using Game.Objects.Walls.BreakableWalls;
using Game.Objects.Other;
using System.Windows.Threading;

namespace Game.GameSystem
{
    internal class MenuWindow
    {
        private Window window;
        private Canvas menu;
        private Canvas gameInterface;
        private Canvas gameBoard;
        private DispatcherTimer timer;

        private SettingsMenu settings;



        public MenuWindow(Window window, Canvas gameInterface, Canvas gameBoard, DispatcherTimer timer)
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

            int indentation = 60;
            {
                Button continuePlay = new Button
                {
                    Content = "Continue",
                    FontSize = 20,
                    Foreground = new SolidColorBrush(Colors.White),
                    Width = 200,
                    Height = 50,
                    Background = new SolidColorBrush(Colors.Black)


                };
                continuePlay.Background.Opacity = 0;
                Canvas.SetLeft(continuePlay, gameInterface.Width / 2 - continuePlay.Width / 2);
                Canvas.SetTop(continuePlay, gameInterface.Height / 2 - continuePlay.Height / 2 - indentation);
                this.menu.Children.Add(continuePlay);
                continuePlay.Click += ContinuePlayClick;

            }

            {
                Button exit = new Button
                {
                    Content = "Log out",
                    FontSize = 20,
                    Foreground = new SolidColorBrush(Colors.White),
                    Width = 200,
                    Height = 50,
                    Background = new SolidColorBrush(Colors.Black)


                };
                exit.Background.Opacity = 0;
                Canvas.SetLeft(exit, this.gameInterface.Width / 2 - exit.Width / 2);
                Canvas.SetTop(exit, this.gameInterface.Height / 2 - exit.Height / 2 + indentation);
                this.menu.Children.Add(exit);
                exit.Click += ExitClick;
            }

            {
                Button settings = new Button
                {
                    Content = "Settings",
                    FontSize = 20,
                    Foreground = new SolidColorBrush(Colors.White),
                    Width = 200,
                    Height = 50,
                    Background = new SolidColorBrush(Colors.Black)


                };
                settings.Background.Opacity = 0;
                Canvas.SetLeft(settings, this.gameInterface.Width / 2 - settings.Width / 2);
                Canvas.SetTop(settings, this.gameInterface.Height / 2 - settings.Height / 2);
                this.menu.Children.Add(settings);
                settings.Click += OpenSettings;
            }
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            this.settings.Open();
        }
        private void ContinuePlayClick(object sender, RoutedEventArgs e)
        {
            this.gameInterface.Children.Remove(this.menu);
            this.timer.Start();
            this.gameBoard.Focus();
            
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
