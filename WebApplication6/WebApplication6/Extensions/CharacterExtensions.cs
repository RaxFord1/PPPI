using PracticeAPI.DTO.Character;
using PracticeAPI.Models;

namespace PracticeAPI.Extensions
{
    public static class CharacterExtensions
    {
        public static Character ToModel(this CreateCharacterRequest request)
        {
            return new Character
            {
                Name = request.Name,
                BaseATK = request.BaseATK,
                BaseHP = request.BaseHP,
            };
        }
    }
}
