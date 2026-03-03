namespace SkillStage.DTOs
{
    public class PostCreateDTO
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int Type { get; set; }
        public string? ImageUrl { get; set; }
    }
}
