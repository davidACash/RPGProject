using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGProject.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Vaera";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Dexterity { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public int Vitality { get; set; } = 10;
        public CharacterClass Class { get; set; } = CharacterClass.Warrior;
        public User? User { get; set; }
    }
}