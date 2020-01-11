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
        public int Group { get; }
        public string DLC { get; }
        public string Function { get; }
        public Binding Binding { get; set; }

        public Hotkey(string defName, int defID, int defGroup, int defDLC, string defFunction, Binding defBinding)
        {
            Name = defName;
            ID = defID;
            Group = defGroup;
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
            Binding = defBinding;
        }
    }
}