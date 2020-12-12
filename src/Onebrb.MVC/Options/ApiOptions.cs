namespace Onebrb.MVC.Config
{
    public class ApiOptions
    {
        public const string Token = "Token";

        public string InstanceId { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BaseAddress { get; set; }
        public string ResourceId { get; set; }
        public string Authority { get; set; }
    }
}
