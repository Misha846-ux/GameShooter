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
using System.Threading.Channels;
using Microsoft.VisualBasic;

namespace Game.GameSystem
{
    internal class SettingsMenu
    {
        private Canvas settingsMenu; // 
        private Canvas window; //the previous page on which the settings menu is based


        public SettingsMenu(Canvas window)
        {
            this.window = window;
            {
                this.settingsMenu = new Canvas
                {
                    Name = "SettingsMenu",
                    Width = window.Width / 2,
                    Height = window.Height - window.Height/4,
                    Background = new SolidColorBrush(Colors.Black)

                };
                Canvas.SetLeft(settingsMenu, settingsMenu.Width/2);
                Canvas.SetTop(settingsMenu, settingsMenu.Height / 6);
            }

            double buttonWidth = this.settingsMenu.Width / 10;
            double buttonHeight = this.settingsMenu.Height / 15;
            Rectangle bottomLine = new Rectangle // the line that is at the bottom and is always visible
            {
                Name = "BottomLine",
                Width = this.settingsMenu.Width,
                Height = buttonHeight,
                Fill = new SolidColorBrush(Colors.Black),
                Stroke = new SolidColorBrush(Colors.White),
            };
            settingsMenu.Children.Add(bottomLine);
            Canvas.SetLeft(bottomLine, 0);
            Canvas.SetTop(bottomLine, settingsMenu.Height);

            {
                Button back = new Button
                {
                    Content = "back",
                    FontSize = 20,
                    Foreground = new SolidColorBrush(Colors.White),
                    Width = buttonWidth,
                    Height = bottomLine.Height,
                    Background = new SolidColorBrush(Colors.Black),
                    BorderBrush = new SolidColorBrush(Colors.White),
                };
                back.Background.Opacity = 0;
                Canvas.SetLeft(back, 0);
                Canvas.SetTop(back, settingsMenu.Height);
                this.settingsMenu.Children.Add(back);
                back.Click += BackClick;
            }
            {
                Button saveСhanges = new Button
                {
                    Content = "Save",
                    FontSize = 20,
                    Foreground = new SolidColorBrush(Colors.White),
                    Width = buttonWidth,
                    Height = bottomLine.Height,
                    Background = new SolidColorBrush(Colors.Black),
                    BorderBrush = new SolidColorBrush(Colors.White),
                };
                saveСhanges.Background.Opacity = 0;
                Canvas.SetLeft(saveСhanges, settingsMenu.Width - buttonWidth);
                Canvas.SetTop(saveСhanges, settingsMenu.Height);
                this.settingsMenu.Children.Add(saveСhanges);
                saveСhanges.Click += SaveСhangesClick;
            }

            {
                Button resetToBase = new Button
                {
                    Content = "Reset",
                    FontSize = 20,
                    Foreground = new SolidColorBrush(Colors.White),
                    Width = buttonWidth,
                    Height = bottomLine.Height,
                    Background = new SolidColorBrush(Colors.Black),
                    BorderBrush = new SolidColorBrush(Colors.White),
                };
                resetToBase.Background.Opacity = 0;
                Canvas.SetLeft(resetToBase, settingsMenu.Width - buttonWidth * 2);
                Canvas.SetTop(resetToBase, settingsMenu.Height);
                this.settingsMenu.Children.Add(resetToBase);
                resetToBase.Click += ResetToBaseClick;
            }

            {//Section of buttons
                {// for movement
                    Label movementSelection = new Label
                    {
                        Content = "Movement Selection",
                        FontSize = 20,
                        Background = new SolidColorBrush(Colors.Black),
                        Foreground= new SolidColorBrush(Colors.White),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Width = this.settingsMenu.Width,
                        Height = buttonHeight,
                        BorderBrush = new SolidColorBrush(Colors.White),
                        BorderThickness = new Thickness(bottomLine.StrokeThickness)
                    };
                    this.settingsMenu.Children.Add (movementSelection);
                }
                //MoveUp Label
                {
                    Label moveUp = new Label
                    {
                        Name = "MoveUp",
                        FontSize = 20,
                        Background = new SolidColorBrush(Colors.Black),
                        Foreground = new SolidColorBrush(Colors.White),
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        Width = this.settingsMenu.Width,
                        Height = buttonHeight,
                        BorderBrush = new SolidColorBrush(Colors.White),
                        BorderThickness = new Thickness(bottomLine.StrokeThickness)
                    };
                    this.settingsMenu.Children.Add(moveUp);
                    Canvas.SetTop(moveUp, buttonHeight * 1);
                    moveUp.Content = "MoveUp:     " + '\'' + KeysBinds.MoveUp + '\'';
                    //button to changemoveUp
                    {
                        Button changemoveUp = new Button
                        {
                            Content = "Change",
                            FontSize = 20,
                            Foreground = new SolidColorBrush(Colors.White),
                            Width = buttonWidth,
                            Height = bottomLine.Height,
                            Background = new SolidColorBrush(Colors.Black),
                            BorderBrush = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Right
                        };
                        changemoveUp.Background.Opacity = 0;
                        Canvas.SetLeft(changemoveUp, settingsMenu.Width - buttonWidth);
                        Canvas.SetTop(changemoveUp, buttonHeight * 1);
                        this.settingsMenu.Children.Add(changemoveUp);
                        changemoveUp.Click += ChangeMoveUpClick;
                    }

                }
                //MoveDown Label
                {
                    Label moveDown = new Label
                    {
                        Name = "MoveDown",
                        FontSize = 20,
                        Background = new SolidColorBrush(Colors.Black),
                        Foreground = new SolidColorBrush(Colors.White),
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        Width = this.settingsMenu.Width,
                        Height = buttonHeight,
                        BorderBrush = new SolidColorBrush(Colors.White),
                        BorderThickness = new Thickness(bottomLine.StrokeThickness)
                    };
                    this.settingsMenu.Children.Add(moveDown);
                    Canvas.SetTop(moveDown, buttonHeight * 2);
                    moveDown.Content = "MoveDown:     " + '\'' + KeysBinds.MoveDown + '\'';
                    //button to changemoveDown
                    {
                        Button changemoveDown = new Button
                        {
                            Content = "Change",
                            FontSize = 20,
                            Foreground = new SolidColorBrush(Colors.White),
                            Width = buttonWidth,
                            Height = bottomLine.Height,
                            Background = new SolidColorBrush(Colors.Black),
                            BorderBrush = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Right
                        };
                        changemoveDown.Background.Opacity = 0;
                        Canvas.SetLeft(changemoveDown, settingsMenu.Width - buttonWidth);
                        Canvas.SetTop(changemoveDown, buttonHeight * 2);
                        this.settingsMenu.Children.Add(changemoveDown);
                        changemoveDown.Click += ChangeMoveDownClick;
                    }

                }
                //MoveLeft Label
                {
                    Label moveLeft = new Label
                    {
                        Name = "MoveLeft",
                        FontSize = 20,
                        Background = new SolidColorBrush(Colors.Black),
                        Foreground = new SolidColorBrush(Colors.White),
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        Width = this.settingsMenu.Width,
                        Height = buttonHeight,
                        BorderBrush = new SolidColorBrush(Colors.White),
                        BorderThickness = new Thickness(bottomLine.StrokeThickness)
                    };
                    this.settingsMenu.Children.Add(moveLeft);
                    Canvas.SetTop(moveLeft, buttonHeight * 3);
                    moveLeft.Content = "MoveLeft:     " + '\'' + KeysBinds.MoveLeft + '\'';
                    //button to changemoveLeft
                    {
                        Button changemoveLeft = new Button
                        {
                            Content = "Change",
                            FontSize = 20,
                            Foreground = new SolidColorBrush(Colors.White),
                            Width = buttonWidth,
                            Height = bottomLine.Height,
                            Background = new SolidColorBrush(Colors.Black),
                            BorderBrush = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Right
                        };
                        changemoveLeft.Background.Opacity = 0;
                        Canvas.SetLeft(changemoveLeft, settingsMenu.Width - buttonWidth);
                        Canvas.SetTop(changemoveLeft, buttonHeight * 3);
                        this.settingsMenu.Children.Add(changemoveLeft);
                        changemoveLeft.Click += ChangeMoveLeftClick;
                    }

                }
                //MoveRight Label
                {
                    Label moveRight = new Label
                    {
                        Name = "MoveRight",
                        FontSize = 20,
                        Background = new SolidColorBrush(Colors.Black),
                        Foreground = new SolidColorBrush(Colors.White),
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        Width = this.settingsMenu.Width,
                        Height = buttonHeight,
                        BorderBrush = new SolidColorBrush(Colors.White),
                        BorderThickness = new Thickness(bottomLine.StrokeThickness)
                    };
                    this.settingsMenu.Children.Add(moveRight);
                    Canvas.SetTop(moveRight, buttonHeight * 4);
                    moveRight.Content = "MoveRight:     " + '\'' + KeysBinds.MoveRight + '\'';
                    //button to changemoveRight
                    {
                        Button changemoveRight = new Button
                        {
                            Content = "Change",
                            FontSize = 20,
                            Foreground = new SolidColorBrush(Colors.White),
                            Width = buttonWidth,
                            Height = bottomLine.Height,
                            Background = new SolidColorBrush(Colors.Black),
                            BorderBrush = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Right
                        };
                        changemoveRight.Background.Opacity = 0;
                        Canvas.SetLeft(changemoveRight, settingsMenu.Width - buttonWidth);
                        Canvas.SetTop(changemoveRight, buttonHeight * 4);
                        this.settingsMenu.Children.Add(changemoveRight);
                        changemoveRight.Click += ChangeMoveRightClick;
                    }

                }
                //FastReload Label
                {
                    Label fastReload = new Label
                    {
                        Name = "FastReload",
                        FontSize = 20,
                        Background = new SolidColorBrush(Colors.Black),
                        Foreground = new SolidColorBrush(Colors.White),
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        Width = this.settingsMenu.Width,
                        Height = buttonHeight,
                        BorderBrush = new SolidColorBrush(Colors.White),
                        BorderThickness = new Thickness(bottomLine.StrokeThickness)
                    };
                    this.settingsMenu.Children.Add(fastReload);
                    Canvas.SetTop(fastReload, buttonHeight * 5);
                    fastReload.Content = "FastReload:     " + '\'' + KeysBinds.FastReload + '\'';
                    //button to changefastReload
                    {
                        Button changefastReload = new Button
                        {
                            Content = "Change",
                            FontSize = 20,
                            Foreground = new SolidColorBrush(Colors.White),
                            Width = buttonWidth,
                            Height = bottomLine.Height,
                            Background = new SolidColorBrush(Colors.Black),
                            BorderBrush = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Right
                        };
                        changefastReload.Background.Opacity = 0;
                        Canvas.SetLeft(changefastReload, settingsMenu.Width - buttonWidth);
                        Canvas.SetTop(changefastReload, buttonHeight * 5);
                        this.settingsMenu.Children.Add(changefastReload);
                        changefastReload.Click += ChangeFastReloadClick;
                    }

                }
                // Interaction Label
                {
                    Label interaction = new Label
                    {
                        Name = "Interaction",
                        FontSize = 20,
                        Background = new SolidColorBrush(Colors.Black),
                        Foreground = new SolidColorBrush(Colors.White),
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        Width = this.settingsMenu.Width,
                        Height = buttonHeight,
                        BorderBrush = new SolidColorBrush(Colors.White),
                        BorderThickness = new Thickness(bottomLine.StrokeThickness)
                    };
                    this.settingsMenu.Children.Add(interaction);
                    Canvas.SetTop(interaction, buttonHeight * 6);
                    interaction.Content = "Interaction:     " + '\'' + KeysBinds.Interaction + '\'';
                    //button to changeInteraction
                    {
                        Button changeInteraction = new Button
                        {
                            Content = "Change",
                            FontSize = 20,
                            Foreground = new SolidColorBrush(Colors.White),
                            Width = buttonWidth,
                            Height = bottomLine.Height,
                            Background = new SolidColorBrush(Colors.Black),
                            BorderBrush = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Right
                        };
                        changeInteraction.Background.Opacity = 0;
                        Canvas.SetLeft(changeInteraction, settingsMenu.Width - buttonWidth);
                        Canvas.SetTop(changeInteraction, buttonHeight * 6);
                        this.settingsMenu.Children.Add(changeInteraction);
                        changeInteraction.Click += ChangeInteractionClick;
                    }

                }
                // OpenMenu Label
                {
                    Label openMenu = new Label
                    {
                        Name = "OpenMenu",
                        FontSize = 20,
                        Background = new SolidColorBrush(Colors.Black),
                        Foreground = new SolidColorBrush(Colors.White),
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        Width = this.settingsMenu.Width,
                        Height = buttonHeight,
                        BorderBrush = new SolidColorBrush(Colors.White),
                        BorderThickness = new Thickness(bottomLine.StrokeThickness)
                    };
                    this.settingsMenu.Children.Add(openMenu);
                    Canvas.SetTop(openMenu, buttonHeight * 7);
                    openMenu.Content = "OpenMenu:     " + '\'' + KeysBinds.OpenMenu + '\'';
                    //button to changeOpenMenu
                    {
                        Button changeopenMenu = new Button
                        {
                            Content = "Change",
                            FontSize = 20,
                            Foreground = new SolidColorBrush(Colors.White),
                            Width = buttonWidth,
                            Height = bottomLine.Height,
                            Background = new SolidColorBrush(Colors.Black),
                            BorderBrush = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Right
                        };
                        changeopenMenu.Background.Opacity = 0;
                        Canvas.SetLeft(changeopenMenu, settingsMenu.Width - buttonWidth);
                        Canvas.SetTop(changeopenMenu, buttonHeight * 7);
                        this.settingsMenu.Children.Add(changeopenMenu);
                        changeopenMenu.Click += ChangeOpenMenuClick;
                    }

                }
                // Shot Label
                {
                    Label shot = new Label
                    {
                        Name = "Shot",
                        FontSize = 20,
                        Background = new SolidColorBrush(Colors.Black),
                        Foreground = new SolidColorBrush(Colors.White),
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        Width = this.settingsMenu.Width,
                        Height = buttonHeight,
                        BorderBrush = new SolidColorBrush(Colors.White),
                        BorderThickness = new Thickness(bottomLine.StrokeThickness)
                    };
                    this.settingsMenu.Children.Add(shot);
                    Canvas.SetTop(shot, buttonHeight * 7);
                    shot.Content = "Shot:     " + '\'' + KeysBinds.Shot + '\'';
                    //button to changeshot
                    {
                        Button changeshot = new Button
                        {
                            Content = "Change",
                            FontSize = 20,
                            Foreground = new SolidColorBrush(Colors.White),
                            Width = buttonWidth,
                            Height = bottomLine.Height,
                            Background = new SolidColorBrush(Colors.Black),
                            BorderBrush = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Right
                        };
                        changeshot.Background.Opacity = 0;
                        Canvas.SetLeft(changeshot, settingsMenu.Width - buttonWidth);
                        Canvas.SetTop(changeshot, buttonHeight * 7);
                        this.settingsMenu.Children.Add(changeshot);
                        changeshot.Click += ChangeShotClick;
                    }

                }
            }
        }
        private void ChangeShotClick(object sender, RoutedEventArgs e)
        {
            this.settingsMenu.MouseDown += ShotBind;
            this.settingsMenu.Focus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button && item != sender)
                {
                    ((Button)item).IsEnabled = false;
                }
            }
        }
        private void ShotBind(object sender, MouseButtonEventArgs e)
        {
            KeysBinds.Shot = e.ChangedButton;
            this.settingsMenu.MouseDown -= ShotBind;
            Keyboard.ClearFocus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button)
                {
                    ((Button)item).IsEnabled = true;
                }
            }
            UpdateLabels();
        }
        private void ChangeOpenMenuClick(object sender, RoutedEventArgs e)
        {
            this.settingsMenu.KeyDown += OpenMenuBind;
            this.settingsMenu.Focus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button && item != sender)
                {
                    ((Button)item).IsEnabled = false;
                }
            }
        }
        private void OpenMenuBind(object sender, KeyEventArgs e)
        {
            KeysBinds.OpenMenu = e.Key;
            this.settingsMenu.KeyDown -= OpenMenuBind;
            Keyboard.ClearFocus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button)
                {
                    ((Button)item).IsEnabled = true;
                }
            }
            UpdateLabels();
        }
        private void ChangeFastReloadClick(object sender, RoutedEventArgs e)
        {
            this.settingsMenu.KeyDown += FastReloadBind;
            this.settingsMenu.Focus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button && item != sender)
                {
                    ((Button)item).IsEnabled = false;
                }
            }
        }
        private void FastReloadBind(object sender, KeyEventArgs e)
        {
            KeysBinds.FastReload = e.Key;
            this.settingsMenu.KeyDown -= FastReloadBind;
            Keyboard.ClearFocus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button)
                {
                    ((Button)item).IsEnabled = true;
                }
            }
            UpdateLabels();
        }
        private void ChangeMoveRightClick(object sender, RoutedEventArgs e)
        {
            this.settingsMenu.KeyDown += MoveRightBind;
            this.settingsMenu.Focus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button && item != sender)
                {
                    ((Button)item).IsEnabled = false;
                }
            }
        }
        private void MoveRightBind(object sender, KeyEventArgs e)
        {
            KeysBinds.MoveRight = e.Key;
            this.settingsMenu.KeyDown -= MoveRightBind;
            Keyboard.ClearFocus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button)
                {
                    ((Button)item).IsEnabled = true;
                }
            }
            UpdateLabels();
        }
        private void ChangeMoveLeftClick(object sender, RoutedEventArgs e)
        {
            this.settingsMenu.KeyDown += MoveLeftBind;
            this.settingsMenu.Focus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button && item != sender)
                {
                    ((Button)item).IsEnabled = false;
                }
            }
        }
        private void MoveLeftBind(object sender, KeyEventArgs e)
        {
            KeysBinds.MoveLeft = e.Key;
            this.settingsMenu.KeyDown -= MoveLeftBind;
            Keyboard.ClearFocus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button)
                {
                    ((Button)item).IsEnabled = true;
                }
            }
            UpdateLabels();
        }
        private void ChangeMoveDownClick(object sender, RoutedEventArgs e)
        {
            this.settingsMenu.KeyDown += MoveDownBind;
            this.settingsMenu.Focus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button && item != sender)
                {
                    ((Button)item).IsEnabled = false;
                }
            }
        }
        private void MoveDownBind(object sender, KeyEventArgs e)
        {
            KeysBinds.MoveDown = e.Key;
            this.settingsMenu.KeyDown -= MoveDownBind;
            Keyboard.ClearFocus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button)
                {
                    ((Button)item).IsEnabled = true;
                }
            }
            UpdateLabels();
        }

        private void ChangeMoveUpClick(object sender, RoutedEventArgs e)
        {
            this.settingsMenu.KeyDown += MoveUpBind;
            this.settingsMenu.Focus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button && item != sender)
                {
                    ((Button)item).IsEnabled = false;
                }
            }
        }
        private void MoveUpBind(object sender, KeyEventArgs e)
        {
            KeysBinds.MoveUp = e.Key;
            this.settingsMenu.KeyDown -= MoveUpBind;
            Keyboard.ClearFocus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button)
                {
                    ((Button)item).IsEnabled = true;
                }
            }
            UpdateLabels();
        }

        private void ChangeInteractionClick(object sender, RoutedEventArgs e)
        {
            this.settingsMenu.KeyDown += InteractionBind;
            this.settingsMenu.Focus();
            foreach (var item in settingsMenu.Children )
            {
                if(item is Button && item != sender)
                {
                    ((Button)item).IsEnabled = false;
                }
            }
        }

        private void InteractionBind(object sender, KeyEventArgs e)
        {
            KeysBinds.Interaction = e.Key;
            this.settingsMenu.KeyDown -= InteractionBind;
            Keyboard.ClearFocus();
            foreach (var item in settingsMenu.Children)
            {
                if (item is Button)
                {
                    ((Button)item).IsEnabled = true;
                }
            }
            UpdateLabels();
        }

        private void ResetToBaseClick(object sender, RoutedEventArgs e)
        {
            KeysBinds.ResetBindsToBase();
            UpdateLabels();
        }

        private void SaveСhangesClick(object sender, RoutedEventArgs e)
        {
            KeysBinds.SaveNewKeyBinds();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.window.Children.Remove(this.settingsMenu);
            KeysBinds.RestoreKeyBinds();
        }

        private void UpdateLabels()
        {
            foreach (var item in settingsMenu.Children)
            {
                if(item is Label)
                {
                    if(((Label)item).Name == "Interaction")
                    {
                        ((Label)item).Content = "Interaction:     " + '\'' + KeysBinds.Interaction + '\'';
                    }
                    else if (((Label)item).Name == "MoveUp")
                    {
                        ((Label)item).Content = "MoveUp:     " + '\'' + KeysBinds.MoveUp + '\'';
                    }
                    else if (((Label)item).Name == "MoveDown")
                    {
                        ((Label)item).Content = "MoveDown:     " + '\'' + KeysBinds.MoveDown + '\'';
                    }
                    else if (((Label)item).Name == "MoveLeft")
                    {
                        ((Label)item).Content = "MoveLeft:     " + '\'' + KeysBinds.MoveLeft + '\'';
                    }
                    else if (((Label)item).Name == "MoveRight")
                    {
                        ((Label)item).Content = "MoveRight:     " + '\'' + KeysBinds.MoveRight + '\'';
                    }
                    else if (((Label)item).Name == "FastReload")
                    {
                        ((Label)item).Content = "FastReload:     " + '\'' + KeysBinds.FastReload + '\'';
                    }
                    else if (((Label)item).Name == "OpenMenu")
                    {
                        ((Label)item).Content = "OpenMenu:     " + '\'' + KeysBinds.OpenMenu + '\'';
                    }
                    else if (((Label)item).Name == "Shot")
                    {
                        ((Label)item).Content = "Shot:     " + '\'' + KeysBinds.Shot + '\'';
                    }
                }
            }
        }
        
        public void Open()
        {
            this.window.Children.Add(this.settingsMenu);
            UpdateLabels();
        }




    }
}
