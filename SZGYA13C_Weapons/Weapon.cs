using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SZGYA13C_Weapons
{
    public class WeaponIdentity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public WeaponIdentity(int id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }

    public class WeaponStats
    {
        public int MinDmg { get; set; }
        public int MaxDmg { get; set; }
        public double AttackSpeed { get; set; }
        public int Range { get; set; }
        public double OffHandWeaponAttackChance { get; set; }

        public WeaponStats(int minDmg, int maxDmg, double attackSpeed, int range, double offHandWeaponAttackChance)
        {
            MinDmg = minDmg;
            MaxDmg = maxDmg;
            AttackSpeed = attackSpeed;
            Range = range;
            OffHandWeaponAttackChance = offHandWeaponAttackChance;
        }
    }

    public class WeaponAttributes
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Wisdom { get; set; }
        public int Perception { get; set; }

        public WeaponAttributes(int strength, int dexterity, int wisdom, int perception)
        {
            Strength = strength;
            Dexterity = dexterity;
            Wisdom = wisdom;
            Perception = perception;
        }
    }

    public class WeaponHitChances
    {
        public int HitChance { get; set; }
        public int StunChance { get; set; }
        public int HeavyAttackChance { get; set; }

        public WeaponHitChances(int hitChance, int stunChance, int heavyAttackChance)
        {
            HitChance = hitChance;
            StunChance = stunChance;
            HeavyAttackChance = heavyAttackChance;
        }
    }

    public class Weapon
    {
        public WeaponIdentity Identity { get; set; }
        public WeaponStats Stats { get; set; }
        public WeaponAttributes Attributes { get; set; }
        public WeaponHitChances Chances { get; set; }

        public Weapon(WeaponIdentity identity, WeaponStats stats, WeaponAttributes attributes, WeaponHitChances chances)
        {
            Identity = identity;
            Stats = stats;
            Attributes = attributes;
            Chances = chances;
        }

        public override string ToString()
        {
            return $"Weapon: {Identity.Name} (ID: {Identity.Id})\n" +
                   $"Type: {Identity.Type}\n" +
                   $"Damage: {Stats.MinDmg}-{Stats.MaxDmg}, Attack Speed: {Stats.AttackSpeed}\n" +
                   $"Range: {Stats.Range}m, Off-Hand Weapon Attack Chance: {Stats.OffHandWeaponAttackChance}%\n" +
                   $"Strength: {Attributes.Strength}, Dexterity: {Attributes.Dexterity}, Wisdom: {Attributes.Wisdom}, Perception: {Attributes.Perception}\n" +
                   $"Hit Chance: {Chances.HitChance}, Stun Chance: {Chances.StunChance}, Heavy Attack Chance: {Chances.HeavyAttackChance}";
        }

        public static List<Weapon> FromFile(string filePath)
        {
            List<Weapon> weapons = new List<Weapon>();

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines.Skip(1))
            {
                string[] data = line.Split(';');

                int id = int.Parse(data[0]);
                string name = data[1];
                string type = data[2];

                //Weapon Stats
                int minDmg = int.Parse(data[3]);
                int maxDmg = int.Parse(data[4]);
                double attackSpeed = double.Parse(data[5]);
                int range = int.Parse(data[6]);
                double offHandWeaponAttackChance = data[7] == "-" ? 0 : double.Parse(data[7]);
                WeaponStats stats = new WeaponStats(minDmg, maxDmg, attackSpeed, range, offHandWeaponAttackChance);

                //Weapon Attributes
                int strength = data[8] == "-" ? 0 : int.Parse(data[8]);
                int dexterity = data[9] == "-" ? 0 : int.Parse(data[9]);
                int wisdom = data[10] == "-" ? 0 : int.Parse(data[10]);
                int perception = data[11] == "-" ? 0 : int.Parse(data[11]);
                WeaponAttributes attributes = new WeaponAttributes(strength, dexterity, wisdom, perception);

                // Parse Weapon Hit Chances
                int hitChance = data[12] == "-" ? 0 : int.Parse(data[12]);
                int stunChance = data[13] == "-" ? 0 : int.Parse(data[13]);
                int heavyAttackChance = data[14] == "-" ? 0 : int.Parse(data[14]);

                WeaponHitChances chances = new WeaponHitChances(hitChance, stunChance, heavyAttackChance);

                // Create Weapon and add to list
                Weapon weapon = new Weapon(new WeaponIdentity(id, name, type), stats, attributes, chances);
                weapons.Add(weapon);
            }

            return weapons;
        }
    }
}
