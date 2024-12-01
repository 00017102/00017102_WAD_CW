using _00017102_WAD_CW_server.models;

namespace _00017102_WAD_CW_server.DTOs
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; }
    }

    public class CategoryResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryWithPostsResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PostResponseDTO> Posts { get; set; }
    }
}
