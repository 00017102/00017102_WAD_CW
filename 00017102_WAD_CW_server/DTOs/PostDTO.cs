namespace _00017102_WAD_CW_server.DTOs
{
    public class PostCreateDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public int CategoryId { get; set; }
    }

    public class PostResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }
        public string AuthorName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
        public string CategoryName { get; set; }
    }

}
