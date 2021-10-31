using TooGoodToGoWatcherCore.DataContracts;

namespace TooGoodToGoWatcherCore
{
    public interface IIftttNotifier
    {
        void Notify(ItemElement item);
    }
}