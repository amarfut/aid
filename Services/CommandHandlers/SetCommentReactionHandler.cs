using DataAccess;
using Domain.Commands;
using Domain.Entities;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.DTOs;
using Services.Utils;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using System;
using Domain.Services;

namespace Services.CommandHandlers
{
    public class SetCommentReactionHandler : ICommandHandler<SetCommentReactionCommand, Result<ReactionDto>>
    {
        protected DatabaseContext _db = new DatabaseContext();

        private UserReactionDomainService _userReactionDS = new UserReactionDomainService();

        public async Task<Result<ReactionDto>> HandleAsync(SetCommentReactionCommand command)
        {
            if (string.IsNullOrEmpty(command.ParentCommandId))
            {
                Comment comment = await _db.Comments.AsQueryable().Where(c => c.InternalId == ObjectId.Parse(command.CommentId)).FirstOrDefaultAsync();
                if (comment == null) return Result.Fail<ReactionDto>("");
                var userReaction = _userReactionDS.GetUserReaction(command.UserId, command.Liked, comment.WhoLiked, comment.WhoDisliked);
                var update = Builders<Comment>.Update.Set(x => x.WhoLiked, userReaction.WhoLiked).Set(x => x.WhoDisliked, userReaction.WhoDisliked);
                await _db.Comments.FindOneAndUpdateAsync(c => c.InternalId == comment.InternalId, update);
                return Result.Ok(new ReactionDto(userReaction.WhoLiked.Length, userReaction.WhoDisliked.Length, userReaction.Reaction));
            }
            else
            {
                Answer answer = await _db.Answers.AsQueryable().Where(c => c.InternalId == ObjectId.Parse(command.CommentId)).FirstOrDefaultAsync();
                if (answer == null) return Result.Fail<ReactionDto>("");
                var userReaction = _userReactionDS.GetUserReaction(command.UserId, command.Liked, answer.WhoLiked, answer.WhoDisliked);
                var update = Builders<Answer>.Update.Set(x => x.WhoLiked, userReaction.WhoLiked).Set(x => x.WhoDisliked, userReaction.WhoDisliked);
                await _db.Answers.FindOneAndUpdateAsync(c => c.InternalId == answer.InternalId, update);
                return Result.Ok(new ReactionDto(userReaction.WhoLiked.Length, userReaction.WhoDisliked.Length, userReaction.Reaction));
            }

            //string rootCommentId = string.IsNullOrEmpty(command.ParentCommandId) ? command.CommentId : command.ParentCommandId;

            //Comment comment = await _db.Comments.AsQueryable()
            //    .Where(c => c.InternalId == ObjectId.Parse(rootCommentId)).FirstOrDefaultAsync();

            //if (comment == null) return Result.Fail<ReactionDto>("");

            //if (!string.IsNullOrEmpty(command.ParentCommandId))
            //{
            //    return await HandleCommentAnswer(command, comment);
            //}

            //var userReaction = _userReactionDS.GetUserReaction(command.UserId, command.Liked, comment.WhoLiked, comment.WhoDisliked);

            //var update = Builders<Comment>.Update.Set(x => x.WhoLiked, userReaction.WhoLiked).Set(x => x.WhoDisliked, userReaction.WhoDisliked);

            //await _db.Comments.FindOneAndUpdateAsync(c => c.InternalId == ObjectId.Parse(rootCommentId), update);

            //return Result.Ok(new ReactionDto() { Likes = userReaction.WhoLiked.Length, Dislikes = userReaction.WhoDisliked.Length });
        }

        //private async Task<Result<ReactionDto>> HandleCommentAnswer(SetCommentReactionCommand command, Comment comment)
        //{
        //    Answer answer = comment.Answers.FirstOrDefault(c => c.InternalId == ObjectId.Parse(command.CommentId));
        //    if (answer == null) return Result.Fail<ReactionDto>("");

        //    var userReaction = _userReactionDS.GetUserReaction(command.UserId, command.Liked, answer.WhoLiked, answer.WhoDisliked);

        //    var filter = Builders<Comment>.Filter;
        //    var commentAnswerFilter = filter.And(
        //          filter.Eq(x => x.InternalId, ObjectId.Parse(command.ParentCommandId)),
        //          filter.ElemMatch(x => x.Answers, c => c.InternalId == answer.InternalId));

        //    var update = Builders<Comment>.Update
        //        .Set("Answers.$.WhoLiked", userReaction.WhoLiked)
        //        .Set("Answers.$.WhoDisliked", userReaction.WhoDisliked);

        //    await _db.Comments.UpdateOneAsync(commentAnswerFilter, update);

        //    return Result.Ok(new ReactionDto() { Likes = userReaction.WhoLiked.Length, Dislikes = userReaction.WhoDisliked.Length });
        //}


    }
}