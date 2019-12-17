using DataAccess;
using Domain.Commands;
using Domain.Entities;
using Domain.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.DTOs;
using Services.Utils;
using System.Linq;
using System.Threading.Tasks;


namespace Services.CommandHandlers
{
    public class SetPostReactionCommandHandler : ICommandHandler<SetPostReactionCommand, Result<ReactionDto>>
    {
        private DatabaseContext _db = new DatabaseContext();

        private UserReactionDomainService _userReactionDS = new UserReactionDomainService();

        public async Task<Result<ReactionDto>> HandleAsync(SetPostReactionCommand command)
        {
            Post post = await _db.Posts.Find(p => p.InternalId == ObjectId.Parse(command.PostId)).FirstOrDefaultAsync();

            var reaction = _userReactionDS.GetUserReaction(command.UserId, command.Liked, post.WhoLiked, post.WhoDisliked);

            var update = Builders<Post>.Update.Set(x => x.WhoLiked, reaction.WhoLiked).Set(x => x.WhoDisliked, reaction.WhoDisliked);
            await _db.Posts.FindOneAndUpdateAsync(c => c.InternalId == ObjectId.Parse(command.PostId), update);

            return Result.Ok(new ReactionDto(reaction.WhoLiked.Length, reaction.WhoDisliked.Length, reaction.Reaction));
        }
    }
}
