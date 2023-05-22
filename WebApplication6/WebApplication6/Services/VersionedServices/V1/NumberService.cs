using PracticeAPI.DTO;

namespace PracticeAPI.Services.VersionedServices.V1
{
    public class NumberService : INumberService
    {
        public async Task<BaseResponse<int>> GetRandomInteger()
        {
            try
            {
                return new BaseResponse<int>
                {
                    Message = "Success",
                    Success = true,
                    StatusCode = 200,
                    ValueCount = 1,
                    Values = new List<int> { await Task.FromResult(new Random().Next(0, 100)) }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>
                {
                    Message = ex.Message,
                };
            }
        }
    }
}
