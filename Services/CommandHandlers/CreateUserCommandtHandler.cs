using DataAccess;
using Domain.Commands;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.DTOs;
using Services.Utils;
using System.Threading.Tasks;

namespace Services.CommandHandlers
{
    public class CreateUserCommandtHandler : ICommandHandler<CreateUserCommand, Result<UserDto>>
    {
        protected DatabaseContext _db = new DatabaseContext();

        public async Task<Result<UserDto>> HandleAsync(CreateUserCommand command)
        {
            var user = await _db.Users.Find(u => u.ExternalId == command.ExternalId).FirstOrDefaultAsync();

            if (user != null)
            {
                var update = Builders<User>.Update.Set("PhotoUrl", command.PhotoUrl);
                await _db.Users.UpdateOneAsync(u => u.InternalId == user.InternalId, update);
                return Result.Ok(new UserDto() { Id = user.Id, Name = user.Name });
            }
            else
            {
                var userObjectId = ObjectId.GenerateNewId();
                await _db.Users.InsertOneAsync(new User
                {
                    ExternalId = command.ExternalId,
                    Name = command.Name,
                    Provider = command.Provider,
                    PhotoUrl = command.PhotoUrl,
                    InternalId = userObjectId
                });
                return Result.Ok(new UserDto() { Id = userObjectId.ToString(), Name = command.Name });
            }
        }
    }
}
