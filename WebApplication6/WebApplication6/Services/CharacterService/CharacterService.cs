using PracticeAPI.DTO.Character;
using PracticeAPI.Extensions;
using PracticeAPI.Models;

namespace PracticeAPI.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly List<Character> _characterRepository;

        public CharacterService()
        {
            _characterRepository = new List<Character>()
            {
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Artorias the Abysswalker",
                    BaseHP = 1000,
                    BaseATK = 200,
                },
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Sif the Great Grey Wolf",
                    BaseHP = 500,
                    BaseATK = 150,
                },
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Gwyn, Lord of Cinder",
                    BaseHP = 2000,
                    BaseATK = 300,
                },
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nito, the Gravelord",
                    BaseHP = 1500,
                    BaseATK = 250,
                },
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Seath the Scaleless",
                    BaseHP = 1000,
                    BaseATK = 200,
                },
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "The Four Kings",
                    BaseHP = 1500,
                    BaseATK = 250,
                },
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Ciaran the Darkmoon",
                    BaseHP = 500,
                    BaseATK = 150,
                },
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Ornstein and Smough",
                    BaseHP = 2000,
                    BaseATK = 300,
                },
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Manus, Father of the Abyss",
                    BaseHP = 3000,
                    BaseATK = 400,
                },
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Lancelot du Lac",
                    BaseHP = 2400,
                    BaseATK = 150,
                }
            };
        }

        public async Task<BaseResponse<Character>> Get(Guid id)
        {
            try
            {
                var character = await Task.FromResult(_characterRepository.Find(g => g.Id == id));
                if (character != null)
                {
                    return new BaseResponse<Character>()
                    {
                        Success = true,
                        Values = new List<Character> { character },
                        ValueCount = 1,
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new BaseResponse<Character>
                {
                    Message = "Character not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Character>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Character>> GetAll()
        {
            try
            {
                var characters = await Task.FromResult(_characterRepository);
                return new BaseResponse<Character>()
                {
                    Success = true,
                    Values = characters,
                    ValueCount = characters.Count,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Character>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Character>> Post(CreateCharacterRequest request)
        {
            try
            {
                var character = request.ToModel();
                var id = await Task.FromResult(character.Id = Guid.NewGuid());
                _characterRepository.Add(character);

                return new BaseResponse<Character>()
                {
                    StatusCode = StatusCodes.Status201Created,
                    Success = true,
                    Values = new List<Character> { character },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Character>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Character>> Put(Guid id, Character character)
        {
            try
            {
                if (character.Id != id)
                {
                    return new BaseResponse<Character>()
                    {
                        Message = "Id mismatch"
                    };
                }

                var current = await Task.FromResult(_characterRepository.Find(x => x.Id == id));
                if (current == null)
                {
                    return new BaseResponse<Character>()
                    {
                        Message = "Character not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                if (!string.IsNullOrWhiteSpace(character.Name))
                {
                    current.Name = character.Name;
                }
                if (character.BaseHP >= 0)
                {
                    current.BaseHP = character.BaseHP;
                }
                if (character.BaseATK >= 0)
                {
                    current.BaseATK = character.BaseATK;
                }

                return new BaseResponse<Character>()
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Values = new List<Character> { current },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Character>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Character>> Delete(Guid id)
        {
            try
            {
                var current = _characterRepository.Find(x => x.Id == id);
                if (current == null)
                {
                    return new BaseResponse<Character>()
                    {
                        Message = "Character not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var isRemoved = await Task.FromResult(_characterRepository.Remove(current));
                if (isRemoved)
                {
                    return new BaseResponse<Character>()
                    {
                        Success = true,
                        Message = "Charachter deleted",
                        StatusCode = StatusCodes.Status204NoContent
                    };
                }
                return new BaseResponse<Character>()
                {
                    Message = "The character was not deleted",
                    StatusCode = StatusCodes.Status204NoContent
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Character>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
