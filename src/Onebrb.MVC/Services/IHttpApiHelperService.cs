using Onebrb.MVC.Models;
using System.Threading.Tasks;

namespace Onebrb.MVC.Services
{
    public interface IApiService
    {
        Task<BaseApiResponse<T>> HttpGetRequest<T>(string uri);
    }
}