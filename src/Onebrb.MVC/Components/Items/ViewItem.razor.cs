using Microsoft.AspNetCore.Components;
using Onebrb.MVC.Constants;
using Onebrb.MVC.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.MVC.Components.Items
{
    public partial class ViewItem : ComponentBase
    {
        // Parameters
        [Parameter]
        public ItemViewModel Item { get; set; }

        public string BtnLikeCss { get; set; } = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnSuccess}";
        public bool IsLikeBtnEnabled { get; set; }
        public string LikeBtnText { get; set; } = "Like";
    }
}
