using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TooGoodToGoWatcherCore.DataContracts;

namespace TooGoodToGoWatcherCore.Handlers
{
    public class ApiHandler : IDisposable
    {
        private const string tgtgApi = "https://apptoogoodtogo.com/api/";

        private HttpClient httpClient;
        private string mail;
        private string password;

        public ApiHandler(string mail, string password)
        {
            this.mail = mail;
            this.password = password;
            httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "TooGoodToGo/21.9.0 (813) (iPhone/iPhone 7 (GSM); iOS 15.1; Scale/2.00)");
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

        private ListFavorites GetListFavorites(long userId)
        {
            return new ListFavorites()
            {
                FavoriteOnly = true,
                Origin = new Coordinates() { Latitude = 51.697815, Longitude = 5.303675 },
                Radius = 15,
                UserId = userId
            };
        }

        private LoginRequest GetLoginRequest()
        {
            return new LoginRequest()
            {
                Email = mail,
                Password = password,
                DeviceType = "UNKNOWN"
            };
        }

        internal void ResetSession()
        {
            TooGoodToGoSessionLoaderHandler tooGoodToGoSessionLoader = new TooGoodToGoSessionLoaderHandler();
            tooGoodToGoSessionLoader.Reset();
        }
    }
}
