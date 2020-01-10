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

        public List<Hotkey> GetDefaultHotkeys()
        {
            List<Hotkey> hotkeys = new List<Hotkey>();

            foreach (XmlNode hotkey in DocumentElement)
            {
                hotkeys.Add(new Hotkey(
                    hotkey.SelectSingleNode("Name").InnerText,
                    int.Parse(hotkey.SelectSingleNode("ID").InnerText),
                    //hotkey.SelectSingleNode("File").InnerText,
                    int.Parse(hotkey.SelectSingleNode("DLC").InnerText),
                    hotkey.SelectSingleNode("Function").InnerText,
                    hotkey.SelectSingleNode("Key").InnerText,
                    Convert.ToBoolean(int.Parse(hotkey.SelectSingleNode("Ctrl").InnerText)),
                    Convert.ToBoolean(int.Parse(hotkey.SelectSingleNode("Shift").InnerText)),
                    Convert.ToBoolean(int.Parse(hotkey.SelectSingleNode("Alt").InnerText))));
            }

            return hotkeys;
        }

        //public Dictionary<int, bool> GetIDDictionary()
        //{
        //    Dictionary<int, bool> dictionary = new Dictionary<int, bool>();

        //    foreach (XmlNode hotkey in DocumentElement)
        //    {
        //        dictionary.Add(int.Parse(hotkey.SelectSingleNode("ID").InnerText), false);
        //    }

        //    return dictionary;
        //}
    }
}