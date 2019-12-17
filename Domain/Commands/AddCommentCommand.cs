using System;

namespace Domain.Commands
{
    public class AddCommentCommand : IDomainCommand
    {
        public string Text { get; set; }

        public string PostId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime Created { get; set; }

        public string ParentCommentId { get; set; }

        public string UserPhotoUrl { get; set; }
    }
}
