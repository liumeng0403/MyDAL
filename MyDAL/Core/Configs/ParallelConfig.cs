namespace MyDAL.Core.Configs
{
    internal class ParallelConfig
    {
        internal bool IsDebug { get; set; } = false;
        internal int TotalCount
        {
            get
            {
                if(IsDebug)
                {
                    return 1;
                }
                else
                {
                    return 10000;
                }
            }
        }
    }
}
