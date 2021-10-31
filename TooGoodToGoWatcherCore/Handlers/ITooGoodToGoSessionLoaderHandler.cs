using System.Threading.Tasks;
using TooGoodToGoWatcherCore.DataContracts;

namespace TooGoodToGoWatcherCore.Handlers
{
    public interface ITooGoodToGoSessionLoaderHandler
    {
        Task<LoginResponse> Load();

        Task Save(LoginResponse loginResponse);

        void Reset();
    }
}