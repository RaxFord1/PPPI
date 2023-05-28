using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Data;
using PracticeAPI.DTO;
using PracticeAPI.DTO.Quest;
using PracticeAPI.Extensions;
using PracticeAPI.Models;

namespace PracticeAPI.Services.QuestService
{
    public class QuestService : IQuestService
    {
        private readonly PracticeContext _context;

        public QuestService(PracticeContext context)
        {
            _context = context;
        }


        public async Task<BaseResponse<Quest>> Get(Guid id)
        {
            try
            {
                var quest = await Task.FromResult(_context.Quests.Find(id));
                if (quest != null)
                {
                    return new BaseResponse<Quest>()
                    {
                        Success = true,
                        Values = new List<Quest> { quest },
                        ValueCount = 1,
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new BaseResponse<Quest>
                {
                    Message = "Quest not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Quest>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Quest>> GetAll()
        {
            try
            {
                var quests = await _context.Quests.ToListAsync();
                return new BaseResponse<Quest>()
                {
                    Success = true,
                    Values = quests,
                    ValueCount = quests.Count,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Quest>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Quest>> Post(CreateQuestRequest request)
        {
            try
            {
                var quest = request.ToModel();
                var id = await Task.FromResult(quest.Id = Guid.NewGuid());
                _context.Quests.Add(quest);
                await _context.SaveChangesAsync();

                return new BaseResponse<Quest>()
                {
                    StatusCode = StatusCodes.Status201Created,
                    Success = true,
                    Values = new List<Quest> { quest },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Quest>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Quest>> Put(Guid id, Quest quest)
        {
            try
            {
                if (quest.Id != id)
                {
                    return new BaseResponse<Quest>() { 
                        Message = "Id mismatch"
                    };
                }

                var current = await Task.FromResult(_context.Quests.Find(id));
                if (current == null)
                {
                    return new BaseResponse<Quest>()
                    {
                        Message = "Quest not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                if (!string.IsNullOrWhiteSpace(quest.Name))
                {
                    current.Name = quest.Name;
                }
                if (!string.IsNullOrWhiteSpace(quest.Description))
                {
                    current.Description = quest.Description;
                }
                await _context.SaveChangesAsync();

                return new BaseResponse<Quest>()
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Values = new List<Quest> { current },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Quest>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Quest>> Delete(Guid id)
        {
            try
            {
                var current = _context.Quests.Find(id);
                if (current == null)
                {
                    return new BaseResponse<Quest>()
                    {
                        Message = "Quest not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                await Task.FromResult(_context.Quests.Remove(current));
                return new BaseResponse<Quest>()
                {
                    Success = true,
                    Message = "Quest deleted",
                    StatusCode = StatusCodes.Status204NoContent
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Quest>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
