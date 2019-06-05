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

using System.Xml;

namespace civ5_keybinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Main list containing hotkeys
        public List<Hotkey> Hotkeys { get; set; } = new List<Hotkey>();

        // List containing changes to a copy of Hotkeys
        public List<Hotkey> NewHotkeys { get; set; } = new List<Hotkey>();

        public static Dictionary<string, string> keyDict { get; } = new Dictionary<string, string>
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

            //XMLHotkeyFile doc = new XMLHotkeyFile("C:\\Users\\edwar\\Documents\\Programming\\civ5-keybinder\\test-files\\1-Builds--\\Base\\CIV5Builds.xml");
            //XMLHotkeyFile doc1 = new XMLHotkeyFile("C:\\Users\\edwar\\Documents\\Programming\\civ5-keybinder\\test-files\\2-InterfaceModes\\Base\\CIV5InterfaceModes.xml");

            //Hotkeys.AddRange(doc.GetHotkeys());
            //Hotkeys.AddRange(doc1.GetHotkeys());

            //Hotkeys = SortHotkeys(Hotkeys);

            //NewHotkeys = Hotkeys;

            //itemsControl.ItemsSource = Hotkeys;

            XMLHotkeyGroup group1 = new XMLHotkeyGroup("C:\\Users\\edwar\\Documents\\Programming\\civ5-keybinder\\test-files\\1-Builds--\\Base\\CIV5Builds.xml");
            XMLHotkeyGroup group2 = new XMLHotkeyGroup("C:\\Users\\edwar\\Documents\\Programming\\civ5-keybinder\\test-files\\2-InterfaceModes\\Base\\CIV5InterfaceModes.xml");
            XMLHotkeyGroup group8 = new XMLHotkeyGroup("C:\\Users\\edwar\\Documents\\Programming\\civ5-keybinder\\test-files\\8-Controls-\\Base\\CIV5Controls.xml",
                "C:\\Users\\edwar\\Documents\\Programming\\civ5-keybinder\\test-files\\8-Controls-\\Expansion\\CIV5Controls.xml",
                "C:\\Users\\edwar\\Documents\\Programming\\civ5-keybinder\\test-files\\8-Controls-\\Expansion2\\CIV5Controls.xml");

            //Hotkeys.AddRange(group1.GetHotkeys());
            //Hotkeys.AddRange(group2.GetHotkeys());
            Hotkeys.AddRange(group8.GetHotkeys());

            Hotkeys = SortHotkeys(Hotkeys);

            itemsControl.ItemsSource = Hotkeys;
        }

        public List<Hotkey> SortHotkeys(List<Hotkey> hotkeys)
        {
            // TODO: Sort hotkeys by category

            List<Hotkey> sortedHotkeys = hotkeys.OrderBy(hotkey => hotkey.ID).ToList(); // Sorts hotkey list by ID

            return sortedHotkeys;
        }

        public void UpdateNewHotkeys(string hotkey, string defKey, bool defCtrl, bool defShift, bool defAlt)
        {
            NewHotkeys.Find(x => x.Name == hotkey).DefineBinding(defKey, defCtrl, defShift, defAlt);
        }

        private void Button_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //void UpdateNewHotkeysDictionary(string key)
            //{
            //    Hotkey hotkey = (Hotkey)itemsControl.Items[0];
            //    hotkey.Key = e.Key.ToString().ToUpper();
            //}

            Button button = sender as Button;

            if (keyDict.ContainsKey(e.Key.ToString()))
            {
                button.Content = keyDict[e.Key.ToString()];
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
                    //Hotkey hotkey = (Hotkey)itemsControl.Items[0];
                    //hotkey.Key = e.Key.ToString().ToUpper();
                    //UpdateNewHotkeysDictionary(button.Content.ToString());
                    e.Handled = true;
                }
            }
        }

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

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            Hotkey hotkey = (Hotkey)itemsControl.Items[FindHotkeyIndex(Hotkeys, "BUILD_ROAD")];

            MessageBox.Show(hotkey.Key);


            //itemsControl.TargetUpdated
            //MessageBox.Show(hotkey.Shift.ToString());

            //HotkeyDataFile hotkeyDataFile = new HotkeyDataFile("HotkeyData.xml");

            //MessageBox.Show(hotkeyDataFile.GetStringAttribute("Function", "BUILD_ROAD"));

            //MessageBox.Show(hotkeys[0].Key);

            //https://stackoverflow.com/questions/2785044/twoway-binding-with-itemscontrol

            //foreach(Hotkey hotkey in itemsControl.Items)
            //{

            //}

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            HotkeyDataFile hotkeyDataFile = new HotkeyDataFile("HotkeyData.xml");

            List<Hotkey> list = new List<Hotkey>();

            itemsControl.ItemsSource = hotkeyDataFile.GetDefaultHotkeys();
        }

        // Returns index of hotkey in list based on name
        public int FindHotkeyIndex(List<Hotkey> list, string hotkeyName)
        {
            foreach(Hotkey hotkey in list)
            {
                if(hotkey.Name == hotkeyName)
                {
                    return list.IndexOf(hotkey);
                }
            }

            throw new ArgumentException();
        }
    }
}
