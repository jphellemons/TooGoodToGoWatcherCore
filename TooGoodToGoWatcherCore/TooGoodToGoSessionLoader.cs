using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TooGoodToGoWatcherCore
{
    public class TooGoodToGoSessionLoader
    {
        private const string tgtgSessionFile = "TooGoodToGoSession.json";

        public async Task<LoginResponse> Load()
        {
            if (File.Exists(tgtgSessionFile))
            {
                using (Stream stream = File.OpenRead(tgtgSessionFile))
                {
                    return await JsonSerializer.DeserializeAsync<LoginResponse>(stream);
                }
            }

            return null;
        }

        public async Task Save(LoginResponse loginResponse)
        {
            using (Stream stream = File.OpenWrite(tgtgSessionFile))
            {
                JsonSerializer.SerializeAsync(stream, loginResponse);
            }
        }
    }
}
