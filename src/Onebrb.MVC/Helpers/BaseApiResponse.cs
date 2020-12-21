namespace Onebrb.MVC.Helpers
{
    public class BaseApiResponse<TResponse>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TResponse Response { get; set; }
    }
}
