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
        public XMLHotkeyFile(string filePath)
        {
            // Ignores comments and reads file
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(filePath, settings);
            Load(reader);
        }

        public List<Hotkey> GetHotkeys(int fileNum)
        {
            // Initializes HotkeyData.xml
            HotkeyDataFile hotkeyDataFile = new HotkeyDataFile("HotkeyData.xml");
            // Gets hotkey names from HotkeyData.xml
            List<string> hotkeyNames = hotkeyDataFile.GetHotkeyNames();

            // Creates list for output
            List<Hotkey> hotkeys = new List<Hotkey>();

            foreach (XmlNode hotkey in DocumentElement.ChildNodes[2])
            {
                string hotkeyName = hotkey.SelectSingleNode("Type").InnerText;

                // Checks to ensure that hotkey in file is in HotkeyData.xml
                if (hotkeyNames.Contains(hotkeyName))
                {
                    hotkeys.Add(new Hotkey(
                        hotkey.SelectSingleNode("Type").InnerText, 
                        hotkeyDataFile.GetIntAttribute("ID", hotkeyName), // Consults HotkeyData.xml to determine ID and other attributes
                        hotkeyDataFile.GetStringAttribute("File", hotkeyName),
                        hotkeyDataFile.GetIntAttribute("DLC", hotkeyName),
                        hotkeyDataFile.GetStringAttribute("Function", hotkeyName),
                        hotkey.SelectSingleNode("HotKey").InnerText,
                        hotkeyDataFile.GetBoolAttribute("Ctrl", hotkeyName),
                        hotkeyDataFile.GetBoolAttribute("Shift", hotkeyName),
                        hotkeyDataFile.GetBoolAttribute("Alt", hotkeyName)));
                }
            }

            return hotkeys;
        }

        public string GetKey(string key)
        {
            return key[3].ToString();
        }
    }
}
