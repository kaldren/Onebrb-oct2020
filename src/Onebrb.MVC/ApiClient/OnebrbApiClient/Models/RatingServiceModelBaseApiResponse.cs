// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace OnebrbApiClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class RatingServiceModelBaseApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the RatingServiceModelBaseApiResponse
        /// class.
        /// </summary>
        public RatingServiceModelBaseApiResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the RatingServiceModelBaseApiResponse
        /// class.
        /// </summary>
        public RatingServiceModelBaseApiResponse(int? statusCode = default(int?), string message = default(string), RatingServiceModel body = default(RatingServiceModel))
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
        public RatingServiceModel Body { get; set; }

    }
}
