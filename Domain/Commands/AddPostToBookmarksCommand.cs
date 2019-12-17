using System;

namespace Domain.Commands
{
    public class AddPostToBookmarksCommand : IDomainCommand
    {
        public string PostId { get; set; }

        public string UserId { get; set; }

        public DateTime Created { get; set; }
    }
}
