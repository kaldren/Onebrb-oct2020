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

        // Misc
        public string BtnLikeCss { get; set; } = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnSuccess}";
        public string BtnDeleteCss { get; set; } = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnDanger}";
        public string BtnEditCss { get; set; } = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnInfo}";
        public bool IsLikeBtnEnabled { get; set; }
    }
}
