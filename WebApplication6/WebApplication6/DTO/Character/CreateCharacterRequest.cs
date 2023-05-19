namespace PracticeAPI.DTO.Character
{
    public class CreateCharacterRequest
    {
        public string Name { get; set; } = string.Empty;
        public int BaseHP { get; set; } = 100;
        public int BaseATK { get; set; } = 100;
    }
}
