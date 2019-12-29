using DataAccess;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class PostDto
    {
        public PostDto(string id)
        {
            Id = id;
        }

        public PostDto(Post post, List<Comment> comments, Dictionary<string, string> userPhotoMap, PostTag[] tags)
        {
            Id = post.Id;
            Title = post.Title;
            Url = post.Url;
            Text = post.Text;
            CommentsCount = post.CommentsCount;
            Views = post.Views;
            Likes = post.WhoLiked.Length;
            Dislikes = post.WhoDisliked.Length;
            Type = (PostType)post.Type;
            WhoLiked = post.WhoLiked;
            WhoDisliked = post.WhoDisliked;
            Created = post.Created;
            Description = post.Description;
            Similar = post.Similar;
            Tags = tags;

            foreach (var comment in comments)
            {
                Comments.Add(new CommentDto(comment, userPhotoMap));
            }
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        
        public int CommentsCount { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public PostTag[] Tags { get; set; }

        public string[] WhoLiked { get; set; }
        public string[] WhoDisliked { get; set; }
        public SimilarPost[] Similar { get; set; }


        public DateTime Created { get; set; }

        public string ViewsFormatted
        {
            get
            {
                return Views > 1000 ? Math.Floor((double)(Views / 1000)).ToString() + "k" : Views.ToString();
            }
        }

        public PostType Type { get; set; }

        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
