using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPGProject.Dtos.Skill;
using RPGProject.Dtos.Weapon;

namespace RPGProject.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Vaera";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Dexterity { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public int Vitality { get; set; } = 10;
        public CharacterClass Class { get; set; } = CharacterClass.Warrior;
        public GetWeaponDto Weapon { get; set; }
        public List<GetSkillDto> Skills { get; set; }
    }
}