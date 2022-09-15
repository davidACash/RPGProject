using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPGProject.Dtos.Character;

namespace RPGProject.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
    }
}