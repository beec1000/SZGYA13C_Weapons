using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Linq;

namespace SZGYA13C_Weapons
{
    public partial class MainWindow : Window
    {
        List<Weapon> weapons = new List<Weapon>();

        public MainWindow()
        {
            InitializeComponent();
            weapons = Weapon.FromFile(@"..\\..\\..\\src\\weapon_stats.txt");
            WeaponTypeComboBox.SelectedIndex = 0;
            ExtraStats.ItemsSource = weapons;
        }

        private void WeaponTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedType = (WeaponTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedType != null)
            {
                if (selectedType == "All")
                {
                    ExtraStats.ItemsSource = weapons;
                }
                else
                {
                    var filteredWeapons = weapons.Where(w => w.Identity.Type == selectedType).ToList();
                    ExtraStats.ItemsSource = filteredWeapons;
                }
            }
        }
    }
}
