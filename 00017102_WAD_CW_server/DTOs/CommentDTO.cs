namespace _00017102_WAD_CW_server.DTOs
{
    public class CommentCreateDTO
    {
        public int PostId { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
    }
    public class CommentResponseDTO
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
    }
}
