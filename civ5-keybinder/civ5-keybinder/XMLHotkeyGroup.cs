using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace civ5_keybinder
{
    // Manages a group of XMLHotkeyFiles
    public class XMLHotkeyGroup
    {
        List<XMLHotkeyFile> Files { get; set; } = new List<XMLHotkeyFile>();

        // Creates an XMLHotkeyGroup with files 1, 1-3, or 1-6
        // File 1 (Always): Base file
        // File 2 (Sometimes): Expansion1 base file
        // File 3 (Sometimes): Expansion2 base file
        // File 4 (Sometimes) (only used by Builds): Expansion1 file inhereted from base
        // File 5 (Sometimes) (only used by Builds): Expansion2 file inhereted from base
        // File 6 (Sometimes) (only used by Builds): Expansion2 file inhereted from Expansion1
        public XMLHotkeyGroup(params string[] paths)
        {
            foreach (string path in paths)
            {
                Files.Add(new XMLHotkeyFile(path));
            }
        }

        public Binding GetBinding(string name)
        {
            if (Files.Count == 1)
            {
                return Files[0].GetBinding(name);
            }
            else
            {
                List<Binding> bindings = new List<Binding>();

                foreach (XMLHotkeyFile file in Files)
                {
                    Binding binding = file.GetBinding(name);
                    // TODO: Is the null check still neccesary?
                    if (binding != null)
                    {
                        bindings.Add(binding);
                    }
                }

                // TODO: Handle case where key or modifier key varies across files
                // NOTE: The below code is dangerous, it caused most keys to be overwritten across most files
                // NOTE: You should handle errors differently; don't just overwrite the key
                // If hotkeys across files differ, changes Key attribute to an error value
                //if (hotkeys.Any(item => !(item.Equals(hotkeys[0]))))
                //{
                //    foreach (Hotkey hotkey in hotkeys)
                //    {
                //        hotkey.Key = "ERROR: MULTIPLE KEYS";
                //    }
                //    return hotkeys[0];
                //}
                return bindings[0];
            }
        }

        public void SetBinding(string name, Binding binding)
        {
            foreach (XMLHotkeyFile file in Files)
            {
                file.SetBinding(name, binding);
            }
        }
    }
}