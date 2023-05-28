namespace PracticeAPI.Models
{
    public class GameAccount
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public int Level { get; set; }

        public List<GameAccountCharacter> Characters { get; set; } = new List<GameAccountCharacter>();
        public List<GameAccountQuest> Quests { get; set; } = new List<GameAccountQuest>();
    }
}
