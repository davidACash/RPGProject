using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGProject.Dtos.Character
{
    public class AddCharacterDto
    {
        public string Name { get; set; } = "Vaera";
        public CharacterClass Class { get; set; } = CharacterClass.Warrior;
    }
}