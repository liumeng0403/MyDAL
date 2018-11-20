namespace MyDAL.Core.Common
{
    /// <summary>
    /// T 实例
    /// </summary>
    internal class ClassInstance<T>
        where T : class, new()
    {
        /// <summary>
        /// T 实例
        /// </summary>
        internal static T Instance
        {
            get
            {
                return new T();
            }
        }
    }
}
