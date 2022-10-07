using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPGProject.Dtos.Character;
using RPGProject.Dtos.Weapon;

namespace RPGProject.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}