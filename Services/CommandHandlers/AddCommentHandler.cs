using DataAccess;
using Domain.Commands;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Utils;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Services.CommandHandlers
{
    public class AddCommentHandler : ICommandHandler<AddCommentCommand, Result>
    {
        protected DatabaseContext _db = new DatabaseContext();

        public async Task<Result> HandleAsync(AddCommentCommand command)
        {
            var commentId = ObjectId.GenerateNewId();
            if (string.IsNullOrEmpty(command.ParentCommentId))
            {
                var comment = new Comment()
                {
                    Text = command.Text,
                    Created = command.Created,
                    PostId = command.PostId,
                    UserId = command.UserId,
                    UserName = command.UserName,
                    InternalId = commentId
                };

                await _db.Comments.InsertOneAsync(comment);
            }
            else
            {
                var answer = new Answer()
                {
                    Text = command.Text,
                    Created = command.Created,
                    ParentCommentId = command.ParentCommentId,
                    UserId = command.UserId,
                    UserName = command.UserName,
                    PostId = command.PostId,
                    InternalId = commentId
                };

                await _db.Answers.InsertOneAsync(answer);
            }

            var increment = Builders<Post>.Update.Inc(p => p.CommentsCount, 1);
            await _db.Posts.UpdateOneAsync(p => p.InternalId == ObjectId.Parse(command.PostId), increment);

            InsertInLatestComments(commentId, command);

            return Result.Ok();
        }

        private async Task InsertInLatestComments(ObjectId commentId, AddCommentCommand command)
        {
            var post = await _db.Posts.Find(p => p.InternalId == ObjectId.Parse(command.PostId)).FirstOrDefaultAsync();
            if (post == null) return;

            var preview = new CommentPreview()
            {
                Created = DateTime.UtcNow,
                PostId = command.PostId,
                PostTitle = post.Title,
                UserPhotoUrl = command.UserPhotoUrl,
                PostUrl = post.Url,
                Text = command.Text,
                UserId = command.UserId,
                UserName = command.UserName,
                CommentId = commentId.ToString()
            };

            var comments = await _db.LatestComments.Find(_ => true).ToListAsync();
            comments.Add(preview);
            var sortedComments = comments.OrderByDescending(c => c.Created).Take(15);

            _db.LatestComments.DeleteMany(_ => true);

            await _db.LatestComments.InsertManyAsync(sortedComments);
        }
    }
}


//else
//{
//    var answer = new Answer() { Text = command.Text,Created = command.Created,ParentCommentId = command.ParentCommentId,UserId = command.UserId,UserName = command.UserName };
//    answer.InternalId = ObjectId.GenerateNewId();
//    var filter = Builders<Comment>.Filter.And(Builders<Comment>.Filter.Where(c => c.InternalId == ObjectId.Parse(command.ParentCommentId)));
//    var update = Builders<Comment>.Update.Push("Answers", answer);
//    await _db.Comments.FindOneAndUpdateAsync(filter, update);
//}