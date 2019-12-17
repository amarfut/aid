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
using Services.Utils;

namespace Services.QueryHandlers
{
    public class GetTopCommentsQueryHandler : IQueryHandler<GetTopCommentsQuery, List<CommentPreviewDto>>
    {
        private DatabaseContext _db = new DatabaseContext();

        public async Task<List<CommentPreviewDto>> HandleAsync(GetTopCommentsQuery query)
        {
            var comments = await _db.LatestComments.AsQueryable()
                .OrderByDescending(c => c.Created)
                .Take(query.Number).ToListAsync();

            var result = comments.Select(c => new CommentPreviewDto()
            {
                CommentId = c.CommentId,
                Text = c.Text,
                UserId = c.UserId,
                PostTitle = c.PostTitle,
                PostUrl = c.PostUrl,
                UserName = c.UserName,
                UserPhotoUrl = c.UserPhotoUrl
            }).ToList();

            return result;
        }
    }
}




////TODO: get answers as well

//var postIds = comments.Select(c => ObjectId.Parse(c.PostId));
//var userIds = comments.Select(c => ObjectId.Parse(c.UserId));

//var postsFilter = Builders<Post>.Filter.In(x => x.InternalId, postIds);
//var usersFilter = Builders<User>.Filter.In(x => x.InternalId, userIds);
//var posts = await _db.Posts.Find(postsFilter).ToListAsync();
//var users = await _db.Users.Find(usersFilter).ToListAsync();

//var postMap = posts.ToDictionary(p => p.InternalId.ToString(), p => new Tuple<string, string>(p.Title, p.Url));
//var usersMap = users.ToDictionary(p => p.Id, p => p.PhotoUrl);

//var result = comments.Select(c => new CommentPreviewDto()
//{
//    CommentId = c.Id,
//    Text = c.Text,
//    UserId = c.UserId,
//    PostTitle = postMap[c.PostId.ToString()].Item1,
//    PostUrl = postMap[c.PostId.ToString()].Item2,
//    UserName = c.UserName,
//    UserPhotoUrl = usersMap[c.UserId]
//}).ToList();