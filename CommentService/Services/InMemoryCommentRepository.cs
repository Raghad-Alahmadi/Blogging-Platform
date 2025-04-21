using Blogging_Platform.DTO;

namespace CommentService.Services
{
    public class InMemoryCommentRepository : ICommentRepository
    {
        private readonly List<CommentDto> _comments = new();

        public IEnumerable<CommentDto> GetByPostId(Guid postId)
        {
            return _comments.Where(c => c.PostId == postId).ToList();
        }

        public CommentDto Add(CommentDto comment)
        {
            _comments.Add(comment);
            return comment;
        }
    }
}