using System.Net.Http;
using System.Threading.Tasks;
using TooGoodToGoWatcherCore.DataContracts;

namespace TooGoodToGoWatcherCore.Handlers
{
    public interface IApiHandler
    {
        Task<LoginResponse> GetSession();

        Task<HttpResponseMessage> GetFavoriteList(LoginResponse loginResponse);

        ListFavorites GetListFavorites(long userId);

        LoginRequest GetLoginRequest();

        void ResetSession();
    }
}