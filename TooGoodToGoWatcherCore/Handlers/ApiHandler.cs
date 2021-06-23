﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TooGoodToGoWatcherCore.DataContracts;

namespace TooGoodToGoWatcherCore.Handlers
{
    public class ApiHandler : IDisposable
    {
        private const string tgtgApi = "https://apptoogoodtogo.com/api/";

        private HttpClient httpClient;

        public ApiHandler()
        {
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
                Email = "info@jphellemons.nl",
                Password = "Your-password-here",
                DeviceType = "UNKNOWN"
            };
        }
    }
}
