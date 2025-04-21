using Blogging_Platform.DTO;

namespace CommentService.Services
{
    public interface ICommentRepository
    {
        IEnumerable<CommentDto> GetByPostId(Guid postId);
        CommentDto Add(CommentDto comment);
    }
}