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
    public class GetUserCommentsQueryHandler : IQueryHandler<GetUserCommentsQuery, List<CommentPreviewDto>>
    {
        private DatabaseContext _db = new DatabaseContext();

        public async Task<List<CommentPreviewDto>> HandleAsync(GetUserCommentsQuery query)
        {
            var comments = await _db.Comments.AsQueryable().Where(c => c.UserId == query.UserId && !c.IsDeleted).ToListAsync();
            var answers = await _db.Answers.AsQueryable().Where(c => c.UserId == query.UserId).ToListAsync();

            var postIds = comments.Select(c => ObjectId.Parse(c.PostId)).ToList();
            postIds.AddRange(answers.Select(a => ObjectId.Parse(a.PostId)));

            var filter = Builders<Post>.Filter.In(x => x.InternalId, postIds);
            var posts = await _db.Posts.Find(filter).ToListAsync();

            var postMap = posts.ToDictionary(p => p.InternalId.ToString(), p => new Tuple<string, string>(p.Title, p.Url));

            var result = comments.Select(c => new CommentPreviewDto()
            {
                CommentId = c.Id,
                Text = c.Text,
                UserId = c.UserId,
                PostTitle = postMap[c.PostId.ToString()].Item1,
                PostUrl = postMap[c.PostId.ToString()].Item2,
                UserName = c.UserName,
                Created = c.Created,
                Likes = c.WhoLiked.Length,
                Dislikes = c.WhoDisliked.Length
            }).ToList();


            result.AddRange(answers.Select(c => new CommentPreviewDto()
            {
                CommentId = c.Id,
                Text = c.Text,
                UserId = c.UserId,
                PostTitle = postMap[c.PostId.ToString()].Item1,
                PostUrl = postMap[c.PostId.ToString()].Item2,
                UserName = c.UserName,
                Created = c.Created,
                ParentCommentId = c.ParentCommentId,
                Likes = c.WhoLiked.Length,
                Dislikes = c.WhoDisliked.Length
            }));

            return result;
        }
    }
}
