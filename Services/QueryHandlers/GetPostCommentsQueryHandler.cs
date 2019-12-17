using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Queries;

namespace Services.QueryHandlers
{
    public class GetPostCommentsQueryHandler : IQueryHandler<GetPostCommentsQuery, List<Comment>>
    {
        private DatabaseContext _db = new DatabaseContext();

        public async Task<List<Comment>> HandleAsync(GetPostCommentsQuery query)
        {
            return await _db.Comments.AsQueryable()
                      .Where(c => c.PostId == query.PostId).ToListAsync();
        }
    }
}
