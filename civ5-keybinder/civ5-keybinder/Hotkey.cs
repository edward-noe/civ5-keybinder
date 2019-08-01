using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace civ5_keybinder
{
    public class Hotkey
    {
        public string Name { get; }
        public int ID { get; } // Used to order hotkeys in MainWindow
        public string File { get; }
        public string DLC { get; }
        public string Function { get; }
        public string Key { get; set; }
        public bool Ctrl { get; set; }
        public bool Shift { get; set; }
        public bool Alt { get; set; }

        public Hotkey(string defName, int defID, string defFile, int defDLC, string defFunction, string defKey, bool defCtrl, bool defShift, bool defAlt)
        {
            Name = defName;
            ID = defID;
            File = defFile;
            switch (defDLC)
            {
                case 0:
                    DLC = "";
                    break;
                case 1:
                    DLC = "G + K";
                    break;
                case 2:
                    DLC = "BNW";
                    break;
            }
            Function = defFunction;
            Key = defKey;
            Ctrl = defCtrl;
            Shift = defShift;
            Alt = defAlt;
        }

        public void DefineBinding(string defKey, bool defCtrl, bool defShift, bool defAlt)
        {
            Key = defKey;
            Ctrl = defCtrl;
            Shift = defShift;
            Alt = defAlt;
        }
    }
}