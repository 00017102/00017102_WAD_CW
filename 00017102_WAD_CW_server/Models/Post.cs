namespace _00017102_WAD_CW_server.models
{
    public class Post
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public String AuthorName { get; set; }
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        //foreign key
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //navigation property
        public ICollection<Comment> Comments { get; set; }

    }
}
