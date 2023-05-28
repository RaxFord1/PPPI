using System.Text.Json.Serialization;

namespace PracticeAPI.Models
{
    public class GameAccountCharacter
    {
        public Guid GameAccountId { get; set; }
        [JsonIgnore]
        public GameAccount GameAccount { get; set; }

        public Guid CharacterId { get; set; }
        [JsonIgnore]
        public Character Character { get; set; }
    }
}
