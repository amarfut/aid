using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Queries;
using Services.DTOs;

namespace Services.QueryHandlers
{
    public class GetUserBookmarksQueryHandler : IQueryHandler<GetUserBookmarksQuery, List<PostPreviewDto>>
    {
        private DatabaseContext _db = new DatabaseContext();

        public async Task<List<PostPreviewDto>> HandleAsync(GetUserBookmarksQuery query)
        {
            User user = await _db.Users
                .Find(x => x.InternalId == ObjectId.Parse(query.UserId))
                .FirstOrDefaultAsync();

            var bookmarks = user.Bookmarks.Select(b => ObjectId.Parse(b.PostId));

            var filter = Builders<Post>.Filter.In(x => x.InternalId, bookmarks);

            var posts = await _db.Posts.Find(filter).ToListAsync();

            return posts
                .Select(p => new PostPreviewDto(p))
                .OrderByDescending(p => p.Created)
                .ToList();
        }
    }
}
