using PracticeAPI.DTO;
using PracticeAPI.DTO.Character;
using PracticeAPI.Models;

namespace PracticeAPI.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<BaseResponse<Character>> Get(Guid id);
        Task<BaseResponse<Character>> GetAll();
        Task<BaseResponse<Character>> Post(CreateCharacterRequest request);
        Task<BaseResponse<Character>> Put(Guid id, Character character);
        Task<BaseResponse<Character>> Delete(Guid id);
    }
}
