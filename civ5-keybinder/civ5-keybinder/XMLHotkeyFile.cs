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

        public void ShowFile()
        {
            // Builds.row
            //MessageBox.Show(DocumentElement.ChildNodes[2].ChildNodes[0].ChildNodes[0].InnerText);
            //foreach (XmlNode row in DocumentElement.ChildNodes[2])
            //{
            //    MessageBox.Show(row.SelectSingleNode("Type").InnerText);
            //}
        }

        public List<Hotkey> GetHotkeys(int fileNum)
        {
            List<Hotkey> hotkeys = new List<Hotkey>();

            Dictionary<string, int> nameDict = new Dictionary<string, int>
            {
                // Dictionary that connects in-file hotkey name with ID as created in DefaultHotkeys() in MainWindow.xaml.cs
                // CIV5Builds.xml base game only
                {"BUILD_ROAD", 53},
                {"BUILD_RAILROAD", 54},
                {"BUILD_FARM", 58},
                {"BUILD_MINE", 59},
                {"BUILD_TRADING_POST", 56},
                {"BUILD_LUMBERMILL", 67},
                {"BUILD_PASTURE", 62},
                {"BUILD_CAMP", 57},
                {"BUILD_PLANTATION", 60},
                {"BUILD_QUARRY", 61},
                {"BUILD_WELL", 64},
                {"BUILD_OFFSHORE_PLATFORM", 65},
                {"BUILD_FISHING_BOATS", 66},
                {"BUILD_FORT", 63},
                {"BUILD_REMOVE_JUNGLE", 50},
                {"BUILD_REMOVE_FOREST", 49},
                {"BUILD_REMOVE_MARSH", 51},
                {"BUILD_SCRUB_FALLOUT", 71},
                {"BUILD_REPAIR", 70},
                {"BUILD_REMOVE_ROUTE", 55},
                {"BUILD_LANDMARK", 47},
                {"BUILD_ACADEMY", 44},
                {"BUILD_CUSTOMS_HOUSE", 42},
                {"BUILD_MANUFACTORY", 46},
                {"BUILD_CITADEL", 43},
            };

            foreach (XmlNode row in DocumentElement.ChildNodes[2])
            {
                //// Determines the hotkey it needs to modify
                //int ID = nameDict[row.SelectSingleNode("Type").InnerText];

                //// Creates new hotkey based on node info
                ////hotkeys.Add(ID, new Hotkey(fileNum, 0, MainWindow.Hotkeys[ID].Function, row.SelectSingleNode("HotKey").InnerText, row.));

                //hotkeys.Add(new Hotkey(
                //    ID,
                //    fileNum,
                //    0,
                //    MainWindow.Hotkeys[ID].Function, // how does this even work?
                //    GetKey(row.SelectSingleNode("HotKey").InnerText),
                //    false,
                //    false,
                //    false));

                
            }

            return hotkeys;
        }

        public string GetKey(string key)
        {
            return key[3].ToString();
        }
    }
}
