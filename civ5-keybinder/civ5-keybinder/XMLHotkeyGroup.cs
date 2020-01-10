using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace civ5_keybinder
{
    // Manages a group of XMLHotkeyFiles
    class XMLHotkeyGroup
    {
        List<XMLHotkeyFile> Files { get; set; } = new List<XMLHotkeyFile>();

        // Creates an XMLHotkeyGroup with files 1, 1-3, or 1-6
        // File 1 (Always): Base file
        // File 2 (Sometimes): Expansion1 base file
        // File 3 (Sometimes): Expansion2 base file
        // File 4 (Sometimes) (only used by Builds): Expansion1 file inhereted from base
        // File 5 (Sometimes) (only used by Builds): Expansion2 file inhereted from base
        // File 6 (Sometimes) (only used by Builds): Expansion2 file inhereted from Expansion1
        public XMLHotkeyGroup(string file1Path)
        {
            Files.Add(new XMLHotkeyFile(file1Path));
        }

        public XMLHotkeyGroup(string file1Path, string file2Path, string file3Path)
        {
            // TODO: Make this a for loop or something
            Files.Add(new XMLHotkeyFile(file1Path));
            Files.Add(new XMLHotkeyFile(file2Path));
            Files.Add(new XMLHotkeyFile(file3Path));
        }

        public XMLHotkeyGroup(string file1Path, string file2Path, string file3Path, string file4Path, string file5Path, string file6Path)
        {
            Files.Add(new XMLHotkeyFile(file1Path));
            Files.Add(new XMLHotkeyFile(file2Path));
            Files.Add(new XMLHotkeyFile(file3Path));
            Files.Add(new XMLHotkeyFile(file4Path));
            Files.Add(new XMLHotkeyFile(file5Path));
            Files.Add(new XMLHotkeyFile(file6Path));
        }

        public Hotkey GetHotkey(string name)
        {
            if (Files.Count == 1)
            {
                return Files[0].GetHotkey(name);
            }
            else
            {
                List<Hotkey> hotkeys = new List<Hotkey>();

                foreach (XMLHotkeyFile file in Files)
                {
                    Hotkey hotkey = file.GetHotkey(name);
                    if (hotkey != null)
                    {
                        hotkeys.Add(hotkey);
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
                return hotkeys[0];
            }
        }

        public void SetHotkey(Hotkey hotkey)
        {
            foreach (XMLHotkeyFile file in Files)
            {
                file.SetHotkey(hotkey);
            }
        }
    }
}