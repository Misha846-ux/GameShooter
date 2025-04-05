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
        private class KeyBindLabel
        {
            private static int countOFLabels;
            private Canvas window;
            private Label text;
            private Key key;
            private MouseButton mouseButton;
            private string content;

            static KeyBindLabel()
            {
                countOFLabels = 1;
            }
            public KeyBindLabel(string bindName, Key bindKey, Canvas window)
            {
                this.window = window;
                this.content = bindName;
                this.key = bindKey;

                double buttonWidth = this.window.Width / 10;
                double buttonHeight = this.window.Height / 15;
                text = new Label
                {
                    Name = this.content,
                    FontSize = 20,
                    Background = new SolidColorBrush(Colors.Black),
                    Foreground = new SolidColorBrush(Colors.White),
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    Width = this.window.Width,
                    Height = buttonHeight,
                    BorderBrush = new SolidColorBrush(Colors.White),
                };
                this.window.Children.Add(text);
                Canvas.SetTop(text, buttonHeight * KeyBindLabel.countOFLabels);
                text.Content = this.content + ":      " + '\'' + this.key + '\'';
                {
                    Button changetext = new Button
                    {
                        Content = "Change",
                        FontSize = 20,
                        Foreground = new SolidColorBrush(Colors.White),
                        Width = buttonWidth,
                        Height = buttonHeight,
                        Background = new SolidColorBrush(Colors.Black),
                        BorderBrush = new SolidColorBrush(Colors.White),
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    changetext.Background.Opacity = 0;
                    Canvas.SetLeft(changetext, window.Width - buttonWidth);
                    Canvas.SetTop(changetext, buttonHeight * KeyBindLabel.countOFLabels);
                    this.window.Children.Add(changetext);
                    changetext.Click += ChangeKeyClick;
                }
                KeyBindLabel.countOFLabels++;

            }



            public KeyBindLabel(string content, MouseButton bindingKey, Canvas window)
            {
                this.window = window;
                this.content = content;
                this.mouseButton = bindingKey;

                double buttonWidth = this.window.Width / 10;
                double buttonHeight = this.window.Height / 15;
                text = new Label
                {
                    Name = this.content,
                    FontSize = 20,
                    Background = new SolidColorBrush(Colors.Black),
                    Foreground = new SolidColorBrush(Colors.White),
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    Width = this.window.Width,
                    Height = buttonHeight,
                    BorderBrush = new SolidColorBrush(Colors.White),
                };
                this.window.Children.Add(text);
                Canvas.SetTop(text, buttonHeight * KeyBindLabel.countOFLabels);
                text.Content = this.content + ":      " + '\'' + this.mouseButton + '\'';
                {
                    Button changetext = new Button
                    {
                        Content = "Change",
                        FontSize = 20,
                        Foreground = new SolidColorBrush(Colors.White),
                        Width = buttonWidth,
                        Height = buttonHeight,
                        Background = new SolidColorBrush(Colors.Black),
                        BorderBrush = new SolidColorBrush(Colors.White),
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    changetext.Background.Opacity = 0;
                    Canvas.SetLeft(changetext, window.Width - buttonWidth);
                    Canvas.SetTop(changetext, buttonHeight * KeyBindLabel.countOFLabels);
                    this.window.Children.Add(changetext);
                    changetext.Click += ChangeMouseClick;
                }
                KeyBindLabel.countOFLabels++;

            }
            private void SetBindKey(KeyEventArgs e)
            {
                switch (this.content)
                {
                    case "MoveUp":
                        KeysBinds.MoveUp = e.Key;
                        break;
                    case "MoveDown":
                        KeysBinds.MoveDown = e.Key;
                        break;
                    case "MoveLeft":
                        KeysBinds.MoveLeft = e.Key;
                        break;
                    case "MoveRight":
                        KeysBinds.MoveRight = e.Key;
                        break;
                    case "Interaction":
                        KeysBinds.Interaction = e.Key;
                        break;
                    case "FastReload":
                        KeysBinds.FastReload = e.Key;
                        break;
                    case "OpenMenu":
                        KeysBinds.OpenMenu = e.Key;
                        break;
                    case "Slot1":
                        KeysBinds.Slot1 = e.Key;
                        break;
                    case "Slot2":
                        KeysBinds.Slot2 = e.Key;
                        break;
                    case "Slot3":
                        KeysBinds.Slot3 = e.Key;
                        break;
                    default:
                        break;
                }
            }

            private void UpdateKey()
            {
                switch (this.content)
                {
                    case "MoveUp":
                        this.key = KeysBinds.MoveUp;
                        break;
                    case "MoveDown":
                        this.key = KeysBinds.MoveDown;
                        break;
                    case "MoveLeft":
                        this.key = KeysBinds.MoveLeft;
                        break;
                    case "MoveRight":
                        this.key = KeysBinds.MoveRight;
                        break;
                    case "Interaction":
                        this.key = KeysBinds.Interaction;
                        break;
                    case "FastReload":
                        this.key = KeysBinds.FastReload;
                        break;
                    case "OpenMenu":
                        this.key = KeysBinds.OpenMenu;
                        break;
                    case "Slot1":
                        this.key = KeysBinds.Slot1;
                        break;
                    case "Slot2":
                        this.key = KeysBinds.Slot2;
                        break;
                    case "Slot3":
                        this.key = KeysBinds.Slot3;
                        break;
                    case "Shot":
                        this.mouseButton = KeysBinds.Shot;
                        break;
                    default:
                        break;
                }
            }

            private void ChangeMouseClick(object sender, RoutedEventArgs e)
            {
                this.window.MouseDown += MouseBind;
                this.window.Focus();
                foreach (var item in window.Children)
                {
                    if (item is Button && item != sender)
                    {
                        ((Button)item).IsEnabled = false;
                    }
                }
            }
            private void MouseBind(object sender, MouseButtonEventArgs e)
            {
                KeysBinds.Shot = e.ChangedButton;
                this.mouseButton = e.ChangedButton;
                this.window.MouseDown -= MouseBind;
                Keyboard.ClearFocus();
                foreach (var item in window.Children)
                {
                    if (item is Button)
                    {
                        ((Button)item).IsEnabled = true;
                    }
                }
                this.text.Content = this.content + ":      " + '\'' + this.mouseButton + '\'';
            }

            private void ChangeKeyClick(object sender, RoutedEventArgs e)
            {
                this.window.KeyDown += KeyBind;
                this.window.Focus();
                foreach (var item in window.Children)
                {
                    if (item is Button && item != sender)
                    {
                        ((Button)item).IsEnabled = false;
                    }
                }
            }
            private void KeyBind(object sender, KeyEventArgs e)
            {
                this.key = e.Key;
                this.SetBindKey(e);
                this.window.KeyDown -= KeyBind;
                Keyboard.ClearFocus();
                foreach (var item in window.Children)
                {
                    if (item is Button)
                    {
                        ((Button)item).IsEnabled = true;
                    }
                }
                this.text.Content = this.content + ":      " + '\'' + this.key + '\'';
            }

            public void UpdateLabel()
            {
                this.UpdateKey();
                if(this.content == "Shot")
                {
                    this.text.Content = this.content + ":      " + '\'' + this.mouseButton + '\'';
                }
                else
                {
                    this.text.Content = this.content + ":      " + '\'' + this.key + '\'';
                }
            }
        }

        private Canvas settingsMenu; // 
        private Canvas window; //the previous page on which the settings menu is based
        private List<KeyBindLabel> keyBindLabels;




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
            // the line that is at the bottom and is always visible
            Rectangle bottomLine = new Rectangle 
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
            //Go back
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
            //SaveChanges
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
            //ResetToBase
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

            {// for movement
                Label movementSelection = new Label
                {
                    Content = "Movement Selection",
                    FontSize = 20,
                    Background = new SolidColorBrush(Colors.Black),
                    Foreground = new SolidColorBrush(Colors.White),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Width = this.settingsMenu.Width,
                    Height = buttonHeight,
                    BorderBrush = new SolidColorBrush(Colors.White),
                    BorderThickness = new Thickness(bottomLine.StrokeThickness)
                };
                this.settingsMenu.Children.Add(movementSelection);
            }
            this.keyBindLabels = new List<KeyBindLabel>();
            this.keyBindLabels.Add(new KeyBindLabel("MoveUp", KeysBinds.MoveUp, this.settingsMenu));

            this.keyBindLabels.Add(new KeyBindLabel("MoveDown", KeysBinds.MoveDown, this.settingsMenu));

            this.keyBindLabels.Add(new KeyBindLabel("MoveLeft", KeysBinds.MoveLeft, this.settingsMenu));

            this.keyBindLabels.Add(new KeyBindLabel("MoveRight", KeysBinds.MoveRight, this.settingsMenu));

            this.keyBindLabels.Add(new KeyBindLabel("Interaction", KeysBinds.Interaction, this.settingsMenu));

            this.keyBindLabels.Add(new KeyBindLabel("OpenMenu", KeysBinds.OpenMenu, this.settingsMenu));

            this.keyBindLabels.Add(new KeyBindLabel("FastReload", KeysBinds.FastReload, this.settingsMenu));

            this.keyBindLabels.Add(new KeyBindLabel("Slot1", KeysBinds.Slot1, this.settingsMenu));

            this.keyBindLabels.Add(new KeyBindLabel("Slot2", KeysBinds.Slot2, this.settingsMenu));

            this.keyBindLabels.Add(new KeyBindLabel("Slot3", KeysBinds.Slot3, this.settingsMenu));

            this.keyBindLabels.Add(new KeyBindLabel("Shot", KeysBinds.Shot, this.settingsMenu));

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
            foreach (var item in keyBindLabels)
            {
                item.UpdateLabel();
            }
        }
        
        public void Open()
        {
            this.window.Children.Add(this.settingsMenu);
            UpdateLabels();
        }




    }
}
