using System.Text.RegularExpressions;

namespace EasyDAL.Exchange
{
    /// <summary>
    ///     排序设置
    /// </summary>
    public sealed class OrderBy
    {
        private string _field;

        /// <summary>
        ///     排序字段
        /// </summary>
        public string Field
        {
            get => _field;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _field = Regex.Match(value, "(\\w+)").Groups[1].Value;
                }
                else
                {
                    _field = null;
                }
            }
        }

        /// <summary>
        ///     正序还是倒序
        /// </summary>
        public bool Desc { get; set; }
    }
}
