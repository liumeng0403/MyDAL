namespace HPC.DAL
{
    public enum DebugEnum
    {
        /// <summary>
        /// SQL 输出到【控制台】窗口
        /// </summary>
        Console,
        /// <summary>
        /// SQL 输出到 Visual Studio【输出】窗口，只能在 Visual Studio【Debug】模式运行
        /// </summary>
        Debug,
        /// <summary>
        /// SQL 输出到 Visual Studio【输出】窗口，可以在 Visual Studio【Debug】或【Release】模式运行
        /// </summary>
        Trace
    }
}
