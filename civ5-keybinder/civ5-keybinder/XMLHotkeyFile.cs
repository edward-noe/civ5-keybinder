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

        public List<Hotkey> GetHotkeys()
        {
            List<string> hotkeyNames = hotkeyDataFile.GetHotkeyNames();

            List<Hotkey> hotkeys = new List<Hotkey>();

            // Selects hotkeyType node to accomodate different file configurations
            foreach (XmlNode node in DocumentElement.SelectSingleNode("/GameData/" + HotkeyType))
            {
                string hotkeyName = node.SelectSingleNode("Type").InnerText;

                // Checks to ensure that hotkey in file is in HotkeyData.xml
                if (hotkeyNames.Contains(hotkeyName))
                {
                    // TODO: Actually add Ctrl, Alt, and Shift functionality
                    hotkeys.Add(new Hotkey(
                        node.SelectSingleNode("Type").InnerText,
                        // Consults HotkeyData.xml to determine ID and other attributes
                        hotkeyDataFile.GetIntAttribute("ID", hotkeyName), 
                        hotkeyDataFile.GetStringAttribute("File", hotkeyName),
                        hotkeyDataFile.GetIntAttribute("DLC", hotkeyName),
                        hotkeyDataFile.GetStringAttribute("Function", hotkeyName),
                        // Converts from KB_A format to A format
                        ConvertFileToUserFormat(node.SelectSingleNode("HotKey").InnerText),
                        hotkeyDataFile.GetBoolAttribute("Ctrl", hotkeyName),
                        hotkeyDataFile.GetBoolAttribute("Shift", hotkeyName),
                        hotkeyDataFile.GetBoolAttribute("Alt", hotkeyName)));
                }
            }
            return hotkeys;
        }

        public Hotkey GetHotkey(string name)
        {
            foreach (XmlNode node in DocumentElement.SelectSingleNode("/GameData/" + HotkeyType)) {
                if (node.SelectSingleNode("Type").InnerText == name)
                {
                    return new Hotkey(
                        node.SelectSingleNode("Type").InnerText,
                        // Consults HotkeyData.xml to determine ID and other attributes
                        hotkeyDataFile.GetIntAttribute("ID", name),
                        hotkeyDataFile.GetStringAttribute("File", name),
                        hotkeyDataFile.GetIntAttribute("DLC", name),
                        hotkeyDataFile.GetStringAttribute("Function", name),
                        // Converts from KB_A format to A format
                        ConvertFileToUserFormat(node.SelectSingleNode("HotKey").InnerText),
                        hotkeyDataFile.GetBoolAttribute("Ctrl", name),
                        hotkeyDataFile.GetBoolAttribute("Shift", name),
                        hotkeyDataFile.GetBoolAttribute("Alt", name));
                }
            }
            return null;
        }

        public void SetHotkey(Hotkey hotkey)
        {
            foreach (XmlNode node in DocumentElement.SelectSingleNode("/GameData/" + HotkeyType))
            {
                if (node.SelectSingleNode("Type").InnerText == hotkey.Name)
                {
                    node.SelectSingleNode("HotKey").InnerText = ConvertUserToFileFormat(hotkey.Key);
                }
            }
            Save(FilePath);
        }

        public string ConvertFileToUserFormat(string fileFormat)
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

        public string ConvertUserToFileFormat(string userFormat)
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
