namespace ClosetRpc.Net
{
    public interface IEventSource
    {
        void SendEvent(IRpcCallBuilder rpcCall);
    }
}
