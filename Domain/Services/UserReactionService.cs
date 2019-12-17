using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domain.Services
{
    public enum Reaction
    {
        None = 1,
        Liked = 2,
        Disliked = 3
    }

    public class UserReaction
    {
        public string UserId { get; set; }

        public bool Liked { get; set; }

        public string[] WhoLiked { get; set; }

        public string[] WhoDisliked { get; set; }

        public Reaction Reaction { get; set; }
    }

    public class UserReactionDomainService
    {
        public UserReaction GetUserReaction(string userId, bool liked, string[] whoLiked, string[] whoDisliked)
        {
            var userReaction = new UserReaction() { UserId = userId, Liked = liked };

            var userIds = new string[] { userId };
            if (liked)
            {
                userReaction.WhoDisliked = whoDisliked.Except(userIds).ToArray();

                if (whoLiked.Contains(userId))
                    userReaction.WhoLiked = whoLiked.Except(userIds).ToArray();
                else
                    userReaction.WhoLiked = whoLiked.Concat(userIds).ToArray();
            }
            else
            {
                userReaction.WhoLiked = whoLiked.Except(userIds).ToArray();

                if (whoDisliked.Contains(userId))
                    userReaction.WhoDisliked = whoDisliked.Except(userIds).ToArray();
                else
                    userReaction.WhoDisliked = whoDisliked.Concat(userIds).ToArray();
            }

            userReaction.Reaction = GetReactionType(userId, userReaction.WhoLiked, userReaction.WhoDisliked);

            return userReaction;
        }

        private Reaction GetReactionType(string userId, string[] whoLiked, string[] whoDisliked)
        {
            if (whoLiked.Contains(userId)) return Reaction.Liked;
            else if (whoDisliked.Contains(userId)) return Reaction.Disliked;
            else return Reaction.None;
        }
    }
}
