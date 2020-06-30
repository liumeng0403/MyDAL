namespace MyDAL
{
    public enum ProtocolTypeEnum
    {
        /// <summary>
        /// Use TCP/IP sockets.
        /// </summary>
        Socket,
        /// <summary>
        /// Use a Unix socket.
        /// </summary>
        Unix,
        /// <summary>
        /// Use a Windows named pipe.
        /// </summary>
        Pipe
    }
}
