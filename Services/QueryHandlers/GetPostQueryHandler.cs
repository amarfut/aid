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
    public class GetPostQueryHandler : IQueryHandler<GetPostQuery, PostDto>
    {
        private DatabaseContext _db = new DatabaseContext();

        public async Task<PostDto> HandleAsync(GetPostQuery query)
        {
            var post = await _db.Posts.Find(x => x.Url == query.Url).FirstOrDefaultAsync();

            if (post == null) return null;

            var comments = await
                _db.Comments.AsQueryable()
                .Where(c => c.PostId == post.Id)
                .OrderByDescending(c => c.Created)
                .ToListAsync();

            var parentCommentIds = comments.Select(c => c.Id).ToList();
            var answers = await _db.Answers.AsQueryable()
                .Where(a => parentCommentIds.Contains(a.ParentCommentId))
                .ToListAsync();

            foreach (var comment in comments)
            {
                comment.Answers = answers.Where(a => a.ParentCommentId == comment.Id).OrderBy(c => c.Created).ToArray();
            }

            var userIds = comments.Select(c => ObjectId.Parse(c.UserId)).ToList();
            userIds.AddRange(answers.Select(a => ObjectId.Parse(a.UserId)).ToList());

            var filter = Builders<User>.Filter.In(x => x.InternalId, userIds);
            var userPhotos = await _db.Users.Find(filter).Project(x => new { x.Id, x.PhotoUrl }).ToListAsync();
            var userPhotoMap = userPhotos.ToDictionary(x => x.Id, x => x.PhotoUrl);

            var tags = await _db.Tags.Find(_ => true).ToListAsync() ?? new List<PostTag>();
            var postTags = tags.Where(t => post.Tags.Contains(t.Name)).ToArray();
            return new PostDto(post, comments, userPhotoMap, postTags);
        }
    }
}
