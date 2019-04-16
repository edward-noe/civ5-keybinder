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

        public List<Hotkey> GetHotkeys()
        {
            List<Hotkey> list = new List<Hotkey>();

            foreach (XmlNode row in DocumentElement.ChildNodes[2])
            {
                //
            }

            return list;
        }
    }
}
