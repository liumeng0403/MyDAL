namespace MyDAL.Core.Common
{
    /// <summary>
    ///     Object 实例
    /// </summary>
    internal class ClassInstance<T>
        where T : class, new()
    {
        /// <summary>
        ///     T 实例
        /// </summary>
        public static T Instance
        {
            get => new T();
        }
    }
}
