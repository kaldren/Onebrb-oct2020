using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Onebrb.MVC.Constants;
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
    public partial class EditItem : ComponentBase
    {
        // Parameters
        [Parameter]
        public EditItemViewModel Item { get; set; }

        [Parameter]
        public string CurrentUserId { get; set; }

        // Dependency Injection
        [Inject]
        public HttpClient HttpClient { get; set; }
        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; }


        // Misc
        public bool IsFormEnabled { get; set; } = true;
        public string OnSubmitResult { get; set; }
        public string notifyBarClass { get; set; }
        public string OnebrbApiToken { get; set; }
        public string EditedItemUrl { get; set; }
        public bool IsItemCreated { get; set; }
        public string EditBtnText { get; set; } = "Edit";
        public string BtnSubmitCss { get; set; } = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnSuccess}";

        public EditItem()
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
            EditBtnText = "Editing...";

            string scheme = HttpContextAccessor.HttpContext.Request.Scheme;
            string host = HttpContextAccessor.HttpContext.Request.Host.Value;
            string pathBase = HttpContextAccessor.HttpContext.Request.PathBase;

            string mvcBaseUrl = $"{scheme}://{host}{pathBase}";
            string mvcItemEndpoint = $"{mvcBaseUrl}/items";

            string apiBaseUrl = "https://localhost:44307";
            string createItemEndpoint = $"{apiBaseUrl}/api/items/{Item.Id}";

            try
            {
                var apiToken = HttpContextAccessor.HttpContext.Request.Cookies["OnebrbApiToken"];
                Item.UserId = CurrentUserId;

                var json = JsonSerializer.Serialize(Item);
                var data = new StringContent(json, Encoding.UTF8, "application/json-patch+json");

                HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiToken);

                var response = await HttpClient.PatchAsync($"{createItemEndpoint}", data);

                string responseJson = response.Content.ReadAsStringAsync().Result;
                IsFormEnabled = false;
                notifyBarClass = BootstrapCssConst.AlertSuccess;
                var createdItem = JsonSerializer.Deserialize<CreateItemResponseModel>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                IsItemCreated = true;
                OnSubmitResult = $"The item was edited successfuly!";
                EditBtnText = "Success";
                EditedItemUrl = $"{mvcItemEndpoint}/{createdItem.Id}";
            }
            catch (Exception ex)
            {
                notifyBarClass = $"{BootstrapCssConst.AlertDanger}";
                OnSubmitResult = "Couldn't edit the item, please try again later...";
                EditBtnText = "Edit";
                BtnSubmitCss = $"{BootstrapCssConst.Btn} {BootstrapCssConst.BtnSuccess}";
            }

        }
    }
}
