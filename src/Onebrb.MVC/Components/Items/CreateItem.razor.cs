using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Onebrb.MVC.Constants;
using Onebrb.MVC.Models;
using Onebrb.MVC.Models.Item;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Onebrb.MVC.Components.Items
{
    public partial class CreateItem : ComponentBase
    {
        // Parameters
        [Parameter]
        public CreateItemViewModel Model { get; set; }

        [Parameter]
        public string CurrentUserId { get; set; }

        // Dependency Injection
        [Inject]
        public HttpClient HttpClient { get; set; }
        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; }


        // Misc
        public string SelectedCategoryId { get; set; }
        public bool IsFormEnabled { get; set; } = true;
        public string OnSubmitResult { get; set; }
        public string notifyBarClass { get; set; }
        public string OnebrbApiToken { get; set; }
        public string CreatedItemUrl { get; set; }
        public bool IsItemCreated { get; set; }
        public string PublishBtnText { get; set; } = "Publish";
        public string BtnSubmitCss { get; set; } = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnSuccess}";

        CreateItemRequestModel Item = new CreateItemRequestModel();


        public CreateItem()
        {
        }

        private async Task HandleValidSubmit()
        {
            if (!IsFormEnabled)
            {
                return;
            }

            IsFormEnabled = false;

            notifyBarClass = BootstrapCssConst.AlertInfo;
            OnSubmitResult = $"Please wait...";
            PublishBtnText = "Publishing...";

            string scheme = HttpContextAccessor.HttpContext.Request.Scheme;
            string host = HttpContextAccessor.HttpContext.Request.Host.Value;
            string pathBase = HttpContextAccessor.HttpContext.Request.PathBase;

            string mvcBaseUrl = $"{scheme}://{host}{pathBase}";
            string mvcItemEndpoint = $"{mvcBaseUrl}/items";

            string apiBaseUrl = "https://localhost:44307";
            string createItemEndpoint = $"{apiBaseUrl}/api/items/create";

            try
            {
                if (string.IsNullOrWhiteSpace(SelectedCategoryId))
                {
                    SelectedCategoryId = @Model.Categories.First().Id.ToString();
                }

                Item.CategoryId = int.Parse(SelectedCategoryId);
                Item.UserId = CurrentUserId;

                var json = JsonSerializer.Serialize(Item);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var apiToken = HttpContextAccessor.HttpContext.Request.Cookies["OnebrbApiToken"];

                HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiToken);

                var response = await HttpClient.PostAsync($"{createItemEndpoint}", data);

                string responseJson = response.Content.ReadAsStringAsync().Result;
                IsFormEnabled = false;
                notifyBarClass = BootstrapCssConst.AlertSuccess;
                var createdItem = JsonSerializer.Deserialize<BaseApiResponse<CreateItemResponseModel>>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                IsItemCreated = true;
                OnSubmitResult = $"The item was published successfuly!";
                PublishBtnText = "Published";
                CreatedItemUrl = $"{mvcItemEndpoint}/{createdItem.Body.Id}";
            }
            catch (Exception)
            {
                notifyBarClass = $"{BootstrapCssConst.AlertDanger}";
                OnSubmitResult = "Couldn't publish the item, please try again later...";
                PublishBtnText = "Publish";
                BtnSubmitCss = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnSuccess}";
            }

        }
    }
}
