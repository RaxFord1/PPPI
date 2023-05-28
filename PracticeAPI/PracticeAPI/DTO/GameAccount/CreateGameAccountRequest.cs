namespace PracticeAPI.DTO.GameAccount
{
    public class CreateGameAccountRequest
    {
        public string Username { get; set; } = string.Empty;
        public IEnumerable<Guid> CharacterIds { get; set; } = new HashSet<Guid>();
        public IEnumerable<Guid> QuestIds { get; set; } = new HashSet<Guid>();
    }
}
