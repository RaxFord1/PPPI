using PracticeAPI.DTO;

namespace PracticeAPI.Services.VersionedServices.V2
{
    public interface IStringService
    {
        Task<BaseResponse<string>> GetSomeText();
    }
}
