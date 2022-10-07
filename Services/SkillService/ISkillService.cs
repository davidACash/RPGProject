using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPGProject.Dtos.Character;
using RPGProject.Dtos.Skill;

namespace RPGProject.Services.SkillService
{
    public interface ISkillService
    {
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
    }
}