using MyDAL.Core.Common;

namespace MyDAL.Core.Models.MysqlFunctionParam.DicParamModel
{
    /// <summary>
    /// mysql count 函数
    /// </summary>
    internal class CountParam 
        : DicParam
    {
        /// <summary>
        /// 函数名
        /// </summary>
        internal string FuncName { get; set; }
    }
}