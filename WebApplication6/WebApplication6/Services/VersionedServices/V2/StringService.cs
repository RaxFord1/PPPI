using PracticeAPI.DTO;

namespace PracticeAPI.Services.VersionedServices.V2
{
    public class StringService : IStringService
    {
        public async Task<BaseResponse<string>> GetSomeText()
        {
            try
            {
                return new BaseResponse<string>
                {
                    Message = "Success",
                    Success = true,
                    StatusCode = 200,
                    ValueCount = 1,
                    Values = new List<string> { await Task.FromResult("Some text") }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>
                {
                    Message = ex.Message,
                };
            }
        }
    }
}
