using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Api.Constants
{
    public static class ResponseMessages
    {
        // 200 codes
        public const string SuccessfulOperation = "Successful operation.";

        // 400 codes
        public const string BadRequest = "Bad Request.";
        public const string NotFound = "Not Found.";
        public const string Unauthorized = "Unauthorized.";
        public const string BadRequestCouldntCreateAccount = "Bad Request. Couldn't create an account.";

        // 500 codes
        public const string ServerError = "Server error.";
    }
}
