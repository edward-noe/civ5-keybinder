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
        public static Dictionary<string, string> userToFileFormatKeyDict { get; } = new Dictionary<string, string>
        {
            // TODO: Update dictionary to include more keys
            {"KB_RETURN", "Enter" },
            {"KB_HOME", "Home" },
            {"KB_END", "End" },
            {"KB_SPACE", "Space" },
            {"KB_BACKSPACE", "Backspace" },
            {"KB_DELETE", "Delete" },
            {"KB_SEMICOLON", "Semicolon" },
        };

        public string hotkeyType { get; }

        // File 1 (Mandatory): Base file
        // File 2 (Optional): Expansion1 base file
        // File 3 (Optional): Expansion2 base file
        // File 4 (Optional) (only used by Builds): Expansion1 file inhereted from base
        // File 5 (Optional) (only used by Builds): Expansion2 file inhereted from base
        // File 6 (Optional) (only used by Builds): Expansion2 file inhereted from Expansion1
        public XMLHotkeyFile(string filePath)
        {
            // Ignores comments and reads file
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(filePath, settings);
            Load(reader);

            // Finds kotkeyType by parsing file name
            // Substring(4) returns string after CIV5; ex. CIV5Builds returns Builds
            hotkeyType = System.IO.Path.GetFileNameWithoutExtension(filePath).Substring(4);
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
            foreach (XmlNode hotkey in DocumentElement.SelectSingleNode("/GameData/" + hotkeyType))
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
            if (userToFileFormatKeyDict.ContainsKey(fileFormat))
            {
                return userToFileFormatKeyDict[fileFormat];
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
