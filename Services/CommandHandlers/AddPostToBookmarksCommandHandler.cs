using DataAccess;
using Domain.Commands;
using Domain.Entities;
using Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Utils;
using System;
using System.Threading.Tasks;

namespace Services.CommandHandlers
{
    public class AddPostToBookmarksCommandHandler : ICommandHandler<AddPostToBookmarksCommand, Result>
    {
        protected DatabaseContext _db = new DatabaseContext();

        public async Task<Result> HandleAsync(AddPostToBookmarksCommand command)
        {
            var bookmark = new Bookmark(command.PostId, command.Created);

            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Where(c => c.InternalId == ObjectId.Parse(command.UserId))
            );

            var update = Builders<User>.Update.Push("Bookmarks", bookmark);

            await _db.Users.FindOneAndUpdateAsync(filter, update);
            return Result.Ok();
        }
    }
}
