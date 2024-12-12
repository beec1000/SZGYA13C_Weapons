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
            WeaponAttributesComboBox.SelectedIndex = 0;
            WeaponChanceComboBox.SelectedIndex = 0;
            MinDamageTextBox.Text = "";
            MaxDamageTextBox.Text = "";
            ExtraStats.ItemsSource = weapons;
        }

        private string selectedWeaponType = "All";
        private string selectedAttribute = "All";
        private string selectedChance = "All";

        private void WeaponTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedWeaponType = (WeaponTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            ApplyFilters();
        }

        private void WeaponAttributeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAttribute = (WeaponAttributesComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            ApplyFilters();
        }

        private void WeaponChanceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedChance = (WeaponChanceComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selectedChance != "All")
            {
                WeaponAttributesComboBox.IsEnabled = false;
                selectedAttribute = "All";
            }
            else
            {
                WeaponAttributesComboBox.IsEnabled = true;
            }
            ApplyFilters();
        }

        private void DamageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filteredWeapons = weapons.AsEnumerable();

            if (selectedWeaponType != "All")
            {
                filteredWeapons = filteredWeapons.Where(w => w.Identity.Type == selectedWeaponType);
            }

            if (selectedAttribute != "All")
            {
                filteredWeapons = filteredWeapons.OrderByDescending(w => GetAttributeValue(w, selectedAttribute));
            }

            if (selectedChance != "All")
            {
                filteredWeapons = filteredWeapons.OrderByDescending(w => GetChanceValue(w, selectedChance));
            }

            if (int.TryParse(MinDamageTextBox.Text, out int minDamage))
            {
                filteredWeapons = filteredWeapons.Where(w => w.Stats.MinDmg >= minDamage);
            }

            if (int.TryParse(MaxDamageTextBox.Text, out int maxDamage))
            {
                filteredWeapons = filteredWeapons.Where(w => w.Stats.MaxDmg <= maxDamage);
            }

            ExtraStats.ItemsSource = filteredWeapons.ToList();
        }

        private int GetAttributeValue(Weapon weapon, string attribute)
        {
            switch (attribute)
            {
                case "Strength":
                    return weapon.Attributes.Strength;
                case "Dexterity":
                    return weapon.Attributes.Dexterity;
                case "Wisdom":
                    return weapon.Attributes.Wisdom;
                case "Perception":
                    return weapon.Attributes.Perception;
                default:
                    return 0;
            }
        }

        private int GetChanceValue(Weapon weapon, string chance)
        {
            switch (chance)
            {
                case "Hit Chance":
                    return weapon.Chances.HitChance;
                case "Stun Chance":
                    return weapon.Chances.StunChance;
                case "Heavy Attack Chance":
                    return weapon.Chances.HeavyAttackChance;
                default:
                    return 0;
            }
        }
    }
}