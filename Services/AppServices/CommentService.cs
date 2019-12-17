using DataAccess;
using Domain.Commands;
using Services.CommandHandlers;
using Services.DTOs;
using Services.Queries;
using Services.QueryHandlers;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.AppServices
{
    public class CommentService
    {
        private AddCommentHandler _addCommentHandler = new AddCommentHandler();

        private AnswerCommentHandler _answerCommentHandler = new AnswerCommentHandler();

        private SetCommentReactionHandler _setCommentReactionHandler = new SetCommentReactionHandler();

        private GetTopCommentsQueryHandler _getTopCommentsQueryHandler = new GetTopCommentsQueryHandler();

        private GetUserCommentsQueryHandler _getUserCommentsQueryHandler = new GetUserCommentsQueryHandler();




        public async Task<Result> AddCommentAsync(AddCommentDto dto, string userId, string userName, string userPhotoUrl)
        {
            if (string.IsNullOrEmpty(dto.Text))
            {
                return Result.Fail("Comment is empty.");
            }

            return await _addCommentHandler.HandleAsync(new AddCommentCommand()
            {
                Created = DateTime.UtcNow,
                UserName = userName,
                PostId = dto.PostId,
                UserId = userId,
                Text = dto.Text,
                ParentCommentId = dto.ParentCommentId,
                UserPhotoUrl = userPhotoUrl
            });
        }

        public async Task<Result> AnswerCommentAsync(AnswerCommentDto dto, string userId, string userName)
        {
            return await _answerCommentHandler.HandleAsync(
                new AnswerCommentCommand(dto.ParentCommentId, dto.Text, userId, userName, DateTime.UtcNow)
            );
        }

        public async Task<Result<ReactionDto>> SetCommentReactionAsync(CommentReactionDto dto, string userId)
        {
            return await _setCommentReactionHandler.HandleAsync(
                new SetCommentReactionCommand(dto.CommentId, dto.ParentCommentId, dto.Liked, userId)
            );
        }

        public async Task<List<CommentPreviewDto>> GetTopCommentPreviews()
        {
            return await _getTopCommentsQueryHandler.HandleAsync(new GetTopCommentsQuery(4));
        }

        public async Task<List<CommentPreviewDto>> GetUserCommentsAsync(string userId)
        {
            return await _getUserCommentsQueryHandler.HandleAsync(new GetUserCommentsQuery(userId));
        }
    }
}
