// PostService/Controllers/PostsController.cs
using Blogging_Platform.DTO;
using Microsoft.AspNetCore.Mvc;
using PostService.Services;

namespace PostService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repository;

        public PostsController(IPostRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var post = _repository.GetById(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePostRequest request)
        {
            if (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Content))
                return BadRequest("Title and Content are required");

            var post = _repository.Create(request.Title, request.Content);
            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
        }

        // Endpoint to validate if a post exists (for CommentService to use)
        [HttpGet("exists/{id}")]
        public IActionResult Exists(Guid id)
        {
            return Ok(_repository.Exists(id));
        }
    }

    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
