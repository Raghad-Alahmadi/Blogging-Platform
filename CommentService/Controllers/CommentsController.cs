using Blogging_Platform.DTO;
using CommentService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        private readonly IPostValidationService _postValidation;

        public CommentsController(
            ICommentRepository repository,
            IPostValidationService postValidation)
        {
            _repository = repository;
            _postValidation = postValidation;
        }

        [HttpGet("{postId}")]
        public IActionResult GetByPostId(Guid postId)
        {
            return Ok(_repository.GetByPostId(postId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentDto comment)
        {
            if (string.IsNullOrEmpty(comment.Author) || string.IsNullOrEmpty(comment.Text))
                return BadRequest("Author and Text are required");

            // Validate that the post exists
            var postExists = await _postValidation.ValidatePostExistsAsync(comment.PostId);
            if (!postExists)
                return BadRequest($"Post with ID {comment.PostId} does not exist");

            var addedComment = _repository.Add(comment);
            return CreatedAtAction(nameof(GetByPostId), new { postId = comment.PostId }, addedComment);
        }
    }
}
