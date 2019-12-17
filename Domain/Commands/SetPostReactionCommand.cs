using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class SetPostReactionCommand : IDomainCommand
    {
        public SetPostReactionCommand(string postId, string userId, bool liked)
        {
            PostId = postId;
            UserId = userId;
            Liked = liked;
        }

        public string PostId { get; set; }
        public string UserId { get; set; }
        public bool Liked { get; set; }
    }
}
