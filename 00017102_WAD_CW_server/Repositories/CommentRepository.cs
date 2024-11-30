using _00017102_WAD_CW_server.Data;
using _00017102_WAD_CW_server.models;

namespace _00017102_WAD_CW_server.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        private readonly GeneralDbContext _context;
        public CommentRepository(GeneralDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<bool> CreateAsync(Comment entity)
        {
            var post = await _context.Posts.FindAsync(entity.PostId);
            if (post == null)
            {
                return false;
            }
            await _context.Comments.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
