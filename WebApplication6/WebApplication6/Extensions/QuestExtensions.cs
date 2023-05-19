using PracticeAPI.DTO.Quest;
using PracticeAPI.Models;

namespace PracticeAPI.Extensions
{
    public static class QuestExtensions
    {
        public static Quest ToModel(this CreateQuestRequest request)
        {
            return new Quest
            {
                Name = request.Name,
                Description = request.Description,
            };
        }
    }
}
