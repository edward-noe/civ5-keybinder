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
        public MainWindow()
        {
            InitializeComponent();

            InitializeGrid(DefaultHotkeys());
        }
        
        public void InitializeGrid(List<Hotkey> hotkeys)
        {
            // I don't know why adding this additional row is neccesary, but it is
            main_grid.RowDefinitions.Add(new RowDefinition());

            void AddText(string text, int row, int col)
            {
                TextBlock block = new TextBlock();
                block.Text = text;
                Grid.SetRow(block, row);
                Grid.SetColumn(block, col);
                main_grid.Children.Add(block);
            }

            void AddButton(string text, int row, int col)
            {
                Button button = new Button();
                button.Width = 150;
                button.Content = text;
                button.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(button_GotKeyboardFocus);
                button.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(button_LostKeyboardFocus);
                button.KeyDown += new KeyEventHandler(button_KeyDown);
                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);
                main_grid.Children.Add(button);
            }

            void AddCheckBox(bool state, int row, int col)
            {
                CheckBox box = new CheckBox();
                box.HorizontalAlignment = HorizontalAlignment.Center;
                box.IsChecked = state;
                Grid.SetRow(box, row);
                Grid.SetColumn(box, col);
                main_grid.Children.Add(box);
            }

            for (int hotkey_num = 0; hotkey_num < hotkeys.Count; hotkey_num++)
            {
                // Adds a row to the main grid for each hotkey
                main_grid.RowDefinitions.Add(new RowDefinition());

                for (int attribute_num = 0; attribute_num < 6; attribute_num++)
                {
                    switch (attribute_num)
                    {
                        case 0:
                            AddText(hotkeys[hotkey_num].DLC, hotkey_num + 1, 0);
                            break;
                        case 1:
                            AddText(hotkeys[hotkey_num].Function, hotkey_num + 1, 1);
                            break;
                        case 2:
                            AddButton(hotkeys[hotkey_num].Key, hotkey_num + 1, 2);
                            break;
                        case 3:
                            AddCheckBox(hotkeys[hotkey_num].Ctrl, hotkey_num + 1, 3);
                            break;
                        case 4:
                            AddCheckBox(hotkeys[hotkey_num].Shift, hotkey_num + 1, 4);
                            break;
                        case 5:
                            AddCheckBox(hotkeys[hotkey_num].Alt, hotkey_num + 1, 5);
                            break;
                    }
                }                
            }
        }

        private void button_KeyDown(object sender, KeyEventArgs e)
        {
            Button button = sender as Button;
            object initialContent = button.Content;
            button.Content = e.Key.ToString().ToUpper();
        }

        private void button_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Button button = sender as Button;
            button.Background = (Brush)new BrushConverter().ConvertFrom("#FFBEE6FD");
            button.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF3C7FB1");
        }

        private void button_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Button button = sender as Button;
            button.Background = (Brush)new BrushConverter().ConvertFrom("#FFDDDDDD");
            button.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF707070");
        }

        public List<Hotkey> DefaultHotkeys()
        {
            List<Hotkey> hotkeys = new List<Hotkey>
            {
                // Menus
                new Hotkey(8, 0, "Civilopedia", "F1", false, false, false),
                new Hotkey(8, 0, "Economic", "F2", false, false, false),
                new Hotkey(8, 0, "Military", "F3", false, false, false),
                new Hotkey(8, 0, "Diplomacy", "F4", false, false, false),
                new Hotkey(8, 0, "Social Policies", "F5", false, false, false),
                new Hotkey(8, 0, "Research", "F6", false, false, false),
                new Hotkey(8, 0, "Notifications", "F7", false, false, false),
                new Hotkey(8, 0, "Victory Progress", "F8", false, false, false),
                new Hotkey(8, 0, "Demographics", "F9", false, false, false),
                new Hotkey(8, 0, "Strategic View", "F10", false, false, false),
                new Hotkey(8, 0, "Advisor Counsel", "V", false, false, false),
                new Hotkey(8, 0, "Espionage", "E", true, false, false),
                new Hotkey(8, 0, "Religion", "P", true, false, false),

                // Turn Management
                new Hotkey(8, 0, "Next Turn", "Enter", false, false, false), // Alternate kotkey: Ctrl + Space
                new Hotkey(8, 0, "Force Next Turn", "Enter", false, true, false),

                // World View
                new Hotkey(6, 0, "Show Hex Grid", "G", false, false, false),
                new Hotkey(8, 0, "Show Resources Icons", "R", true, false, false),
                new Hotkey(8, 0, "Show Yields", "Y", false, false, false),

                // Zoom
                new Hotkey(7, 0, "Zoom In", "=", false, false, false), // Alternate kotkey: PageUp
                new Hotkey(7, 0, "Zoom Out", "-", false, false, false), // Alternate kotkey: PageDown

                // Cities
                new Hotkey(8, 0, "Capital", "Home", false, false, false),
                new Hotkey(8, 0, "Next", "End", false, false, false), // Possibly flipped with previous
                new Hotkey(8, 0, "Previous", "Insert", false, false, false),

                // Saves
                new Hotkey(8, 0, "Quick Save", "F11", false, false, false),
                new Hotkey(8, 0, "Quick Load", "F11", true, false, false), // Ctrl or shift?
                new Hotkey(8, 0, "Save", "S", true, false, false),
                new Hotkey(8, 0, "Load", "L", true, false, false),

                // Other Menus
                new Hotkey(6, 0, "Menu", "Escape", false, false, false),
                new Hotkey(8, 0, "Options", "O", true, false, false),

                // General
                new Hotkey(5, 0, "Do Nothing", "Space", false, false, false),
                new Hotkey(5, 0, "Sleep/Fortify/Wake", "F", false, false, false),
                new Hotkey(4, 0, "Cancel Last Order", "Backspace", false, false, false),
                new Hotkey(8, 0, "Next Unit", "W", false, false, false), // Alternate hotkey: .
                new Hotkey(8, 0, "Previous Unit", "Comma", false, false, false),
                new Hotkey(2, 0, "Move", "M", false, false, false),
                new Hotkey(5, 0, "Fortify Until Healed", "H", false, false, false),
                new Hotkey(2, 0, "Airlift", "A", false, true, false),
                new Hotkey(2, 0, "Embark", "K", false, false, false), // Embark and disembark are two seperate hotkeys
                new Hotkey(4, 0, "Upgrade Unit", "U", false, false, false),
                new Hotkey(3, 0, "Explore", "E", false, false, false),
                new Hotkey(4, 0, "Disband", "Delete", false, false, false),

                // Great Person
                new Hotkey(1, 0, "Customs House", "H", false, false, false),
                new Hotkey(1, 0, "Citadel", "C", false, false, false),
                new Hotkey(1, 0, "Academy", "A", false, false, false),
                new Hotkey(1, 0, "Holy Site", "L", false, false, false),
                new Hotkey(1, 0, "Maunfactory", "M", false, false, false),
                //Landmark (1, "L")

                // Civilian
                new Hotkey(5, 0, "Found City", "B", false, false, false),
                new Hotkey(1, 0, "Chop Forest", "C", false, false, true), // Also jungle
                new Hotkey(2, 0, "Route to", "R", true, true, false),
                new Hotkey(1, 0, "Construct Road", "R", false, false, false),
                new Hotkey(1, 0, "Construct Railroad", "R", false, false, true),
                new Hotkey(1, 0, "Remove Road", "R", true, false, true),
                new Hotkey(1, 0, "Trading Post", "T", false, false, false),
                new Hotkey(1, 0, "Camp", "H", false, false, false),
                new Hotkey(1, 0, "Farm", "I", false, false, false),
                new Hotkey(1, 0, "Mine", "N", false, false, false),
                new Hotkey(1, 0, "Plantation", "P", false, false, false),
                new Hotkey(1, 0, "Quarry", "Q", false, false, false),
                new Hotkey(1, 0, "Pasture", "P", false, false, false),
                new Hotkey(1, 0, "Fort", "F", true, false, false),
                new Hotkey(1, 0, "Oil Well", "O", false, false, false),
                new Hotkey(1, 0, "Fishing Boats", "F", false, false, false),
                new Hotkey(1, 0, "Lumber Mill", "L", false, false, false),
                new Hotkey(1, 0, "Archaeological Dig", "A", true, false, false),
                new Hotkey(8, 0, "Next Unassigned Worker", "/", false, false, false),
                new Hotkey(1, 0, "Repair Improvement", "P", true, false, false),
                new Hotkey(1, 0, "Scrub Fallout", "S", false, false, false),
                new Hotkey(3, 0, "Automated Build", "A", false, false, false),

                // Aircraft
                new Hotkey(5, 0, "Rebase", "R", false, false, true), // Ctrl?
                new Hotkey(2, 0, "Air Strike", "S", false, false, false),
                new Hotkey(5, 0, "Air Sweep", "S", false, false, true),
                new Hotkey(5, 0, "Intercept", "I", false, false, false),
                new Hotkey(2, 0, "Nuke Mode", "N", false, false, false),

                // Military
                new Hotkey(2, 0, "Attack", "A", true, false, false),
                new Hotkey(5, 0, "Alert", "A", false, false, false),
                new Hotkey(5, 0, "Set Up Ranged", "S", false, false, false),
                new Hotkey(5, 0, "Ranged Attack", "B", false, false, false),
                new Hotkey(5, 0, "Pillage", "P", false, true, false),
                new Hotkey(2, 0, "Paradrop", "P", false, false, false),
            };

            return hotkeys;
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("apply button clicked");
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("reset button clicked");
        }
    }

    public class Hotkey
    {
        public int File { get; }
        public string DLC { get; }
        public string Function { get; }
        public string Key { get; set; }
        public bool Ctrl { get; set; }
        public bool Shift { get; set; }
        public bool Alt { get; set; }

        public Hotkey(int defFile, int defDLC, string defFunction, string defKey, bool defCtrl, bool defShift, bool defAlt)
        {
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
