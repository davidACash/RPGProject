using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RPGProject.Data;
using RPGProject.Dtos.Character;

namespace RPGProject.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters.Where(c => c.User.Id == GetUserId()).Select(c=> _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        // TODO: Don't return list. Just return a 201.
        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();

            try 
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if(character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();
                    response.Data = await _context.Characters.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                }
            }
            catch(Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            response.Data = await _context.Characters.Where(c => c.User.Id == GetUserId()).Select(c=> _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();

            try {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id && c.User.Id == GetUserId());

                if(character != null)
                {
                    character.HitPoints = updatedCharacter.HitPoints;
                    character.Strength = updatedCharacter.Strength;
                    character.Dexterity = updatedCharacter.Dexterity;
                    character.Intelligence = updatedCharacter.Intelligence;
                    character.Vitality = updatedCharacter.Vitality;

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                }
            }
            catch(Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }

            return response;
        }
    }
}