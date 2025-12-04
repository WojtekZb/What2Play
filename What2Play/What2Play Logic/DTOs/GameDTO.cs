namespace What2Play_Logic.DTOs
{
    public class GameDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public bool Played { get; set; }
    }
}
