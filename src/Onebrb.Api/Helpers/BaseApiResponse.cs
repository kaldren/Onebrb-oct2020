namespace Onebrb.Api.Helpers
{
    public class BaseApiResponse<TBody>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TBody Body { get; set; }
    }
}
