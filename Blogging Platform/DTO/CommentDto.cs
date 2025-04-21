using System;

namespace Blogging_Platform.DTO
{
    public record CommentDto(Guid PostId, string Author, string Text);
}
