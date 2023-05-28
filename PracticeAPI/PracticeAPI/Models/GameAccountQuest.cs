using System.Text.Json.Serialization;

namespace PracticeAPI.Models
{
    public class GameAccountQuest
    {
        public Guid GameAccountId { get; set; }
        [JsonIgnore]
        public GameAccount GameAccount { get; set; }

        public Guid QuestId { get; set; }
        [JsonIgnore]
        public Quest Quest { get; set; }
    }
}
