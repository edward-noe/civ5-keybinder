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
        public List<Hotkey> OldHotkeys { get; set; } = new List<Hotkey>();

        // List containing changes to a copy of Hotkeys
        public List<Hotkey> NewHotkeys { get; set; } = new List<Hotkey>();

        // List containing HotkeyGroups
        Dictionary<int, XMLHotkeyGroup> Groups = new Dictionary<int, XMLHotkeyGroup>();
        
        readonly HotkeyDataFile hotkeyDataFile = new HotkeyDataFile("Resources\\HotkeyData.xml");

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

            GetHotkeys();
        }

        public void GetHotkeys()
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

            // TODO: Add support for LUA hotkeys

            // Adds groups to main Hotkey list
            foreach (var item in Groups)
            {
                OldHotkeys.AddRange(item.Value.GetHotkeys());
                // NewHotkeys gets values directly from the group
                NewHotkeys.AddRange(item.Value.GetHotkeys());
            }

            OldHotkeys = SortHotkeys(OldHotkeys);
            NewHotkeys = SortHotkeys(NewHotkeys);

            //NewHotkeys = OldHotkeys.GetRange(0, OldHotkeys.Count);


            // Adds Hotkeys list to itemsControl for display
            itemsControl.ItemsSource = NewHotkeys;
        }

        public List<Hotkey> SortHotkeys(List<Hotkey> hotkeys)
        {
            // TODO: Sort hotkeys by category

            List<Hotkey> sortedHotkeys = hotkeys.OrderBy(hotkey => hotkey.ID).ToList(); // Sorts hotkey list by ID

            return sortedHotkeys;
        }

        //public void UpdateNewHotkeys(string hotkey, string defKey, bool defCtrl, bool defShift, bool defAlt)
        //{
        //    NewHotkeys.Find(x => x.Name == hotkey).DefineBinding(defKey, defCtrl, defShift, defAlt);
        //}

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

        public void UpdateNewHotkeys()
        {
            //NewHotkeys.Find(x => x.Name == hotkey).DefineBinding(defKey, defCtrl, defShift, defAlt);
            foreach (Hotkey hotkey in itemsControl.Items)
            {

            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            // TEST CODE: Works
            //Hotkey hotkey = (Hotkey)itemsControl.Items[FindHotkeyIndex(OldHotkeys, "CONTROL_CIVILOPEDIA")];

            //MessageBox.Show(hotkey.Key);

            for (int i = 0; i < itemsControl.Items.Count; i++)
            {
                Hotkey newHotkey = (Hotkey)itemsControl.Items[i];
                Hotkey oldHotkey = OldHotkeys.Find(item => item.Name == newHotkey.Name);

                if (newHotkey.Key != oldHotkey.Key || newHotkey.Ctrl != oldHotkey.Ctrl || newHotkey.Shift != oldHotkey.Shift || newHotkey.Alt != oldHotkey.Alt)
                {
                    // Converts file string to char and then int
                    char groupChar = newHotkey.File[0];
                    int group = int.Parse(groupChar.ToString());

                    Groups[group].SetHotkey(newHotkey);
                }
            }

            //foreach (Object ob in itemsControl.Items)
            //{
            //    Hotkey hotkey = (Hotkey)ob;
            //    if (hotkey.Key != OldHotkeys.Find(item => item.Name == hotkey.Name).Key)
            //    {
            //        MessageBox.Show("hello world");
            //    }
            //}

            //foreach (Hotkey hotkey in itemsControl.Items)
            //{
            //    // Begins process of changing hotkey binding if hotkey in Hotkeys differs from hotkeys in itemsControl
            //    if (hotkey != OldHotkeys.Find(item => item.Name == hotkey.Name))
            //    {
            //        MessageBox.Show("hello world");

            //        int group = hotkeyDataFile.GetStringAttribute("File", hotkey.Name)[0];

            //        Groups[group].SetHotkey(hotkey);
            //    }
            //}
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            //HotkeyDataFile hotkeyDataFile = new HotkeyDataFile("Resources\\HotkeyData.xml");

            itemsControl.ItemsSource = SortHotkeys(hotkeyDataFile.GetDefaultHotkeys());
            //MessageBox.Show((Hotkey)itemsControl.Items[FindHotkeyIndex(OldHotkeys, "CONTROL_CIVILOPEDIA")]);

            // TODO: Change value of hotkey bindings
        }

        // Returns index of hotkey in list based on name
        public int FindHotkeyIndex(List<Hotkey> list, string hotkeyName)
        {
            foreach (Hotkey hotkey in list)
            {
                if (hotkey.Name == hotkeyName)
                {
                    return list.IndexOf(hotkey);
                }
            }
            throw new ArgumentException(); // Required to complete code path
        }
    }
}