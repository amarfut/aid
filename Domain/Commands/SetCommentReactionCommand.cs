using System;

namespace Domain.Commands
{
    public class SetCommentReactionCommand : IDomainCommand
    {
        public SetCommentReactionCommand(string commentId, string parentCommandId, bool liked, string userId)
        {
            CommentId = commentId;
            Liked = liked;
            UserId = userId;
            ParentCommandId = parentCommandId;
        }

        public string CommentId { get; set; }

        public bool Liked { get; set; }

        public string UserId { get; set; }
        public string ParentCommandId { get; set; }
    }
}
