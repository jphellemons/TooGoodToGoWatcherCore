using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TooGoodToGoWatcherCore.DataContracts;

namespace TooGoodToGoWatcherCore.Handlers
{
    public class TooGoodToGoSessionLoaderHandler : ITooGoodToGoSessionLoaderHandler
    {
        private const string tgtgSessionFile = "TooGoodToGoSession.json";

        public TooGoodToGoSessionLoaderHandler()
        {

        }

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
                await JsonSerializer.SerializeAsync(stream, loginResponse);
            }
        }

        public void Reset()
        {
            File.Delete(tgtgSessionFile);
        }
    }
}
