﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Web.Brokers.Loggings;
using Blog.Web.Models.Posts;
using Blog.Web.Models.PostViews;
using Blog.Web.Services.Foundations.Posts;

namespace Blog.Web.Services.Views.PostViews
{
    public partial class PostViewService : IPostViewService
    {
        private readonly IPostService postService;
        private readonly ILoggingBroker loggingBroker;

        public PostViewService(IPostService postService, ILoggingBroker loggingBroker)
        {
            this.postService = postService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<List<PostView>> RetrieveAllPostViewsAsync()
        {
            List<Post> retrievedPosts = 
                await this.postService.RetrieveAllPostsAsync();

            return retrievedPosts.Select(AsPostView).ToList();
        }

        public ValueTask<PostView> RemovePostViewByIdAsync(Guid postViewId) =>
            TryCatch(async () =>
            {
                ValidatePostViewId(postViewId);

                Post deletedPost =  await this.postService.RemovePostByIdAsync(postViewId);

                return MapToPostView(deletedPost);
            });

        private static Func<Post, PostView> AsPostView => 
            post => MapToPostView(post);

        private static PostView MapToPostView(Post post)
        {
            return new PostView
            {
                Id = post.Id,
                Title = post.Title,
                SubTitle = post.SubTitle,
                Author = post.Author,
                Content = post.Content,
                CreatedDate = post.CreatedDate,
                UpdatedDate = post.UpdatedDate
            };
        }

    }
}
