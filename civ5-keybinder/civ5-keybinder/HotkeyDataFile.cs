using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace civ5_keybinder
{
    class HotkeyDataFile : XmlDocument
    {
        public HotkeyDataFile(string filePath)
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            XmlReader reader = XmlReader.Create(filePath, settings);
            Load(reader);
        }
        
        public string GetFunction(string hotkeyName)
        {
            return "hello";
        }

        public List<Hotkey> GetDefaultHotkeys()
        {
            List<Hotkey> hotkeys = new List<Hotkey>();

            foreach (XmlNode hotkey in DocumentElement)
            {
                hotkeys.Add(new Hotkey(
                    hotkey.SelectSingleNode("Name").InnerText,
                    Int32.Parse(hotkey.SelectSingleNode("ID").InnerText),
                    hotkey.SelectSingleNode("File").InnerText,
                    Int32.Parse(hotkey.SelectSingleNode("DLC").InnerText),
                    hotkey.SelectSingleNode("Function").InnerText,
                    hotkey.SelectSingleNode("Key").InnerText,
                    Convert.ToBoolean(Convert.ToInt16(hotkey.SelectSingleNode("Ctrl").InnerText)),
                    Convert.ToBoolean(Convert.ToInt16(hotkey.SelectSingleNode("Shift").InnerText)),
                    Convert.ToBoolean(Convert.ToInt16(hotkey.SelectSingleNode("Alt").InnerText))));
            }

            //foreach (XmlNode hotkey in DocumentElement.ChildNodes[0])
            //{
            //    hotkeys.Add(new Hotkey(
            //        0,
            //        "1.1",
            //        0,
            //        "hello",
            //        "K",
            //        false,
            //        false,
            //        false));
            //}

            return hotkeys;
        }
    }
}
