using Microsoft.EntityFrameworkCore;
using PracticeAPI.Data;
using PracticeAPI.DTO;
using PracticeAPI.DTO.GameAccount;
using PracticeAPI.Models;
using PracticeAPI.Services.CharacterService;
using PracticeAPI.Services.QuestService;
using System.Linq;

namespace PracticeAPI.Services.GameAccountService
{
    public class GameAccountService : IGameAccountService
    {
        private readonly ICharacterService _characterService;
        private readonly IQuestService _questService;
        private readonly PracticeContext _context;

        public GameAccountService(ICharacterService characterService, IQuestService questService, PracticeContext context)
        {
            _characterService = characterService;
            _questService = questService;
            _context = context;
        }

        public async Task<BaseResponse<GameAccount>> Get(Guid id)
        {
            try
            {
                var gameAccount = await _context.Accounts
                    .Include(x => x.Quests)
                    .Include(x => x.Characters)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (gameAccount != null)
                {
                    return new BaseResponse<GameAccount>()
                    {
                        Success = true,
                        Values = new List<GameAccount> { gameAccount },
                        ValueCount = 1,
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new BaseResponse<GameAccount>()
                {
                    Message = "Game account not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GameAccount>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<GameAccount>> GetAll()
        {
            try
            {
                var gameAccounts = await _context.Accounts
                    .Include(x => x.Quests)
                    .Include(x => x.Characters)
                    .ToListAsync();
                return new BaseResponse<GameAccount>()
                {
                    Success = true,
                    Values = gameAccounts,
                    ValueCount = gameAccounts.Count,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GameAccount>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<GameAccount>> Post(CreateGameAccountRequest request)
        {
            try
            {
                var characters = await Task.WhenAll(request.CharacterIds.Select(_characterService.Get));
                var quests = await Task.WhenAll(request.QuestIds.Select(_questService.Get));

                var gameAccount = new GameAccount
                {
                    Id = Guid.NewGuid(),
                    Username = request.Username,
                    Created = DateTime.Now,
                    Level = 1,

                    Characters = characters.Select(x => new GameAccountCharacter { CharacterId = x.Values.First().Id }).ToList(),
                    Quests = quests.Select(x => new GameAccountQuest { QuestId = x.Values.First().Id }).ToList(),
                };

                _context.Accounts.Add(gameAccount);
                await _context.SaveChangesAsync();

                return new BaseResponse<GameAccount>()
                {
                    StatusCode = StatusCodes.Status201Created,
                    Success = true,
                    Values = new List<GameAccount> { gameAccount },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GameAccount>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<GameAccount>> Put(Guid id, UpdateGameAccountRequest request)
        {
            try
            {
                if (request.Id != id)
                {
                    return new BaseResponse<GameAccount>()
                    {
                        Message = "Id mismatch"
                    };
                }

                var current = await Task.FromResult(_context.Accounts.Include(x => x.Characters).Include(x => x.Quests).SingleOrDefault(x => x.Id == id));
                if (current == null)
                {
                    return new BaseResponse<GameAccount>()
                    {
                        Message = "Game account not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                if (!string.IsNullOrWhiteSpace(request.Username))
                {
                    current.Username = request.Username;
                }
                current.Level = request.Level;

                var characters = await Task.WhenAll(request.CharacterIds.Select(_characterService.Get));
                var quests = await Task.WhenAll(request.QuestIds.Select(_questService.Get));

                current.Characters.Clear();
                current.Characters.AddRange(characters.Select(x => new GameAccountCharacter { CharacterId = x.Values.First().Id }));

                current.Quests.Clear();
                current.Quests.AddRange(quests.Select(x => new GameAccountQuest { QuestId = x.Values.First().Id }));

                await _context.SaveChangesAsync();

                return new BaseResponse<GameAccount>()
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Values = new List<GameAccount> { current },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GameAccount>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        public async Task<BaseResponse<GameAccount>> Delete(Guid id)
        {
            try
            {
                var current = await Task.FromResult(_context.Accounts.Find(id));
                if (current == null)
                {
                    return new BaseResponse<GameAccount>()
                    {
                        Message = "Game account not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                _context.Accounts.Remove(current);
                await _context.SaveChangesAsync();

                return new BaseResponse<GameAccount>()
                {
                    Success = true,
                    Message = "Game account deleted",
                    StatusCode = StatusCodes.Status204NoContent
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GameAccount>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
