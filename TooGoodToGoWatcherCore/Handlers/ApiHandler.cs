using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TooGoodToGoWatcherCore.DataContracts;
using TooGoodToGoWatcherCore.Models;

namespace TooGoodToGoWatcherCore.Handlers
{
    public class ApiHandler : IApiHandler, IDisposable
    {
        private const string tgtgApi = "https://apptoogoodtogo.com/api/";

        private HttpClient httpClient;
        private string mail;
        private string password;

        public ApiHandler(Secrets secrets)
        {
            this.mail = secrets.Email;
            this.password = secrets.Password;
            httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US");
        }

        ~ ApiHandler()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null;
            }
        }

        public async Task<LoginResponse> GetSession()
        {
            TooGoodToGoSessionLoaderHandler tooGoodToGoSessionLoader = new TooGoodToGoSessionLoaderHandler();
            var loginResponse = await tooGoodToGoSessionLoader.Load();

            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.RefreshToken))
            {
                var response = await httpClient.PostAsJsonAsync(tgtgApi + "auth/v1/loginByEmail", GetLoginRequest());

                loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                await tooGoodToGoSessionLoader.Save(loginResponse);
            }

            return loginResponse;
        }

        public async Task<HttpResponseMessage> GetFavoriteList(LoginResponse loginResponse)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.AccessToken);

            var lfResponse = await httpClient.PostAsJsonAsync(tgtgApi + "item/v7/", GetListFavorites(loginResponse.StartupData.User.UserId));

            return lfResponse;
        }

        public ListFavorites GetListFavorites(long userId)
        {
            return new ListFavorites()
            {
                FavoriteOnly = true,
                Origin = new Coordinates() { Latitude = 51.697815, Longitude = 5.303675 },
                Radius = 15,
                UserId = userId
            };
        }

        public LoginRequest GetLoginRequest()
        {
            return new LoginRequest()
            {
                Email = mail,
                Password = password,
                DeviceType = "UNKNOWN"
            };
        }

        public void ResetSession()
        {
            TooGoodToGoSessionLoaderHandler tooGoodToGoSessionLoader = new TooGoodToGoSessionLoaderHandler();
            tooGoodToGoSessionLoader.Reset();
        }
    }
}
