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
        public static Dictionary<string, string> UserToFileFormatKeyDict { get; } = new Dictionary<string, string>
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

        public string HotkeyType { get; }

        public XMLHotkeyFile(string filePath)
        {
            // Ignores comments and reads file
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(filePath, settings);
            Load(reader);

            // Finds kotkeyType by parsing file name
            HotkeyType = System.IO.Path.GetFileNameWithoutExtension(filePath);
            if (HotkeyType.Contains("_"))
            {
                // Finds index of first "_" character
                int index = HotkeyType.IndexOf("_");
                // Substring(4, index) returns string after CIV5 and excluding text after "_"; ex. CIV5Builds_Inherited_Expansion2 returns Builds
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
            // Initializes HotkeyData.xml
            HotkeyDataFile hotkeyDataFile = new HotkeyDataFile("HotkeyData.xml");

            // Gets hotkey names from HotkeyData.xml
            List<string> hotkeyNames = hotkeyDataFile.GetHotkeyNames();

            // Creates list for output
            List<Hotkey> hotkeys = new List<Hotkey>();

            // Selects hotkeyType node to accomodate different file configurations
            foreach (XmlNode hotkey in DocumentElement.SelectSingleNode("/GameData/" + HotkeyType))
            {
                string hotkeyName = hotkey.SelectSingleNode("Type").InnerText;

                // Checks to ensure that hotkey in file is in HotkeyData.xml
                if (hotkeyNames.Contains(hotkeyName))
                {
                    hotkeys.Add(new Hotkey(
                        hotkey.SelectSingleNode("Type").InnerText,
                        // Consults HotkeyData.xml to determine ID and other attributes
                        hotkeyDataFile.GetIntAttribute("ID", hotkeyName), 
                        hotkeyDataFile.GetStringAttribute("File", hotkeyName),
                        hotkeyDataFile.GetIntAttribute("DLC", hotkeyName),
                        hotkeyDataFile.GetStringAttribute("Function", hotkeyName),
                        // Converts from KB_A format to A format
                        ConvertFileToUserFormat(hotkey.SelectSingleNode("HotKey").InnerText),
                        hotkeyDataFile.GetBoolAttribute("Ctrl", hotkeyName),
                        hotkeyDataFile.GetBoolAttribute("Shift", hotkeyName),
                        hotkeyDataFile.GetBoolAttribute("Alt", hotkeyName)));
                }
            }

            return hotkeys;
        }

        public void SetHotkeys(List<Hotkey> hotkeys)
        {
            ;
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

        public string GetKey(string key)
        {
            return key[3].ToString();
        }
    }
}
