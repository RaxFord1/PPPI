using PracticeAPI.DTO.GameAccount;
using PracticeAPI.Models;

namespace PracticeAPI.Services.GameAccountService
{
    public interface IGameAccountService
    {
        Task<BaseResponse<GameAccount>> Get(Guid id);
        Task<BaseResponse<GameAccount>> GetAll();
        Task<BaseResponse<GameAccount>> Post(CreateGameAccountRequest request);
        Task<BaseResponse<GameAccount>> Put(Guid id, UpdateGameAccountRequest ugar);
        Task<BaseResponse<GameAccount>> Delete(Guid id);
    }
}
