using MyDAL.Core.Common;

namespace MyDAL.Core.Models.MysqlFunctionParam.DicParamModel
{
    /// <summary>
    /// mysql sum 函数
    /// </summary>
    internal class SumParam
        : DicParam
    {
        /// <summary>
        /// 函数名
        /// </summary>
        internal string FuncName { get; set; }
    }
}