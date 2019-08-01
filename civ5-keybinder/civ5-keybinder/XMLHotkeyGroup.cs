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
        public XMLHotkeyGroup(string file1Path, string file2Path = "", string file3Path = "", string file4Path = "", string file5Path = "", string file6Path = "")
        {
            Files.Add(new XMLHotkeyFile(file1Path));
            if (file2Path != "") { Files.Add(new XMLHotkeyFile(file2Path)); }
            if (file3Path != "") { Files.Add(new XMLHotkeyFile(file3Path)); }
            if (file4Path != "") { Files.Add(new XMLHotkeyFile(file4Path)); }
            if (file5Path != "") { Files.Add(new XMLHotkeyFile(file5Path)); }
            if (file6Path != "") { Files.Add(new XMLHotkeyFile(file6Path)); }
        }

        public List<Hotkey> GetHotkeys()
        {
            List<Hotkey> hotkeys = new List<Hotkey>();

            foreach (XMLHotkeyFile file in Files)
            {
                foreach (Hotkey hotkey in file.GetHotkeys())
                {
                    // Checks to ensure there is not already a hotkey with the same Name attribute
                    if (hotkeys.Any(item => item.Name == hotkey.Name) == false)
                    {
                        hotkeys.Add(hotkey);
                    }
                    // If the Key attribute in the new hotkey does not match that in the hotkeys list, error message is created
                    else if (hotkey.Key != hotkeys.Single(item => item.Name == hotkey.Name).Key)
                    {
                        hotkeys.Single(item => item.Name == hotkey.Name).Key = "ERROR: MULTIPLE KEYS";
                    }
                }
            }

            return hotkeys;
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