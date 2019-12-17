using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DataAccess;
using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services;
using Services.AppServices;
using Services.DTOs;
using Services.InternalCommandHandlers;
using Services.Utils;
using Web.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Web.Controllers
{
    public class UserCookieData
    {
        public List<string> ViewedPostIds { get; set; } = new List<string>();

        public DateTime LastAccessed { get; set; } = DateTime.UtcNow;
    }


    public class HomeController : BaseController
    {
        private PostService _postService = new PostService();

        private CryptoManager _crypto = new CryptoManager();

        private const string userCookieId = "_yId";

        public IActionResult Index()
        {
            ViewBag.Title = "YouIT - программирование и карьера в IT";
            return View();
        }

        public async Task<IEnumerable<PostPreviewDto>> LoadPosts(int skip)
        {
            return await _postService.GetPostPreviews(skip);
        }

        public async Task<IEnumerable<PostPreviewDto>> LoadSimilarPosts(PostType type)
        {
            return await _postService.GetSimilarPosts(type);
        }

        [Route("status/{code}")]
        public IActionResult Status(int code)
        {
            if (code == 404)
            {
                return View("PageNotFound");
            }
            else if (code == 500)
            {
                return View("Error");
            }

            return View("Error");
        }

        

        [HttpGet("post/{url?}/", Name = "Post")]
        public async Task<IActionResult> Post(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return await RedirectToMap();
            }

            var service = new PostService();
            var post = await service.GetPost(url);

            if (post == null)
            {
                return await RedirectToMap();
            }

            UserCookieData data = Deserialize(_crypto.Decrypt(HttpContext.Request.Cookies[userCookieId]));
            if ((DateTime.UtcNow - data.LastAccessed).TotalHours > 12) data = new UserCookieData();

            if (!data.ViewedPostIds.Contains(post.Id))
            {
                data.ViewedPostIds.Add(post.Id);

                string encryptedCookie = _crypto.Encrypt(GetJson(data.ViewedPostIds));

                HttpContext.Response.Cookies.Delete(userCookieId);
                HttpContext.Response.Cookies.Append(userCookieId, encryptedCookie, new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddHours(24)
                });
                new IncrementPostViewCount().HandleAsync(post.Id);
            }

            ViewBag.Title = post.Title + " - YouIT";
            ViewBag.MetaDescription = post.Description;

            return View(post);
        }

        private async Task<IActionResult> RedirectToMap()
        {
            var model = new PostMapViewModel
            {
                Programming = await _postService.GetPosts(PostType.Programming),
                Career = await _postService.GetPosts(PostType.Career)
            };

            return View("Map", model);
        }

        private UserCookieData Deserialize(string postViewCookie)
        {
            if (string.IsNullOrEmpty(postViewCookie))
            {
                return new UserCookieData();
            }
            return JsonConvert.DeserializeObject<UserCookieData>(postViewCookie);
        }

        private string GetJson(List<string> postIds)
        {
            var data = new UserCookieData
            {
                ViewedPostIds = postIds,
                LastAccessed = DateTime.UtcNow
            };

            return JsonConvert.SerializeObject(data);
        }

        [HttpGet("error/")]
        public IActionResult Error()
        {
            return View();
        }

        public RedirectResult Search(string term)
        {
            string sitesearch = "youit.pro";
            string url = $"https://www.google.com/search?q={term}&sitesearch={sitesearch}";
            return Redirect(new Uri(url).AbsoluteUri);
        }

        public async Task<IActionResult> SetPostReaction([FromBody] PostReactionDto dto)
        {
            //todo: move to custom attribute
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            Result<ReactionDto> result = await _postService.SetPostReaction(dto, UserId);
            return FromResult(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Authorize()
        {
            return View();
        }
    }
}


            //TODO: introduce UI services layer and move it to there and refactor

            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
            //    string userId = UserId;

            //    foreach (var comment in post.Comments)
            //    {
            //        if (comment.WhoLiked.Contains(userId))
            //            comment.UserReaction = UserReaction.Liked;
            //        else if (comment.WhoDisliked.Contains(userId))
            //            comment.UserReaction = UserReaction.Disliked;
            //        else comment.UserReaction = UserReaction.None;

            //        foreach (var answer in comment.Answers)
            //        {
            //            if (answer.WhoLiked.Contains(userId))
            //                answer.UserReaction = UserReaction.Liked;
            //            else if (answer.WhoDisliked.Contains(userId))
            //                answer.UserReaction = UserReaction.Disliked;
            //            else answer.UserReaction = UserReaction.None;
            //        }
            //    }
            //}
