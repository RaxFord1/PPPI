using PracticeAPI.DTO;
using PracticeAPI.DTO.Quest;
using PracticeAPI.Models;

namespace PracticeAPI.Services.QuestService
{
    public interface IQuestService
    {
        Task<BaseResponse<Quest>> Get(Guid id);
        Task<BaseResponse<Quest>> GetAll();
        Task<BaseResponse<Quest>> Post(CreateQuestRequest request);
        Task<BaseResponse<Quest>> Put(Guid id, Quest quest);
        Task<BaseResponse<Quest>> Delete(Guid id);
    }
}
