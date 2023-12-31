﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Web.Brokers.Apis;
using Blog.Web.Brokers.Loggings;
using Blog.Web.Models.Posts;

namespace Blog.Web.Services.Foundations.Posts
{
    public partial class PostService : IPostService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public PostService(IApiBroker apiBroker, ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Post> AddPostAsync(Post post) =>
            TryCatch(async () =>
            {
                ValidatePostOnAdd(post);
                return await this.apiBroker.PostPostAsync(post);
            });

        public ValueTask<List<Post>> RetrieveAllPostsAsync() =>
            TryCatch(async () =>
            {
                return await this.apiBroker.GetAllPostsAsync();
            });

        public ValueTask<Post> RemovePostByIdAsync(Guid postId) =>
            TryCatch(async () =>
            {
                ValidatePostId(postId);
                return await this.apiBroker.DeletePostByIdAsync(postId);
            });
    }
}
