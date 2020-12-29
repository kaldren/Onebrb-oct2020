using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Onebrb.MVC.Constants;
using Onebrb.MVC.Models;
using Onebrb.MVC.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Onebrb.MVC.Components.Items
{
    public partial class ViewItem : ComponentBase
    {
        // Parameters
        [Parameter]
        public ItemViewModel Item { get; set; }

        // Dependency Injection
        [Inject]
        public HttpClient HttpClient { get; set; }

        public Core.Models.User CurrentUser { get; set; }

        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        [Inject]
        UserManager<Core.Models.User> UserManager { get; set; }

        // Misc
        public string BtnLikeCss { get; set; } = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnSuccess}";
        public string BtnDeleteCss { get; set; } = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnDanger}";
        public string BtnEditCss { get; set; } = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnInfo}";
        public bool IsLikeBtnEnabled { get; set; }
        public bool IsDeleteBtnEnabled { get; set; }
        public bool IsEditBtnEnabled { get; set; }

        // Misc
        public bool IsFormEnabled { get; set; } = true;
        public string OnDeleteResult { get; set; }
        public string notifyBarClass { get; set; }
        public string OnebrbApiToken { get; set; }
        public string EditedItemUrl { get; set; }
        public bool IsItemDeleted { get; set; }
        public string BtnSubmitCss { get; set; } = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnSuccess}";

        protected override async Task OnInitializedAsync()
        {
            CurrentUser = await UserManager.GetUserAsync(HttpContextAccessor.HttpContext.User);

            if (CurrentUser.Id != Item.UserId)
            {
                return;
            }

            IsLikeBtnEnabled = true;
            IsDeleteBtnEnabled = true;
            IsEditBtnEnabled = true;
        }

        private async Task DeleteItem(long itemId)
        {
            string scheme = HttpContextAccessor.HttpContext.Request.Scheme;
            string host = HttpContextAccessor.HttpContext.Request.Host.Value;
            string pathBase = HttpContextAccessor.HttpContext.Request.PathBase;

            string mvcBaseUrl = $"{scheme}://{host}{pathBase}";
            string mvcItemEndpoint = $"{mvcBaseUrl}/items";

            string apiBaseUrl = "https://localhost:44307";
            string deleteItemEndpoint = $"{apiBaseUrl}/api/items/{Item.Id}?userId={CurrentUser.Id}&securityHash={CurrentUser.SecurityHash}";

            try
            {
                var apiToken = HttpContextAccessor.HttpContext.Request.Cookies["OnebrbApiToken"];
                Item.UserId = CurrentUser.Id;

                var json = JsonSerializer.Serialize(Item);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiToken);

                var response = await HttpClient.DeleteAsync(deleteItemEndpoint);

                string responseJson = response.Content.ReadAsStringAsync().Result;
                IsFormEnabled = false;
                notifyBarClass = BootstrapCssConst.AlertSuccess;
                var deleteItem = JsonSerializer.Deserialize<BaseApiResponse<DeleteItemResponseModel>>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                IsItemDeleted = true;
                OnDeleteResult = $"The item was deleted successfuly!";
            }
            catch (Exception ex)
            {
                notifyBarClass = $"{BootstrapCssConst.AlertDanger}";
                OnDeleteResult = "Couldn't edit the item, please try again later...";
                BtnSubmitCss = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnSuccess}";
            }
        }
    }
}
