namespace PracticeAPI.Models
{
    public class GameAccount
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public int Level { get; set; }

        public List<Character> Characters { get; set; } = new List<Character>();
        public List<Quest> Quests { get; set; } = new List<Quest>();
    }
}
