namespace SkillStage.DTO{

public class PostResponseDTO
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Type { get; set; } 
        public string UserId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
