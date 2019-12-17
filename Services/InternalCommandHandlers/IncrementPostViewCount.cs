using DataAccess;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.InternalCommandHandlers
{
    public class IncrementPostViewCount
    {
        private DatabaseContext _db = new DatabaseContext();

        public async Task HandleAsync(string postId)
        {
            var increment = Builders<Post>.Update.Inc(p => p.Views, 1);
            await _db.Posts.UpdateOneAsync(p => p.InternalId == ObjectId.Parse(postId), increment);
        }
    }
}
