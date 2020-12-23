// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace OnebrbApiClient.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ItemServiceModelICollectionBaseApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the
        /// ItemServiceModelICollectionBaseApiResponse class.
        /// </summary>
        public ItemServiceModelICollectionBaseApiResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// ItemServiceModelICollectionBaseApiResponse class.
        /// </summary>
        public ItemServiceModelICollectionBaseApiResponse(int? statusCode = default(int?), string message = default(string), IList<ItemServiceModel> body = default(IList<ItemServiceModel>))
        {
            StatusCode = statusCode;
            Message = message;
            Body = body;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statusCode")]
        public int? StatusCode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "body")]
        public IList<ItemServiceModel> Body { get; set; }

    }
}