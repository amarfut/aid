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
    public class GetLikedPostsQueryHandler : IQueryHandler<GetLikedPostsQuery, List<PostPreviewDto>>
    {
        private DatabaseContext _db = new DatabaseContext();

        public async Task<List<PostPreviewDto>> HandleAsync(GetLikedPostsQuery query)
        {
            IEnumerable<string> userId = new List<string>() { query.UserId };
            var posts = await _db.Posts.AsQueryable().Where(x => x.WhoLiked.Contains(query.UserId)).ToListAsync();
            return posts.Select(l => new PostPreviewDto(l)).ToList();
        }
    }
}
