using Microsoft.EntityFrameworkCore;
using PracticeAPI.Data;
using PracticeAPI.DTO;
using PracticeAPI.DTO.Character;
using PracticeAPI.Extensions;
using PracticeAPI.Models;

namespace PracticeAPI.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly PracticeContext _context;

        public CharacterService(PracticeContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<Character>> Get(Guid id)
        {
            try
            {
                var character = await Task.FromResult(_context.Characters.Find(id));
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
                var characters = await _context.Characters.ToListAsync();
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
                _context.Characters.Add(character);
                await _context.SaveChangesAsync();

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

                var current = await Task.FromResult(_context.Characters.Find(id));
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
                await _context.SaveChangesAsync();

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
                var current = _context.Characters.Find(id);
                if (current == null)
                {
                    return new BaseResponse<Character>()
                    {
                        Message = "Character not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                _context.Characters.Remove(current);
                await _context.SaveChangesAsync();

                return new BaseResponse<Character>()
                {
                    Success = true,
                    Message = "Charachter deleted",
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
