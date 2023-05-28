using PracticeAPI.DTO;
using PracticeAPI.Models;

namespace PracticeAPI.Services.VersionedServices.V1
{
    public interface INumberService
    {
        Task<BaseResponse<int>> GetRandomInteger();
    }
}
