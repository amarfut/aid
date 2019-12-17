using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Services.DTOs
{
    public class PostPreviewDto
    {
        public PostPreviewDto(Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Url = post.Url;

            string textPreview = Regex.Replace(post.Text, "<.*?>", string.Empty);
            TextPreview = textPreview.Length > 100 ? textPreview.Substring(0, 100) + "..." : textPreview; 

            ViewsCount = post.Views;
            CommentsCount = post.CommentsCount;
            LikesCount = post.WhoLiked.Length;
            DislikesCount = post.WhoDisliked.Length;
            Created = post.Created;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string TextPreview { get; set; }
        public int ViewsCount { get; set; }
        public int CommentsCount { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public DateTime Created { get; set; }
    }
}
