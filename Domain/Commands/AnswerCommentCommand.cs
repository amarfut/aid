using System;

namespace Domain.Commands
{
    public class AnswerCommentCommand : IDomainCommand
    {
        public AnswerCommentCommand(
            string parentCommentId, 
            string text, 
            string userId, 
            string userName, 
            DateTime created)
        {
            ParentCommentId = parentCommentId;
            UserId = userId;
            UserName = userName;
            Text = text;
            Created = created;
        }

        public string ParentCommentId { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime Created { get; set; }
    }
}
