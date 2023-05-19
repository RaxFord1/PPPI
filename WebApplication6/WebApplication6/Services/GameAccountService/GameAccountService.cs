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
        private readonly List<GameAccount> _gameAccounts = new List<GameAccount>();

        public GameAccountService(ICharacterService characterService, IQuestService questService)
        {
            _characterService = characterService;
            _questService = questService;

            InitGameAccounts();
        }

        private async void InitGameAccounts()
        {
            var characters = await _characterService.GetAll();
            var quests = await _questService.GetAll();
            var random = new Random();

            // Create 10 game accounts
            for (int i = 0; i < 10; i++)
            {
                var gameAccount = new GameAccount
                {
                    Id = Guid.NewGuid(),
                    Username = $"Player{i + 1}",
                    Created = DateTime.Now,
                    Level = 1,
                };

                // Assign random characters to the game account
                for (int j = 0; j < 3; j++)
                {
                    var character = characters.Values.ElementAt(random.Next(characters.ValueCount));
                    gameAccount.Characters.Add(character);
                }

                // Assign random quests to the game account
                for (int j = 0; j < 3; j++)
                {
                    var quest = quests.Values.ElementAt(random.Next(characters.ValueCount-1));
                    gameAccount.Quests.Add(quest);
                }

                _gameAccounts.Add(gameAccount);
            }
        }

        public async Task<BaseResponse<GameAccount>> Get(Guid id)
        {
            try
            {
                var gameAccount = await Task.FromResult(_gameAccounts.Find(x => x.Id == id));
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
                var gameAccounts = await Task.FromResult(_gameAccounts);
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

                    Characters = characters.Select(x => x.Values.First()).ToList(),
                    Quests = quests.Select(x => x.Values.First()).ToList(),
                };

                _gameAccounts.Add(gameAccount);

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

                var current = await Task.FromResult(_gameAccounts.Find(x => x.Id == id));
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

                current.Characters = characters.Select(x => x.Values.First()).ToList();
                current.Quests = quests.Select(x => x.Values.First()).ToList();

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
                var current = await Task.FromResult(_gameAccounts.Find(x => x.Id == id));
                if (current == null)
                {
                    return new BaseResponse<GameAccount>()
                    {
                        Message = "Game account not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                _gameAccounts.Remove(current);

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
