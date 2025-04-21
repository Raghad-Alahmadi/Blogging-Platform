namespace CommentService.Services
{
    public interface IPostValidationService
    {
        Task<bool> ValidatePostExistsAsync(Guid postId);
    }
}