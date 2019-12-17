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
    public class GetRandomPostPreviewsQueryHandler : IQueryHandler<GetRandomPostPreviewsQuery, List<PostPreviewDto>>
    {
        private readonly DatabaseContext _db = new DatabaseContext();

        public async Task<List<PostPreviewDto>> HandleAsync(GetRandomPostPreviewsQuery query)
        {
            List<Post> postsPreviewsForType = null;
            if (query.PostType == Domain.PostType.None)
            {
                postsPreviewsForType = await _db.Posts.AsQueryable().Sample(query.Number).ToListAsync();
            }
            else
            {
                postsPreviewsForType = await _db.Posts.AsQueryable()
                        .Where(x => x.Type == (int)query.PostType)
                        .Sample(query.Number).ToListAsync();
            }
        
            return postsPreviewsForType.Select(p => new PostPreviewDto(p)).ToList();
        }
    }
}
