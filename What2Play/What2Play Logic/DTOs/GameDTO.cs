namespace What2Play_Logic.DTOs
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TypeId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public int SourceId { get; set; }
        public string SourceName { get; set; } = string.Empty;
        public bool Played { get; set; }
    }
}
