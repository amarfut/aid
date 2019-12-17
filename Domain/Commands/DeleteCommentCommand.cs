using System;

namespace Domain.Commands
{
    public class DeleteCommentCommand : IDomainCommand
    {
        public bool TopLevel { get; set; }

        public string CommentId { get; set; }

        public string PostUrl { get; set; }
    }
}
