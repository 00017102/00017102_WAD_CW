namespace _00017102_WAD_CW_server.models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
