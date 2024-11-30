namespace _00017102_WAD_CW_server.models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        //navigation property
        public ICollection<Post> Posts { get; set; }
    }
}
