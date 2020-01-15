using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DataAccess;
using Domain;
using MongoDB.Driver.Linq;
using Services.QueryHandlers;
using Domain.Entities;
using Services.DTOs;
using Services.Queries;
using Services.InternalCommandHandlers;
using Domain.Commands;
using Services.CommandHandlers;
using Services.Utils;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using DataAccess;
using Domain.Commands;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Services.Utils;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace Services.AppServices
{
    public class PostService
    {

        private GetPostQueryHandler _getPostQueryHandler = new GetPostQueryHandler();
        private GetRandomPostPreviewsQueryHandler _getRandomPostPreviewsQueryHandler = new GetRandomPostPreviewsQueryHandler();
        private SetPostReactionCommandHandler _setPostReactionCommandHandler = new SetPostReactionCommandHandler();
        private GetPostPreviewsQueryHandler _getPostPreviewsQueryHandler = new GetPostPreviewsQueryHandler();
        private AddPostToBookmarksCommandHandler _addPostToBookmarksCommandHandler = new AddPostToBookmarksCommandHandler();
        


        public async Task<IEnumerable<PostPreviewDto>> GetSimilarPosts(PostType type = PostType.None)
        {
            int number = type == PostType.None ? 6 : 3;
            var posts = await _getRandomPostPreviewsQueryHandler.HandleAsync(new GetRandomPostPreviewsQuery(type, number));
            return posts;
        }

        private GetUserBookmarksQueryHandler _getUserBookmarksQueryHandler = new GetUserBookmarksQueryHandler();
        private DeleteCommentCommandHandler _deleteCommentCommandHandler = new DeleteCommentCommandHandler();


        public async Task<IEnumerable<PostPreviewDto>> GetPostPreviews(int skip, int take)
        {
            var posts = await _getPostPreviewsQueryHandler.HandleAsync(new GetPostPreviewQuery(skip, take));
            return posts;
        }

        public async Task<IEnumerable<PostPreviewDto>> GetRandomPostPreviews(PostType type, int number)
        {
            var posts = await _getRandomPostPreviewsQueryHandler.HandleAsync(new GetRandomPostPreviewsQuery(type, number));
            return posts;
        }

        public async Task<PostDto> GetPost(string url)
        {
            var post = await _getPostQueryHandler.HandleAsync(new GetPostQuery(url));
            return post;
        }


        public async Task<List<PostPreviewDto>> GetPosts(PostType type)
        {
            DatabaseContext db = new DatabaseContext();

            var results = await db.Posts.AsQueryable()
                .Where(x => x.Type == (int)type)
                .OrderByDescending(x => x.Created).ToListAsync();

            return results.Select(p => new PostPreviewDto(p)).ToList();
        }

        public async Task<Result<ReactionDto>> SetPostReaction(PostReactionDto dto, string userId)
        {
            var result = await _setPostReactionCommandHandler
                .HandleAsync(new SetPostReactionCommand(dto.PostId, userId, dto.Liked));

            return result;
        }

        public async Task<Result> AddPostToBookMarksAsync(string postId, string userId)
        {
            return await _addPostToBookmarksCommandHandler
                .HandleAsync(new AddPostToBookmarksCommand()
                {
                    PostId = postId,
                    UserId = userId,
                    Created = DateTime.UtcNow
                });
        }

        public async Task<Result> DeleteCommentAsync(DeleteCommentDto comment, string userId)
        {
            return await _deleteCommentCommandHandler.HandleAsync(new DeleteCommentCommand()
            {
                CommentId = comment.CommentId,
                TopLevel = comment.TopLevel,
                PostUrl = comment.PostUrl
            });
        }
            

        public async Task<List<PostPreviewDto>> GetUserBookmarksAsync(string userId)
        {
            return await _getUserBookmarksQueryHandler.HandleAsync(new GetUserBookmarksQuery(userId));
        }

        public async Task<List<PostPreviewDto>> GetTopPosts()
        {
            DatabaseContext db = new DatabaseContext();
            var config = await db.Config.AsQueryable().ToListAsync();

            if (config != null & config.Count > 0)
            {
                string[] popularPosts = config[0].Popular;
                var posts = await db.Posts.AsQueryable().Where(p => popularPosts.Contains(p.Url)).ToListAsync();

                if (posts.Count != popularPosts.Length)
                {
                    return posts.Select(p => new PostPreviewDto(p)).ToList();
                }

                List<Post> sorted = new List<Post>();
                for (int i = 0; i < posts.Count; i++)
                {
                    var postToAdd = posts.FirstOrDefault(x => x.Url == popularPosts[i]);
                    if(postToAdd != null) sorted.Add(postToAdd);
                }

                return sorted.Select(p => new PostPreviewDto(p)).ToList();
            }

            return new List<PostPreviewDto>();
        }
    }
}
