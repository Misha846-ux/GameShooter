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
using System.Xml.Linq;

using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Game.Creatures.Players
{
    [Serializable]
    internal class KeysBinds
    {

        private static Key BasicInteraction;
        private static Key BasicMoveUp;
        private static Key BasicMoveDown;
        private static Key BasicMoveLeft;
        private static Key BasicMoveRight;
        private static Key BasicFastReload;
        private static Key BasicOpenMenu;
        private static MouseButton BasicShot;
        
        public static Key Interaction {  get; set; }
        public static Key MoveUp {  get; set; }
        public static Key MoveDown { get; set; }
        public static Key MoveLeft { get; set; }
        public static Key MoveRight { get; set; }
        public static Key FastReload { get; set; }
        public static Key OpenMenu { get; set; }
        public static MouseButton Shot { get; set; }

        private static string keyboardFilePath;
        private static string mouseFilePath;
        private static List<Key> keyboardBinds;
        private static List<MouseButton> mouseBinds;

        static KeysBinds()
        { 

            keyboardFilePath = @"..\..\..\Resources\SavedData\KeysBinds\KeyboardBinds.xml";
            mouseFilePath = @"..\..\..\Resources\SavedData\KeysBinds\MouseBinds.xml";
            {
                BasicInteraction = Key.E;
                BasicMoveUp = Key.W;
                BasicMoveDown = Key.S;
                BasicMoveLeft = Key.A;
                BasicMoveRight = Key.D;
                BasicFastReload = Key.R;
                BasicOpenMenu = Key.Escape;
                BasicShot = MouseButton.Left;
            }
            keyboardBinds = DeserializeKeysBinds<Key>(keyboardFilePath);
            mouseBinds = DeserializeKeysBinds<MouseButton>(mouseFilePath);
            {
                Interaction = keyboardBinds[0];
                MoveUp = keyboardBinds[1];
                MoveDown = keyboardBinds[2];
                MoveLeft = keyboardBinds[3];
                MoveRight = keyboardBinds[4];
                FastReload = keyboardBinds[5];
                OpenMenu = keyboardBinds[6];
                Shot = mouseBinds[0];
            }
            
        }

        public static void ResetBindsToBase()
        {
            KeysBinds.Interaction = KeysBinds.BasicInteraction;
            KeysBinds.MoveUp = KeysBinds.BasicMoveUp;
            KeysBinds.MoveDown = KeysBinds.BasicMoveDown;
            KeysBinds.MoveLeft = KeysBinds.BasicMoveLeft;
            KeysBinds.MoveRight = KeysBinds.BasicMoveRight;
            KeysBinds.FastReload = KeysBinds.BasicFastReload;
            KeysBinds.OpenMenu = KeysBinds.BasicOpenMenu;
            KeysBinds.Shot = KeysBinds.BasicShot;
            KeysBinds.SaveNewKeyBinds();
        }

        public static void SaveNewKeyBinds()
        {
            KeysBinds.keyboardBinds.Clear();
            KeysBinds.mouseBinds.Clear();
            KeysBinds.keyboardBinds = new List<Key> { 
                KeysBinds.Interaction, KeysBinds.MoveUp, KeysBinds.MoveDown, 
                KeysBinds.MoveLeft, KeysBinds.MoveRight, KeysBinds.FastReload, KeysBinds.OpenMenu
            };
            KeysBinds.mouseBinds = new List<MouseButton> { KeysBinds.Shot };

            KeysBinds.SerializeKeysBinds(KeysBinds.keyboardBinds, KeysBinds.keyboardFilePath);
            KeysBinds.SerializeKeysBinds(KeysBinds.mouseBinds, KeysBinds.mouseFilePath);
        }

        public static void RestoreKeyBinds()
        {
            KeysBinds.keyboardBinds.Clear();
            KeysBinds.mouseBinds.Clear();
            KeysBinds.keyboardBinds = KeysBinds.DeserializeKeysBinds<Key>(KeysBinds.keyboardFilePath);
            KeysBinds.mouseBinds = KeysBinds.DeserializeKeysBinds<MouseButton>(KeysBinds.mouseFilePath);
            {
                KeysBinds.Interaction = KeysBinds.keyboardBinds[0];
                KeysBinds.MoveUp = KeysBinds.keyboardBinds[1];
                KeysBinds.MoveDown = KeysBinds.keyboardBinds[2];
                KeysBinds.MoveLeft = KeysBinds.keyboardBinds[3];
                KeysBinds.MoveRight = KeysBinds.keyboardBinds[4];
                KeysBinds.FastReload = KeysBinds.keyboardBinds[5];
                KeysBinds.OpenMenu = KeysBinds.keyboardBinds[6];
                Shot = mouseBinds[0];
            }
        }


        private static void SerializeKeysBinds<T>(T keyList, string path)
        {
            XmlSerializer serializerForKeys = new XmlSerializer((typeof(T)));
            using (StreamWriter writer = new StreamWriter(path))
            {
                serializerForKeys.Serialize(writer, keyList);
            }
        }

        private static List<T> DeserializeKeysBinds<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (List<T>)serializer.Deserialize(reader);
            }
        }


    }
}
