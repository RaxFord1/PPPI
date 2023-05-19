using PracticeAPI.DTO;
using PracticeAPI.DTO.Quest;
using PracticeAPI.Extensions;
using PracticeAPI.Models;

namespace PracticeAPI.Services.QuestService
{
    public class QuestService : IQuestService
    {
        private readonly List<Quest> _questRepository;

        public QuestService()
        {
            _questRepository = new List<Quest>()
            {
                new Quest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Find the Golden Fleece",
                    Description = "The Golden Fleece is a legendary object of great power. It is said to be the skin of a magical ram that could fly. The fleece is guarded by a dragon in Colchis.",
                },
                new Quest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Retrieve the Holy Grail",
                    Description = "The Holy Grail is a sacred cup that is said to have been used by Jesus Christ at the Last Supper. It is said to have magical powers, such as the ability to heal the sick and wounded. The Grail is said to be hidden in a castle somewhere in Europe.",
                },
                new Quest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Kill the Wicked Witch of the West",
                    Description = "The Wicked Witch of the West is a powerful witch who rules over the land of Oz. She is said to be very cruel and wicked. The only way to defeat her is to kill her.",
                },
                new Quest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Find the One Ring",
                    Description = "The One Ring is a powerful magical ring that was created by the Dark Lord Sauron. The ring can control the minds of other people and can be used to summon armies of orcs. The only way to destroy the ring is to throw it into the fires of Mount Doom.",
                },
                new Quest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Save the Princess",
                    Description = "The Princess is a beautiful young woman who has been kidnapped by an evil dragon. The only way to save her is to defeat the dragon and rescue her from his lair.",
                },
                new Quest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Kill the Minotaur",
                    Description = "The Minotaur is a half-man, half-bull creature that lives in the Labyrinth. It is said to be very strong and dangerous. The only way to kill the Minotaur is to enter the Labyrinth and find its lair.",
                },
                new Quest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Find the Lost City of Atlantis",
                    Description = "The Lost City of Atlantis is a legendary city that is said to have sunk beneath the waves. The city is said to be full of treasure and riches. The only way to find the city is to follow a map that leads to its location.",
                },
                new Quest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Reach the top of Mount Everest",
                    Description = "Mount Everest is the highest mountain in the world. It is located in the Himalayas. The only way to reach the top of the mountain is to climb it.",
                },
                new Quest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Find the cure for cancer",
                    Description = "Cancer is a deadly disease that affects millions of people around the world. There is no known cure for cancer. The only way to find a cure is to continue research and development.",
                },
                new Quest()
                {
                    Id = Guid.NewGuid(),
                    Name = "End world hunger",
                    Description = "World hunger is a serious problem that affects millions of people around the world. The only way to end world hunger is to increase food production and to distribute food more evenly.",
                },
            };
        }


        public async Task<BaseResponse<Quest>> Get(Guid id)
        {
            try
            {
                var quest = await Task.FromResult(_questRepository.Find(g => g.Id == id));
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
                var quests = await Task.FromResult(_questRepository);
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
                var id = await Task.FromResult(() => {
                    quest.Id = Guid.NewGuid();
                    _questRepository.Add(quest);
                    return quest.Id;
                });

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

                var current = await Task.FromResult(_questRepository.Find(x => x.Id == id));
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
                var current = _questRepository.Find(x => x.Id == id);
                if (current == null)
                {
                    return new BaseResponse<Quest>()
                    {
                        Message = "Quest not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var isRemoved = await Task.FromResult(_questRepository.Remove(current));
                if (isRemoved)
                {
                    return new BaseResponse<Quest>()
                    {
                        Success = true,
                        Message = "Quest deleted",
                        StatusCode = StatusCodes.Status204NoContent
                    };
                }
                return new BaseResponse<Quest>()
                {
                    Message = "The quest was not deleted",
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
