using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public enum UserReaction
    {
        None = 1,
        Liked = 2,
        Disliked = 3
    }


    public class CommentDto
    {
        public CommentDto()
        {

        }

        public CommentDto(Comment comment, Dictionary<string, string> userPhotoMap)
        {
            InitProperties(comment, userPhotoMap);
            IsDeleted = comment.IsDeleted;

            foreach (Answer answer in comment.Answers)
            {
                Answers.Add(new CommentDto(answer, userPhotoMap));
            }
        }

        private void InitProperties(Comment comment, Dictionary<string, string> userPhotoMap)
        {
            Id = comment.Id;
            UserName = comment.UserName;
            Text = comment.Text;
            Likes = comment.WhoLiked.Length;
            Dislikes = comment.WhoDisliked.Length;
            Created = comment.Created;
            WhoLiked = comment.WhoLiked;
            WhoDisliked = comment.WhoDisliked;
            UserPhoto = userPhotoMap[comment.UserId];
            PostId = comment.PostId;
        }

        public CommentDto(Answer answer, Dictionary<string, string> userPhotoMap)
        {
            Id = answer.Id;
            ParentCommentId = answer.ParentCommentId;
            UserName = answer.UserName;
            Text = answer.Text;
            Likes = answer.WhoLiked.Length;
            Dislikes = answer.WhoDisliked.Length;
            Created = answer.Created;
            WhoLiked = answer.WhoLiked;
            WhoDisliked = answer.WhoDisliked;
            UserPhoto = userPhotoMap[answer.UserId];
            PostId = answer.PostId;
        }


        public string Id { get; set; }
        public string ParentCommentId { get; set; }
        public string Text { get; set; }

        public string PostId { get; set; }

        public string UserName { get; set; }

        public DateTime Created { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public string[] WhoLiked { get; set; }
        public string[] WhoDisliked { get; set; }

        public string UserPhoto { get; set; }

        public List<CommentDto> Answers { get; set; } = new List<CommentDto>();

        public UserReaction UserReaction { get; set; }

        public bool IsDeleted { get; set; }
    }
}
