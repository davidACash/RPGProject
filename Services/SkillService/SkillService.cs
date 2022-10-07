using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RPGProject.Data;
using RPGProject.Dtos.Character;
using RPGProject.Dtos.Skill;

namespace RPGProject.Services.SkillService
{
    public class SkillService : ISkillService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public SkillService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper )
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.User.Id == GetUserId());

                if(character != null)
                {
                    var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                    if(skill != null)
                    {
                        character.Skills.Add(skill);
                        await _context.SaveChangesAsync();
                        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                    }
                    else
                    {
                       serviceResponse.Success = false;
                        serviceResponse.Message = "Skill not found."; 
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Charcter not found.";
                }
            }
            catch(Exception exc)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = exc.Message;
                serviceResponse.InnerExceptionMessage = string.IsNullOrEmpty(exc.InnerException.Message) ? string.Empty : exc.InnerException.Message;
            }
            return serviceResponse;
        }
    }
}