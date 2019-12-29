using DataAccess;
using Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.DTOs;
using Services.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Services.QueryHandlers
{
    public class GetPostPreviewsQueryHandler : IQueryHandler<GetPostPreviewQuery, List<PostPreviewDto>>
    {
        private DatabaseContext _db = new DatabaseContext();

        public async Task<List<PostPreviewDto>> HandleAsync(GetPostPreviewQuery query)
        {
            var postsPreviews = await _db.Posts.AsQueryable()
                .OrderByDescending(x => x.Created)
                .Skip(query.Skip)
                .Take(query.Take).ToListAsync();

            return postsPreviews.Select(p => new PostPreviewDto(p)).ToList();
        }
    }
}
