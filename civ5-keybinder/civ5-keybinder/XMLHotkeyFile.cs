using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Xml;

namespace civ5_keybinder
{
    class XMLHotkeyFile : XmlDocument
    {
        private static Dictionary<string, string> UserToFileFormatKeyDict { get; } = new Dictionary<string, string>
        {
            // TODO: Update dictionary to include more keys
            {"KB_RETURN", "Enter" },
            {"KB_HOME", "Home" },
            {"KB_END", "End" },
            {"KB_SPACE", "Space" },
            {"KB_BACKSPACE", "Backspace" },
            {"KB_DELETE", "Delete" },
            {"KB_SEMICOLON", ";" },
            {"KB_COMMA", "," },
            {"KB_SLASH", "/" }
        };

        private static Dictionary<string, string> FileToUserFormatKeyDict { get; } = new Dictionary<string, string>
        {
            {"Enter", "KB_RETURN" },
            {"Home", "KB_HOME" },
            {"End", "KB_END" },
            {"Space", "KB_SPACE" },
            {"Backspace", "KB_BACKSPACE" },
            {"Delete", "KB_DELETE" },
            {";", "KB_SEMICOLON" },
            {",", "KB_COMMA" },
            {"/", "KB_SLASH" }
        };

        private string HotkeyType { get; }

        private string FilePath { get; }

        // TODO: Rely on MainWindow.hotkeyDataFile
        readonly HotkeyDataFile hotkeyDataFile = new HotkeyDataFile("Resources\\HotkeyData.xml");

        public XMLHotkeyFile(string filePath)
        {
            FilePath = filePath;

            // Ignores comments and reads file
            XmlReaderSettings readerSettings = new XmlReaderSettings
            {
                IgnoreComments = true
            };
            XmlReader reader = XmlReader.Create(filePath, readerSettings);
            Load(reader);

            // Finds HotkeyType by parsing file name
            HotkeyType = System.IO.Path.GetFileNameWithoutExtension(filePath);
            if (HotkeyType.Contains("_"))
            {
                // Finds index of first "_" character
                int index = HotkeyType.IndexOf("_");
                // Substring(4, index - 4) returns string after CIV5 and excluding text after "_"; ex. CIV5Builds_Inherited_Expansion2 returns Builds
                HotkeyType = HotkeyType.Substring(4, index - 4);
            }
            else
            {
                // Substring(4) returns string after CIV5; ex. CIV5Builds returns Builds
                HotkeyType = HotkeyType.Substring(4);
            }
        }

        public Binding GetBinding(string hotkeyName)
        {
            foreach (XmlNode node in DocumentElement.SelectSingleNode("/GameData/" + HotkeyType))
            {
                if (node.SelectSingleNode("Type").InnerText == hotkeyName)
                {
                    Binding binding = new Binding(ConvertFileToUserFormat(node.SelectSingleNode("HotKey").InnerText), false, false, false);

                    // Note: This section of code is slow in Debug mode, but fine in Release mode
                    try
                    {
                        if (node.SelectSingleNode("CtrlDown").InnerText.Equals("1")) { binding.Ctrl = true; }
                    }
                    catch (NullReferenceException) { }

                    try
                    {
                        if (node.SelectSingleNode("ShiftDown").InnerText.Equals("1")) { binding.Shift = true; }
                    }
                    catch (NullReferenceException) { }

                    try
                    {
                        if (node.SelectSingleNode("AltDown").InnerText.Equals("1")) { binding.Alt = true; }
                    }
                    catch (NullReferenceException) { }

                    return binding;
                }
            }
            return null;
        }

        public void SetBinding(string hotkeyName, Binding binding)
        {
            foreach (XmlNode node in DocumentElement.SelectSingleNode("/GameData/" + HotkeyType))
            {
                if (node.SelectSingleNode("Type").InnerText == hotkeyName)
                {
                    node.SelectSingleNode("HotKey").InnerText = ConvertUserToFileFormat(binding.Key);

                    try
                    {
                        node.SelectSingleNode("CtrlDown").InnerText = Convert.ToInt32(binding.Ctrl).ToString();
                    }
                    catch (NullReferenceException)
                    {
                        XmlElement element = CreateElement("CtrlDown");
                        node.InsertAfter(element, node.SelectSingleNode("Hotkey"));
                        node.SelectSingleNode("CtrlDown").InnerText = Convert.ToInt32(binding.Ctrl).ToString();
                    }

                    try
                    {
                        node.SelectSingleNode("ShiftDown").InnerText = Convert.ToInt32(binding.Shift).ToString();
                    }
                    catch (NullReferenceException)
                    {
                        XmlElement element = CreateElement("ShiftDown");
                        node.InsertAfter(element, node.SelectSingleNode("Hotkey"));
                        node.SelectSingleNode("ShiftDown").InnerText = Convert.ToInt32(binding.Shift).ToString();
                    }

                    try
                    {
                        node.SelectSingleNode("AltDown").InnerText = Convert.ToInt32(binding.Alt).ToString();
                    }
                    catch (NullReferenceException)
                    {
                        XmlElement element = CreateElement("AltDown");
                        node.InsertAfter(element, node.SelectSingleNode("Hotkey"));
                        node.SelectSingleNode("AltDown").InnerText = Convert.ToInt32(binding.Alt).ToString();
                    }
                }
            }
            Save(FilePath);
        }

        private string ConvertFileToUserFormat(string fileFormat)
        {
            if (UserToFileFormatKeyDict.ContainsKey(fileFormat))
            {
                return UserToFileFormatKeyDict[fileFormat];
            }
            else
            {
                // Returns 4th character; ex. A in KB_A
                return fileFormat.Substring(3);
            }
        }

        private string ConvertUserToFileFormat(string userFormat)
        {
            if (FileToUserFormatKeyDict.ContainsKey(userFormat))
            {
                return FileToUserFormatKeyDict[userFormat];
            }
            else
            {
                return "KB_" + userFormat;
            }
        }
    }
}
