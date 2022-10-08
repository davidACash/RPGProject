using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGProject.Dtos.Character;
using RPGProject.Services.CharacterService;

namespace RPGProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        /// <summary>
        /// Get all characters in the game.
        /// </summary>
        [HttpGet("GetAllCharacters")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAllCharacters()
        {
            var response = await _characterService.GetAllCharacters();
            if(response.Data == null)
            {
                return NotFound(response); 
            }
            return Ok(response);
        }

        /// <summary>
        /// Only returns the characters belonging to the player.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsersCharacters")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetUsersCharacters()
        {
            var response = await _characterService.GetUsersCharacters();
            if(response.Data == null)
            {
                return NotFound(response); 
            }
            return Ok(response);
        }

        /// <summary>
        /// Returns the player's selected character.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetCharacter(int id)
        {
            var response = await _characterService.GetCharacterById(id);
            if(response.Data == null)
            {
                return NotFound(response); 
            }
            return Ok(response);
        }

        /// <summary>
        /// Returns all available opponents for the player's character to fight.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetOpponents")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetOpponents(int id)
        {
            var response = await _characterService.GetOpponents(id);
            if(response.Data == null)
            {
                return NotFound(response); 
            }
            return Ok(response);
        }

        /// <summary>
        /// Creates and adds a new character tied to the player's account.
        /// </summary>
        /// <param name="newCharacter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {;
            var response = await _characterService.AddCharacter(newCharacter);
            if(response.Data == null)
            {
                return BadRequest(response); 
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates the character's attributes.
        /// </summary>
        /// <param name="updatedCharacter"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);
            if(response.Data == null)
            {
                return BadRequest(response); 
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a player's character.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if(response.Data == null)
            {
                return NotFound(response); 
            }
            return Ok(response);
        }
    }
}