﻿using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Blog.Web.Views.Bases
{
    public partial class ValidationSummaryBase : ComponentBase
    {
        [Parameter]
        public IDictionary ValidationData { get; set; }

        [Parameter]
        public string Message { get; set; }

        [Parameter]
        public string Key { get; set; }

        [Parameter]
        public string Color { get; set; }

        public IEnumerable<string> Errors
        {
            get => this.ValidationData?[Key] as IEnumerable<string>;
            set => Errors = value;
        }
    }
}
