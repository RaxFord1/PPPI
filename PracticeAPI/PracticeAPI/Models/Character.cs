using System.Text.Json.Serialization;

namespace PracticeAPI.Models
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BaseHP { get; set; } = 100;
        public int BaseATK { get; set; } = 100;

        [JsonIgnore]
        public List<GameAccountCharacter> GameAccounts { get; set; } = new List<GameAccountCharacter>();
    }
}
