using System;
using Blogging_Platform.DTO;

namespace PostService.Services
{
    public class InMemoryPostRepository : IPostRepository
    {
        private readonly Dictionary<Guid, PostDto> _posts = new();

        public IEnumerable<PostDto> GetAll()
        {
            return _posts.Values;
        }

        public PostDto GetById(Guid id)
        {
            return _posts.TryGetValue(id, out var post) ? post : null;
        }

        public PostDto Create(string title, string content)
        {
            var id = Guid.NewGuid();
            var post = new PostDto(id, title, content);
            _posts[id] = post;
            return post;
        }

        public bool Exists(Guid id)
        {
            return _posts.ContainsKey(id);
        }
    }
}
