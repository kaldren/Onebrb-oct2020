// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace OnebrbApiClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class EditItemModel
    {
        /// <summary>
        /// Initializes a new instance of the EditItemModel class.
        /// </summary>
        public EditItemModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the EditItemModel class.
        /// </summary>
        public EditItemModel(double? price = default(double?), string title = default(string), string description = default(string), int? itemId = default(int?), string userId = default(string))
        {
            Price = price;
            Title = title;
            Description = description;
            ItemId = itemId;
            UserId = userId;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public double? Price { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "itemId")]
        public int? ItemId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

    }
}