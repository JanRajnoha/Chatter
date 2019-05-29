using Chatter.BL.DTO.Post;

namespace Chatter.BL.Messages.PostMessages
{
    public class UpdatePostMessage
    {
        public PostDetailModelDTO UpdatedPost { get; set; }

        public UpdatePostMessage(PostDetailModelDTO updatedPost)
        {
            UpdatedPost = updatedPost;
        }
    }
}
