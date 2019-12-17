using DataAccess;
using Domain.Commands;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Utils;
using System.Threading.Tasks;

namespace Services.CommandHandlers
{
    public class AnswerCommentHandler : ICommandHandler<AnswerCommentCommand, Result>
    {
        protected DatabaseContext _db = new DatabaseContext();

        public async Task<Result> HandleAsync(AnswerCommentCommand command)
        {
            
            return Result.Ok();
        }
    }
}
