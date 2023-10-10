namespace MyDAL.Core.Enums.Conns
{
    internal enum SslModeTypeEnum
    {
        /// <summary>
        /// Use SSL if the server supports it.
        /// </summary>
        Preferred,
        /// <summary>
        /// Do not use SSL.
        /// </summary>
        None,
        /// <summary>
        /// Always use SSL. Deny connection if server does not support SSL. Does not validate CA or hostname.
        /// </summary>
        Required,
        /// <summary>
        /// Always use SSL. Validates the CA but tolerates hostname mismatch.
        /// </summary>
        VerifyCA,
        /// <summary>
        /// Always use SSL. Validates CA and hostname.
        /// </summary>
        VerifyFull
    }
}
