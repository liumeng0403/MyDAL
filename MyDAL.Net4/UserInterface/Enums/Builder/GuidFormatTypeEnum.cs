namespace MyDAL
{
    public enum GuidFormatTypeEnum
    {
        /// <summary>
        /// All CHAR(36) columns are read/written as a Guid using lowercase hex with hyphens, which matches UUID().
        /// </summary>
        Char36,
        /// <summary>
        /// All CHAR(32) columns are read/written as a Guid using lowercase hex without hyphens.
        /// </summary>
        Char32,
        /// <summary>
        /// All BINARY(16) columns are read/written as a Guid using big-endian byte order, which matches UUID_TO_BIN(x).
        /// </summary>
        Binary16,
        /// <summary>
        /// All BINARY(16) columns are read/written as a Guid using big-endian byte order with time parts swapped, which matches UUID_TO_BIN(x,1).
        /// </summary>
        TimeSwapBinary16,
        /// <summary>
        /// All BINARY(16) columns are read/written as a Guid using little-endian byte order, i.e.the byte order used by Guid.ToByteArray() and the Guid(byte[]) constructor.
        /// </summary>
        LittleEndianBinary16,
        /// <summary>
        /// No column types are automatically read as a Guid.
        /// </summary>
        None,
        /// <summary>
        /// Same as Char36 if OldGuids= False; same as LittleEndianBinary16 if OldGuids=True.
        /// </summary>
        Default

    }
}
