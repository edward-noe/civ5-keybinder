using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace civ5_keybinder
{
    public class HotkeyDataFile : XmlDocument
    {
        public HotkeyDataFile(string filePath)
        {
            // Reads HotkeyData.xml
            XmlReaderSettings settings = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(filePath, settings);
            Load(reader);
        }

        public int GetIntAttribute(string attribute, string hotkeyName)
        {
            // Selects hotkey node with hotkeyName
            XmlNode hotkey = SelectSingleNode("//Hotkey[Name =\'" + hotkeyName + "\']");

            // Returns attribute node of hotkey
            return int.Parse(hotkey.SelectSingleNode(attribute).InnerText);
        }

        public string GetStringAttribute(string attribute, string hotkeyName)
        {
            // Selects hotkey node with hotkeyName
            XmlNode hotkey = SelectSingleNode("//Hotkey[Name =\'" + hotkeyName + "\']");

            // Returns attribute node of hotkey
            return hotkey.SelectSingleNode(attribute).InnerText;
        }

        public bool GetBoolAttribute(string attribute, string hotkeyName)
        {
            // Selects hotkey node with hotkeyName
            XmlNode hotkey = SelectSingleNode("//Hotkey[Name =\'" + hotkeyName + "\']");

            // Returns attribute node of hotkey
            return Convert.ToBoolean(int.Parse(hotkey.SelectSingleNode(attribute).InnerText));
        }

        // Returns list of each hotkey name
        public List<string> GetHotkeyNames()
        {
            List<string> names = new List<string>();

            foreach (XmlNode hotkey in DocumentElement)
            {
                names.Add(hotkey.SelectSingleNode("Name").InnerText);
            }

            return names;
        }

        public Tuple<string, bool, bool, bool> GetDefaultHotkeyBinding(string name)
        {
            foreach (XmlNode hotkey in DocumentElement)
            {
                if (hotkey.SelectSingleNode("Name").InnerText == name)
                {
                    return new Tuple<string, bool, bool, bool>(
                        hotkey.SelectSingleNode("Key").InnerText,
                        Convert.ToBoolean(int.Parse(hotkey.SelectSingleNode("Ctrl").InnerText)),
                        Convert.ToBoolean(int.Parse(hotkey.SelectSingleNode("Shift").InnerText)),
                        Convert.ToBoolean(int.Parse(hotkey.SelectSingleNode("Alt").InnerText)));
                }
            }
            // TODO: Better error handling
            return null;
        }

        // Gets group number from file attribute
        public int GetGroupNumber(string name)
        {
            char groupChar = GetStringAttribute("File", name)[0];
            return int.Parse(groupChar.ToString());
        }
    }
}