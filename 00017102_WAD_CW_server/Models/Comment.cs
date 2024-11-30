namespace _00017102_WAD_CW_server.models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public String AuthorName { get; set; }
        //foreign key
        public int PostId { get; set; }
        //navigation property
        public Post Post { get; set; }
    }
}
