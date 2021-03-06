// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace OnebrbApiClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class ItemServiceModelBaseApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the ItemServiceModelBaseApiResponse
        /// class.
        /// </summary>
        public ItemServiceModelBaseApiResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ItemServiceModelBaseApiResponse
        /// class.
        /// </summary>
        public ItemServiceModelBaseApiResponse(int? statusCode = default(int?), string message = default(string), ItemServiceModel body = default(ItemServiceModel))
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
        public ItemServiceModel Body { get; set; }

    }
}
