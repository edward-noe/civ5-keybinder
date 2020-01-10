using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace civ5_keybinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Main list containing hotkeys
        public List<Hotkey> Hotkeys { get; set; } = new List<Hotkey>();

        // List of modified hotkeys
        public Dictionary<string, bool> ModifiedHotkeys { get; set; }

        // List containing HotkeyGroups
        Dictionary<int, XMLHotkeyGroup> Groups = new Dictionary<int, XMLHotkeyGroup>();
        
        public static readonly HotkeyDataFile hotkeyDataFile = new HotkeyDataFile("Resources\\HotkeyData.xml");

        public static Dictionary<string, string> KeyDict { get; } = new Dictionary<string, string>
        {
            {"Oem1", ";"},
            {"Oem3", "`"},
            {"Oem5", "\\"},
            {"OemMinus", "-"},
            {"OemPlus", "="},
            {"OemOpenBrackets", "["},
            {"Oem6", "]"},
            {"OemQuotes", "'"},
            {"OemComma", ","},
            {"OemPeriod", "."},
            {"OemQuestion", "/"},
            {"D1", "1"},
            {"D2", "2"},
            {"D3", "3"},
            {"D4", "4"},
            {"D5", "5"},
            {"D6", "6"},
            {"D7", "7"},
            {"D8", "8"},
            {"D9", "9"},
            {"D0", "0"},
        };

        public MainWindow()
        {
            InitializeComponent();

            InitializeDocuments();

            InitializeDictionary();

            GetHotkeys();
        }
        public void InitializeDocuments()
        {
            // Imports documents as XMLHotkeyGroups
            Groups.Add(1, new XMLHotkeyGroup(
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\1-Builds--\\Base\\CIV5Builds.xml",
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\1-Builds--\\Expansion\\CIV5Builds_Expansion.xml",
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\1-Builds--\\Expansion2\\CIV5Builds_Expansion2.xml",
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\1-Builds--\\Expansion\\CIV5Builds.xml",
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\1-Builds--\\Expansion2\\CIV5Builds.xml",
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\1-Builds--\\Expansion2\\CIV5Builds_Inherited_Expansion2.xml"));
            Groups.Add(2, new XMLHotkeyGroup(
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\2-InterfaceModes\\Base\\CIV5InterfaceModes.xml"));
            Groups.Add(3, new XMLHotkeyGroup(
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\3-Automates\\CIV5Automates.xml"));
            Groups.Add(4, new XMLHotkeyGroup(
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\4-Commands\\CIV5Commands.xml"));
            Groups.Add(5, new XMLHotkeyGroup(
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\5-Missions-\\Base\\CIV5Missions.xml",
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\5-Missions-\\Expansion\\CIV5Missions.xml",
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\5-Missions-\\Expansion2\\CIV5Missions.xml"));
            Groups.Add(8, new XMLHotkeyGroup(
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\8-Controls-\\Base\\CIV5Controls.xml",
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\8-Controls-\\Expansion\\CIV5Controls.xml",
                "C:\\Users\\Edward Noe\\Documents\\Computing\\civ5-keybinder\\test-files\\8-Controls-\\Expansion2\\CIV5Controls.xml"));

            // TODO: Add support for LUA hotkeys (Groups 6 and 7) 
        }

        public void InitializeDictionary()
        {
            ModifiedHotkeys = new Dictionary<string, bool>();

            List<string> list = hotkeyDataFile.GetHotkeyNames();
            foreach (string name in list)
            {
                ModifiedHotkeys.Add(name, false);
            }
        }

        // Fills in Hotkeys list when application is started
        public void GetHotkeys()
        {
            List<string> hotkeyNames = hotkeyDataFile.GetHotkeyNames();

            foreach (string name in hotkeyNames)
            {
                int groupNumber = int.Parse(hotkeyDataFile.GetStringAttribute("File", name).ToCharArray()[0].ToString());

                if (Groups.TryGetValue(groupNumber, out XMLHotkeyGroup group))
                {
                    Hotkeys.Add(group.GetHotkey(name));
                }
            }
            // Changes source of itemsControl to allow for display
            itemsControl.ItemsSource = SortHotkeys(Hotkeys);
        }

        // Updates hotkeys as they have been modified
        public void UpdateHotkeys()
        {
            List<string> hotkeyNames = new List<string>();

            foreach (KeyValuePair<string, bool> pair in ModifiedHotkeys.ToList())
            {
                if (pair.Value == true)
                {
                    hotkeyNames.Add(pair.Key);
                    ModifiedHotkeys[pair.Key] = false;
                }
            }

            foreach (string name in hotkeyNames)
            {
                int groupNumber = int.Parse(hotkeyDataFile.GetStringAttribute("File", name).ToCharArray()[0].ToString());

                Hotkey hotkey = Hotkeys.Find(item => item.Name == name);

                if (Groups.TryGetValue(groupNumber, out XMLHotkeyGroup group))
                {
                    hotkey = group.GetHotkey(name);
                }
            }
        }

        public List<Hotkey> SortHotkeys(List<Hotkey> hotkeys)
        {
            // TODO: Sort hotkeys by category

            // Sorts hotkey list by ID
            List<Hotkey> sortedHotkeys = hotkeys.OrderBy(hotkey => hotkey.ID).ToList(); 

            return sortedHotkeys;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < itemsControl.Items.Count; i++)
            {
                Hotkey hotkey = (Hotkey)itemsControl.Items[i];
                if (ModifiedHotkeys.TryGetValue(hotkey.Name, out bool output))
                {
                    if (output)
                    {
                        // Converts file string to char and then int
                        char groupChar = hotkeyDataFile.GetStringAttribute("File", hotkey.Name)[0];
                        int group = int.Parse(groupChar.ToString());

                        Groups[group].SetHotkey(hotkey);

                        // Moved to UpdateHotkeys to be safe
                        //ModifiedHotkeys[hotkey.Name] = false;
                    }
                }
            }
            //Refreshes list of hotkeys
            UpdateHotkeys();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            //HotkeyDataFile hotkeyDataFile = new HotkeyDataFile("Resources\\HotkeyData.xml");

            itemsControl.ItemsSource = SortHotkeys(hotkeyDataFile.GetDefaultHotkeys());
            //MessageBox.Show((Hotkey)itemsControl.Items[FindHotkeyIndex(OldHotkeys, "CONTROL_CIVILOPEDIA")]);

            // TODO: Change value of hotkey bindings
        }

        private void Button_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Button button = sender as Button;

            if (KeyDict.ContainsKey(e.Key.ToString()))
            {
                button.Content = KeyDict[e.Key.ToString()];
            }
            else if (e.Key == Key.Return)
            {
                button.Content = "Enter";
                e.Handled = true;
            }
            else if (e.Key == Key.System)
            {
                button.Content = "F10";
                e.Handled = true;
            }
            else if (e.Key == Key.Next)
            {
                button.Content = "PageDown";
                // Prevents page from scrolling when pressed
                e.Handled = true;
            }
            else if (e.Key == Key.PageDown)
            {
                button.Content = "PageUp";
                e.Handled = true;
            }
            else
            {
                if (e.Key.ToString().Length > 1)
                {
                    button.Content = e.Key.ToString();
                    e.Handled = true;
                }
                else
                {
                    button.Content = e.Key.ToString().ToUpper();
                    e.Handled = true;
                }
            }

            // Updates ModifiedHotkeys dictionary using DataContext
            ModifiedHotkeys[((Hotkey)button.DataContext).Name] = true;
        }

        // Changes appearance of button
        private void Button_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Button button = sender as Button;
            button.Background = (Brush)new BrushConverter().ConvertFrom("#FFBEE6FD");
            button.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF3C7FB1");
        }

        private void Button_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Button button = sender as Button;
            button.Background = (Brush)new BrushConverter().ConvertFrom("#FFDDDDDD");
            button.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF707070");
        }
    }
}