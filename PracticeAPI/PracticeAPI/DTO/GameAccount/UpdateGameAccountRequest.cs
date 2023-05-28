namespace PracticeAPI.DTO.GameAccount
{
    public class UpdateGameAccountRequest
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Level { get; set; }
        public IEnumerable<Guid> CharacterIds { get; set; } = new HashSet<Guid>();
        public IEnumerable<Guid> QuestIds { get; set; } = new HashSet<Guid>();
    }
}
