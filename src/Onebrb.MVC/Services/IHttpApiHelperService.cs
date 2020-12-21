using Onebrb.MVC.Helpers;
using System.Threading.Tasks;

namespace Onebrb.MVC.Services
{
    public interface IApiService
    {
        Task<BaseApiResponse<T>> HttpGetRequest<T>(string uri);
    }
}