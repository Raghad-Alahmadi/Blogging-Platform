using Blogging_Platform.DTO;

namespace PostService.Services
{
    public interface IPostRepository
    {
        IEnumerable<PostDto> GetAll();
        PostDto GetById(Guid id);
        PostDto Create(string title, string content);
        bool Exists(Guid id);
    }
}
