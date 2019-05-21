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
        // Main dictionary containing hotkeys
        public static List<Hotkey> Hotkeys { get; set; }

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

            //Hotkeys = DefaultHotkeys();

            XMLHotkeyFile doc = new XMLHotkeyFile("C:\\Users\\edwar\\Documents\\Programming\\civ5-keybinder\\test-files\\1-Builds--\\Base\\CIV5Builds.xml");

            //Display the document element.
            //doc.ShowFile();

            //Hotkeys = doc.GetHotkeys(0);

            //List<Hotkey> sortedHotkeys = Hotkeys.OrderBy(hotkey => hotkey.ID).ToList(); // Sorts hotkey list by ID

            //// Sets the source of data for ItemControl element to values of hotkeys dictionary
            //itemsControl.ItemsSource = sortedHotkeys;

            DisplayHotkeys(doc.GetHotkeys(0));
        }

        public void DisplayHotkeys(List<Hotkey> hotkeys)
        {
            // TO DO: Sort hotkeys by category

            List<Hotkey> sortedHotkeys = hotkeys.OrderBy(hotkey => hotkey.ID).ToList(); // Sorts hotkey list by ID

            // Sets the source of data for ItemControl element to values of hotkeys dictionary
            itemsControl.ItemsSource = sortedHotkeys;
        }

        private void Button_PreviewKeyDown(object sender, KeyEventArgs e)
        { 
            void UpdateHotkeysDictionary(string key)
            {
                Hotkey hotkey = (Hotkey)itemsControl.Items[0];
                hotkey.Key = e.Key.ToString().ToUpper();
            }

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

        //public Dictionary<int, Hotkey> DefaultHotkeys()
        //{
        //    Dictionary<int, Hotkey> hotkeys = new Dictionary<int, Hotkey>
        //    {
        //        // Menus
        //        {0, new Hotkey(8, 0, "Civilopedia", "F1", false, false, false)},
        //        {1, new Hotkey(8, 0, "Economic", "F2", false, false, false)},
        //        {2, new Hotkey(8, 0, "Military", "F3", false, false, false)},
        //        {3, new Hotkey(8, 0, "Diplomacy", "F4", false, false, false)},
        //        {4, new Hotkey(8, 0, "Social Policies", "F5", false, false, false)},
        //        {5, new Hotkey(8, 0, "Research", "F6", false, false, false)},
        //        {6, new Hotkey(8, 0, "Notifications", "F7", false, false, false)},
        //        {7, new Hotkey(8, 0, "Victory Progress", "F8", false, false, false)},
        //        {8, new Hotkey(8, 0, "Demographics", "F9", false, false, false)},
        //        {9, new Hotkey(8, 0, "Strategic View", "F10", false, false, false)},
        //        {10, new Hotkey(8, 0, "Advisor Counsel", "V", false, false, false)},
        //        {11, new Hotkey(8, 0, "Espionage", "E", true, false, false)},
        //        {12, new Hotkey(8, 0, "Religion", "P", true, false, false)},
                
        //        // Turn Management
        //        {13, new Hotkey(8, 0, "Next Turn", "Enter", false, false, false)}, // Alternate kotkey: Ctrl + Space
        //        {14, new Hotkey(8, 0, "Force Next Turn", "Enter", false, true, false)},
                
        //        // World View
        //        {15, new Hotkey(6, 0, "Show Hex Grid", "G", false, false, false)},
        //        {16, new Hotkey(8, 0, "Show Resources Icons", "R", true, false, false)},
        //        {17, new Hotkey(8, 0, "Show Yields", "Y", false, false, false)},
                
        //        // Zoom
        //        {18, new Hotkey(7, 0, "Zoom In", "=", false, false, false)}, // Alternate kotkey: PageUp
        //        {19, new Hotkey(7, 0, "Zoom Out", "-", false, false, false)}, // Alternate kotkey: PageDown
                
        //        // Cities
        //        {20, new Hotkey(8, 0, "Capital", "Home", false, false, false)},
        //        {21, new Hotkey(8, 0, "Next", "End", false, false, false)}, // Possibly flipped with previous
        //        {22, new Hotkey(8, 0, "Previous", "Insert", false, false, false)},
                
        //        // Saves
        //        {23, new Hotkey(8, 0, "Quick Save", "F11", false, false, false)},
        //        {24, new Hotkey(8, 0, "Quick Load", "F11", true, false, false)}, // Ctrl or shift?
        //        {25, new Hotkey(8, 0, "Save", "S", true, false, false)},
        //        {26, new Hotkey(8, 0, "Load", "L", true, false, false)},
                
        //        // Other Menus
        //        {27, new Hotkey(6, 0, "Menu", "Escape", false, false, false)},
        //        {28, new Hotkey(8, 0, "Options", "O", true, false, false)},
                
        //        // General
        //        {29, new Hotkey(5, 0, "Do Nothing", "Space", false, false, false)},
        //        {30, new Hotkey(5, 0, "Sleep/Fortify/Wake", "F", false, false, false)},
        //        {31, new Hotkey(4, 0, "Cancel Last Order", "Backspace", false, false, false)},
        //        {32, new Hotkey(8, 0, "Next Unit", "W", false, false, false)}, // Alternate hotkey: .
        //        {33, new Hotkey(8, 0, "Previous Unit", "Comma", false, false, false)},
        //        {34, new Hotkey(2, 0, "Move", "M", false, false, false)},
        //        {35, new Hotkey(5, 0, "Fortify Until Healed", "H", false, false, false)},
        //        {36, new Hotkey(2, 0, "Airlift", "A", false, true, false)},
        //        {37, new Hotkey(2, 0, "Embark", "K", false, false, false)},
        //        {38, new Hotkey(2, 0, "Disembark", "K", false, false, false)},
        //        {39, new Hotkey(4, 0, "Upgrade Unit", "U", false, false, false)},
        //        {40, new Hotkey(3, 0, "Explore", "E", false, false, false)},
        //        {41, new Hotkey(4, 0, "Disband", "Delete", false, false, false)},
                
        //        // Great Person
        //        {42, new Hotkey(1, 0, "Customs House", "H", false, false, false)},
        //        {43, new Hotkey(1, 0, "Citadel", "C", false, false, false)},
        //        {44, new Hotkey(1, 0, "Academy", "A", false, false, false)},
        //        {45, new Hotkey(1, 0, "Holy Site", "L", false, false, false)},
        //        {46, new Hotkey(1, 0, "Maunfactory", "M", false, false, false)},
        //        {47, new Hotkey(1, 0, "Landmark", "L", false, false, false)},
                
        //        // Civilian
        //        {48, new Hotkey(5, 0, "Found City", "B", false, false, false)},
        //        {49, new Hotkey(1, 0, "Chop Forest", "C", false, false, true)}, // Also jungle and marsh
        //        {50, new Hotkey(1, 0, "Chop Jungle", "C", false, false, true)},
        //        {51, new Hotkey(1, 0, "Chop Marsh", "C", false, false, true)},
        //        {52, new Hotkey(2, 0, "Route to", "R", true, true, false)},
        //        {53, new Hotkey(1, 0, "Construct Road", "R", false, false, false)},
        //        {54, new Hotkey(1, 0, "Construct Railroad", "R", false, false, true)},
        //        {55, new Hotkey(1, 0, "Remove Road", "R", true, false, true)},
        //        {56, new Hotkey(1, 0, "Trading Post", "T", false, false, false)},
        //        {57, new Hotkey(1, 0, "Camp", "H", false, false, false)},
        //        {58, new Hotkey(1, 0, "Farm", "I", false, false, false)},
        //        {59, new Hotkey(1, 0, "Mine", "N", false, false, false)},
        //        {60, new Hotkey(1, 0, "Plantation", "P", false, false, false)},
        //        {61, new Hotkey(1, 0, "Quarry", "Q", false, false, false)},
        //        {62, new Hotkey(1, 0, "Pasture", "P", false, false, false)},
        //        {63, new Hotkey(1, 0, "Fort", "F", true, false, false)},
        //        {64, new Hotkey(1, 0, "Oil Well", "O", false, false, false)},
        //        {65, new Hotkey(1, 0, "Offshore Oil Platform", "O", false, false, false)},
        //        {66, new Hotkey(1, 0, "Fishing Boats", "F", false, false, false)},
        //        {67, new Hotkey(1, 0, "Lumber Mill", "L", false, false, false)},
        //        {68, new Hotkey(1, 0, "Archaeological Dig", "A", true, false, false)},
        //        {69, new Hotkey(8, 0, "Next Unassigned Worker", "/", false, false, false)},
        //        {70, new Hotkey(1, 0, "Repair Improvement", "P", true, false, false)},
        //        {71, new Hotkey(1, 0, "Scrub Fallout", "S", false, false, false)},
        //        {72, new Hotkey(3, 0, "Automated Build", "A", false, false, false)},
                
        //        // Aircraft
        //        {73, new Hotkey(5, 0, "Rebase", "R", false, false, true)}, // Ctrl?
        //        {74, new Hotkey(2, 0, "Air Strike", "S", false, false, false)},
        //        {75, new Hotkey(5, 0, "Air Sweep", "S", false, false, true)},
        //        {76, new Hotkey(5, 0, "Intercept", "I", false, false, false)},
        //        {77, new Hotkey(2, 0, "Nuke Mode", "N", false, false, false)},
                
        //        // Military
        //        {78, new Hotkey(2, 0, "Attack", "A", true, false, false)},
        //        {79, new Hotkey(5, 0, "Alert", "A", false, false, false)},
        //        {80, new Hotkey(5, 0, "Set Up Ranged", "S", false, false, false)},
        //        {81, new Hotkey(5, 0, "Ranged Attack", "B", false, false, false)},
        //        {82, new Hotkey(5, 0, "Pillage", "P", false, true, false)},
        //        {83, new Hotkey(2, 0, "Paradrop", "P", false, false, false)},
        //    };

        //    return hotkeys;
        //}

        //public List<Hotkey> DefaultHotkeys()
        //{
        //    List<Hotkey> hotkeys = new List<Hotkey>
        //    {
        //        // Menus
        //        new Hotkey(0, 8, 0, "Civilopedia", "F1", false, false, false),
        //        new Hotkey(1, 8, 0, "Economic", "F2", false, false, false),
        //        new Hotkey(2, 8, 0, "Military", "F3", false, false, false),
        //        new Hotkey(3, 8, 0, "Diplomacy", "F4", false, false, false),
        //        new Hotkey(4, 8, 0, "Social Policies", "F5", false, false, false),
        //        new Hotkey(5, 8, 0, "Research", "F6", false, false, false),
        //        new Hotkey(6, 8, 0, "Notifications", "F7", false, false, false),
        //        new Hotkey(7, 8, 0, "Victory Progress", "F8", false, false, false),
        //        new Hotkey(8, 8, 0, "Demographics", "F9", false, false, false),
        //        new Hotkey(9, 8, 0, "Strategic View", "F10", false, false, false),
        //        new Hotkey(10, 8, 0, "Advisor Counsel", "V", false, false, false),
        //        new Hotkey(11, 8, 0, "Espionage", "E", true, false, false),
        //        new Hotkey(12, 8, 0, "Religion", "P", true, false, false),

        //        // Turn Management
        //        new Hotkey(13, 8, 0, "Next Turn", "Enter", false, false, false), // Alternate kotkey: Ctrl + Space
        //        new Hotkey(14, 8, 0, "Force Next Turn", "Enter", false, true, false),

        //        // World View
        //        new Hotkey(15, 6, 0, "Show Hex Grid", "G", false, false, false),
        //        new Hotkey(16, 8, 0, "Show Resources Icons", "R", true, false, false),
        //        new Hotkey(17, 8, 0, "Show Yields", "Y", false, false, false),

        //        // Zoom
        //        new Hotkey(18, 7, 0, "Zoom In", "=", false, false, false), // Alternate kotkey: PageUp
        //        new Hotkey(19, 7, 0, "Zoom Out", "-", false, false, false), // Alternate kotkey: PageDown

        //        // Cities
        //        new Hotkey(20, 8, 0, "Capital", "Home", false, false, false),
        //        new Hotkey(21, 8, 0, "Next", "End", false, false, false), // Possibly flipped with previous
        //        new Hotkey(22, 8, 0, "Previous", "Insert", false, false, false),

        //        // Saves
        //        new Hotkey(23, 8, 0, "Quick Save", "F11", false, false, false),
        //        new Hotkey(24, 8, 0, "Quick Load", "F11", true, false, false), // Ctrl or shift?
        //        new Hotkey(25, 8, 0, "Save", "S", true, false, false),
        //        new Hotkey(26, 8, 0, "Load", "L", true, false, false),

        //        // Game Menus
        //        new Hotkey(27, 6, 0, "Menu", "Escape", false, false, false),
        //        new Hotkey(28, 8, 0, "Options", "O", true, false, false),

        //        // General
        //        new Hotkey(29, 5, 0, "Do Nothing", "Space", false, false, false),
        //        new Hotkey(30, 5, 0, "Sleep/Fortify/Wake", "F", false, false, false),
        //        new Hotkey(31, 4, 0, "Cancel Last Order", "Backspace", false, false, false),
        //        new Hotkey(32, 8, 0, "Next Unit", "W", false, false, false), // Alternate hotkey: .
        //        new Hotkey(33, 8, 0, "Previous Unit", "Comma", false, false, false),
        //        new Hotkey(34, 2, 0, "Move", "M", false, false, false),
        //        new Hotkey(35, 5, 0, "Fortify Until Healed", "H", false, false, false),
        //        new Hotkey(36, 2, 0, "Airlift", "A", false, true, false),
        //        new Hotkey(37, 2, 0, "Embark", "K", false, false, false),
        //        new Hotkey(38, 2, 0, "Disembark", "K", false, false, false),
        //        new Hotkey(39, 4, 0, "Upgrade Unit", "U", false, false, false),
        //        new Hotkey(40, 3, 0, "Explore", "E", false, false, false),
        //        new Hotkey(41, 4, 0, "Disband", "Delete", false, false, false),

        //        // Great Person
        //        new Hotkey(42, 1, 0, "Customs House", "H", false, false, false),
        //        new Hotkey(43, 1, 0, "Citadel", "C", false, false, false),
        //        new Hotkey(44, 1, 0, "Academy", "A", false, false, false),
        //        new Hotkey(45, 1, 0, "Holy Site", "L", false, false, false),
        //        new Hotkey(46, 1, 0, "Maunfactory", "M", false, false, false),
        //        new Hotkey(47, 1, 0, "Landmark", "L", false, false, false),

        //        // Civilian
        //        new Hotkey(48, 5, 0, "Found City", "B", false, false, false),
        //        new Hotkey(49, 1, 0, "Chop Forest", "C", false, false, true),
        //        new Hotkey(50, 1, 0, "Chop Jungle", "C", false, false, true),
        //        new Hotkey(51, 1, 0, "Chop Marsh", "C", false, false, true),
        //        new Hotkey(52, 2, 0, "Route to", "R", true, true, false),
        //        new Hotkey(53, 1, 0, "Construct Road", "R", false, false, false),
        //        new Hotkey(54, 1, 0, "Construct Railroad", "R", false, false, true),
        //        new Hotkey(55, 1, 0, "Remove Road", "R", true, false, true),
        //        new Hotkey(56, 1, 0, "Trading Post", "T", false, false, false),
        //        new Hotkey(57, 1, 0, "Camp", "H", false, false, false),
        //        new Hotkey(58, 1, 0, "Farm", "I", false, false, false),
        //        new Hotkey(59, 1, 0, "Mine", "N", false, false, false),
        //        new Hotkey(60, 1, 0, "Plantation", "P", false, false, false),
        //        new Hotkey(61, 1, 0, "Quarry", "Q", false, false, false),
        //        new Hotkey(62, 1, 0, "Pasture", "P", false, false, false),
        //        new Hotkey(63, 1, 0, "Fort", "F", true, false, false),
        //        new Hotkey(64, 1, 0, "Oil Well", "O", false, false, false),
        //        new Hotkey(65, 1, 0, "Offshore Oil Platform", "O", false, false, false),
        //        new Hotkey(66, 1, 0, "Fishing Boats", "F", false, false, false),
        //        new Hotkey(67, 1, 0, "Lumber Mill", "L", false, false, false),
        //        new Hotkey(68, 1, 0, "Archaeological Dig", "A", true, false, false),
        //        // 69 polder, 70 kabash, 71 brazilwood, 72 feitoria, 73 chateau
        //        new Hotkey(74, 8, 0, "Next Unassigned Worker", "/", false, false, false),
        //        new Hotkey(75, 1, 0, "Repair Improvement", "P", true, false, false),
        //        new Hotkey(76, 1, 0, "Scrub Fallout", "S", false, false, false),
        //        new Hotkey(77, 3, 0, "Automated Build", "A", false, false, false),

        //        // Aircraft
        //        new Hotkey(78, 5, 0, "Rebase", "R", false, false, true), // Ctrl?
        //        new Hotkey(79, 2, 0, "Air Strike", "S", false, false, false),
        //        new Hotkey(80, 5, 0, "Air Sweep", "S", false, false, true),
        //        new Hotkey(81, 5, 0, "Intercept", "I", false, false, false),
        //        new Hotkey(82, 2, 0, "Nuke Mode", "N", false, false, false),

        //        // Military
        //        new Hotkey(83, 2, 0, "Attack", "A", true, false, false),
        //        new Hotkey(84, 5, 0, "Alert", "A", false, false, false),
        //        new Hotkey(85, 5, 0, "Set Up Ranged", "S", false, false, false),
        //        new Hotkey(86, 5, 0, "Ranged Attack", "B", false, false, false),
        //        new Hotkey(87, 5, 0, "Pillage", "P", false, true, false),
        //        new Hotkey(88, 2, 0, "Paradrop", "P", false, false, false),

        //        // 89 city ranged attack = B
        //        // 90 cancel all orders = Backspace + Shift
        //        // 91 stop automation
        //        // 92 sleep
        //        // 93 fortify
        //        // 94 garrison
        //    };

        //    return hotkeys;
        //}

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            //Hotkey hotkey = (Hotkey)itemsControl.Items[0];
            //MessageBox.Show(hotkey.Key);

            HotkeyDataFile hotkeyDataFile = new HotkeyDataFile("HotkeyData.xml");

            MessageBox.Show(hotkeyDataFile.GetStringAttribute("Function", "BUILD_ROAD"));

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            HotkeyDataFile hotkeyDataFile = new HotkeyDataFile("HotkeyData.xml");

            List<Hotkey> list = new List<Hotkey>();

            DisplayHotkeys(hotkeyDataFile.GetDefaultHotkeys());
        }
    }
}
