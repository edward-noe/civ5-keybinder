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

            List<Hotkey> hotkeys = new List<Hotkey>();
            hotkeys.Add(new Hotkey(1, "Civilopedia"));
            hotkeys[0].DefineBinding("F1", false, false, false);

            InitializeGrid(hotkeys);

        }

        public void InitializeGrid(List<Hotkey> hotkeys)
        {
            void AddText(string text, int row, int col)
            {
                main_grid.RowDefinitions.Add(new RowDefinition());
                TextBlock text_block = new TextBlock();
                text_block.Text = text;
                Grid.SetRow(text_block, row);
                Grid.SetColumn(text_block, col);
                main_grid.Children.Add(text_block);
            }

            for (int hotkey_num = 0; hotkey_num < hotkeys.Count; hotkey_num++)
            {
                for (int attribute_num = 0; attribute_num < 3; attribute_num++)
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
                            AddText(hotkeys[hotkey_num].Key, hotkey_num + 1, 2);
                            break;
                    }
                }                
            }
        }
    }

    public class Hotkey
    {
        public int ID { get; }
        public string DLC { get; }
        public string Function { get; }
        public string Key { get; set; }
        public bool Ctrl { get; set; }
        public bool Shift { get; set; }
        public bool Alt { get; set; }

        public Hotkey(int numDLC, string defFunction)
        {
            switch (numDLC)
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
