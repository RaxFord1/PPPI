using System.Text.Json.Serialization;

namespace PracticeAPI.Models
{
    public class Quest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public List<GameAccountQuest> GameAccounts { get; set; } = new List<GameAccountQuest>();
    }
}
