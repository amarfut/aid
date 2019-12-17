using DataAccess;
using Domain.Commands;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Utils;
using System.Threading.Tasks;

namespace Services.CommandHandlers
{
    public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand, Result>
    {
        protected DatabaseContext _db = new DatabaseContext();

        public async Task<Result> HandleAsync(UpdateProfileCommand command)
        {
            var update = Builders<User>.Update.Set(x => x.Name, command.Username);
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Where(c => c.InternalId == ObjectId.Parse(command.UserId))
            );

            await _db.Users.FindOneAndUpdateAsync(filter, update);

            return Result.Ok();
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