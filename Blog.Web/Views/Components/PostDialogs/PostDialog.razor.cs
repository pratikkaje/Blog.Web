﻿using System.Threading.Tasks;
using Blog.Web.Models.PostViews;
using Blog.Web.Models.Views.Components.PostDialogs;
using Blog.Web.Services.Views.PostViews;
using Blog.Web.Views.Bases;
using Microsoft.AspNetCore.Components;

namespace Blog.Web.Views.Components.PostDialogs
{
    public partial class PostDialog : ComponentBase
    {
        [Inject]
        public IPostViewService PostViewService { get; set; }
        public PostDialogComponentState State { get; set; }
        public TextAreaBase TextArea { get; set; }
        public DialogBase Dialog { get; set; }
        public bool IsVisible { get; set; }
        public PostView PostView { get; set; }

        protected override void OnInitialized()
        {
            this.State = PostDialogComponentState.Content;
            this.PostView = new PostView(); 
        }
            
        public void OpenDialog()
        {
            this.Dialog.Show();
            this.IsVisible = true;
        }

        public void CloseDialog()
        {
            this.Dialog.Hide();
            this.IsVisible = false;
        }

        public async ValueTask PostViewAsync()
        {
            //harcoded below properties till I figure out the Create Blog layout
            this.PostView.Title = "Title";
            this.PostView.SubTitle = "Subtitle";
            this.PostView.Author = "Author";
            
            this.TextArea.Disable();
            this.Dialog.DisableButton();

            await 
                this.PostViewService.AddPostViewAsync(this.PostView);

            CloseDialog();
        }
    }
}
