
namespace MyDAL
{
    public enum LoadBalanceTypeEnum
    {
        /// <summary>
        /// Each new connection opened for this connection pool uses the next host name (sequentially with wraparound). Requires Pooling=True. This is the default if Pooling=True.
        /// </summary>
        RoundRobin,
        /// <summary>
        /// Each new connection tries to connect to the first host; subsequent hosts are used only if connecting to the first one fails.This is the default if Pooling= False.
        /// </summary>
        FailOver,
        /// <summary>
        /// Servers are tried in a random order.
        /// </summary>
        Random,
        /// <summary>
        /// Servers are tried in ascending order of number of currently-open connections in this connection pool. Requires Pooling = True.
        /// </summary>
        LeastConnections

    }
}
