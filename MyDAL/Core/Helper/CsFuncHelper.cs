using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Core.Models.ExpPara;
using System.Linq.Expressions;
using MyDAL.Core.Models.MysqlFunctionParam;
using MyDAL.Tools;

namespace MyDAL.Core.Helper
{
    internal class CsFuncHelper
    {
        private Context DC { get; set; }
        private CsFuncHelper() { }
        internal CsFuncHelper(Context dc)
        {
            DC = dc;
        }

        internal bool IsToStringFunc(string expStr)
        {
            if (expStr.Contains(".ToString(")
                && expStr.IndexOf(".") < expStr.LastIndexOf("."))
            {
                return true;
            }

            return false;
        }
        internal bool IsLengthFunc(string expStr)
        {
            if (expStr.Contains(".Length")
                && expStr.IndexOf(".") < expStr.LastIndexOf("."))
            {
                return true;
            }

            return false;
        }

        internal MydalFunction IsMydalMysqlFunction(MethodCallExpression mcExpr)
        {
            if (typeof(XFunction) == mcExpr.Method.DeclaringType)
            {
                return new MydalFunction()
                {
                    Flag = true
                };
            }

            return new MydalFunction()
            {
                Flag = false
            };
        }
        
        internal CountParam IsCount(MethodCallExpression mcExpr)
        {
            var exprStr = mcExpr.ToString();
            
            
            return new CountParam
            {
                Flag = false
            };
            // if (exprStr.Contains(".Contains("))
            // {
            //     if (mcExpr.Object != null)
            //     {
            //         var objExpr = mcExpr.Object;
            //         var objNodeType = mcExpr.Object.NodeType;
            //         if (objNodeType == ExpressionType.MemberAccess)
            //         {
            //             var memType = objExpr.Type;
            //             if (memType == XConfig.CSTC.String)
            //             {
            //                 return new ContainsLikeParam
            //                 {
            //                     Flag = true,
            //                     Like = StringLikeEnum.Contains
            //                 };
            //             }
            //             else
            //             {
            //                 return new ContainsLikeParam
            //                 {
            //                     Flag = false,
            //                     Like = StringLikeEnum.None
            //                 };
            //             }
            //         }
            //         else
            //         {
            //             return new ContainsLikeParam
            //             {
            //                 Flag = false,
            //                 Like = StringLikeEnum.None
            //             };
            //         }
            //     }
            //     else
            //     {
            //         return new ContainsLikeParam
            //         {
            //             Flag = false,
            //             Like = StringLikeEnum.None
            //         };
            //     }
            // }
            // else if (exprStr.Contains(".StartsWith("))
            // {
            //     return new ContainsLikeParam
            //     {
            //         Flag = true,
            //         Like = StringLikeEnum.StartsWith
            //     };
            // }
            // else if (exprStr.Contains(".EndsWith("))
            // {
            //     return new ContainsLikeParam
            //     {
            //         Flag = true,
            //         Like = StringLikeEnum.EndsWith
            //     };
            // }
            // else
            // {
            //     return new ContainsLikeParam
            //     {
            //         Flag = false,
            //         Like = StringLikeEnum.None
            //     };
            // }

        }
        
        internal ContainsLikeParam IsContainsLikeFunc(MethodCallExpression mcExpr)
        {
            var exprStr = mcExpr.ToString();
            if (exprStr.Contains(".Contains("))
            {
                if (mcExpr.Object != null)
                {
                    var objExpr = mcExpr.Object;
                    var objNodeType = mcExpr.Object.NodeType;
                    if (objNodeType == ExpressionType.MemberAccess)
                    {
                        var memType = objExpr.Type;
                        if (memType == XConfig.CSTC.String)
                        {
                            return new ContainsLikeParam
                            {
                                Flag = true,
                                Like = StringLikeEnum.Contains
                            };
                        }
                        else
                        {
                            return new ContainsLikeParam
                            {
                                Flag = false,
                                Like = StringLikeEnum.None
                            };
                        }
                    }
                    else
                    {
                        return new ContainsLikeParam
                        {
                            Flag = false,
                            Like = StringLikeEnum.None
                        };
                    }
                }
                else
                {
                    return new ContainsLikeParam
                    {
                        Flag = false,
                        Like = StringLikeEnum.None
                    };
                }
            }
            else if (exprStr.Contains(".StartsWith("))
            {
                return new ContainsLikeParam
                {
                    Flag = true,
                    Like = StringLikeEnum.StartsWith
                };
            }
            else if (exprStr.Contains(".EndsWith("))
            {
                return new ContainsLikeParam
                {
                    Flag = true,
                    Like = StringLikeEnum.EndsWith
                };
            }
            else
            {
                return new ContainsLikeParam
                {
                    Flag = false,
                    Like = StringLikeEnum.None
                };
            }
        }

        internal EqualsParam IsEqualsFunc(MethodCallExpression mcExpr)
        {
            var exprStr = mcExpr.ToString();
            if (exprStr.Contains(".Equals("))
            {
                if (mcExpr.Object.NonNull())
                {
                    var memVal = mcExpr.Arguments[0];
                    if (memVal.NodeType == ExpressionType.MemberAccess)
                    {
                        return new EqualsParam
                        {
                            Flag = true,
                            Type = ExpressionType.MemberAccess,
                            Val = memVal
                        };
                    }
                    else
                    {
                        return new EqualsParam
                        {
                            Flag = false
                        };
                    }
                }
                else
                {
                    return new EqualsParam
                    {
                        Flag = false
                    };
                }
            }
            else
            {
                return new EqualsParam
                {
                    Flag = false
                };
            }
        }
        internal ContainsInParam IsContainsInFunc(MethodCallExpression mcExpr)
        {
            var exprStr = mcExpr.ToString();
            if (exprStr.Contains(".Contains("))
            {
                if (mcExpr.Object == null)
                {
                    var memKey = mcExpr.Arguments[1];
                    var memVal = mcExpr.Arguments[0];
                    if (memVal.NodeType == ExpressionType.MemberAccess)
                    {
                        return new ContainsInParam
                        {
                            Flag = true,
                            Type = ExpressionType.MemberAccess,
                            Key = memKey,
                            Val = memVal
                        };
                    }
                    else if (memVal.NodeType == ExpressionType.NewArrayInit)
                    {
                        return new ContainsInParam
                        {
                            Flag = true,
                            Type = ExpressionType.NewArrayInit,
                            Key = memKey,
                            Val = memVal
                        };
                    }
                    else
                    {
                        return new ContainsInParam
                        {
                            Flag = false,
                            Type = default(ExpressionType),
                            Key = default(Expression),
                            Val = default(Expression)
                        };
                    }
                }
                else
                {
                    var objExpr = mcExpr.Object;
                    if (objExpr.NodeType == ExpressionType.MemberAccess)
                    {
                        var memType = objExpr.Type;
                        if (memType.IsList())
                        {
                            return new ContainsInParam
                            {
                                Flag = true,
                                Type = ExpressionType.MemberAccess,
                                Key = mcExpr,
                                Val = objExpr
                            };
                        }
                        else
                        {
                            return new ContainsInParam
                            {
                                Flag = false,
                                Type = default(ExpressionType),
                                Key = default(Expression),
                                Val = default(Expression)
                            };
                        }
                    }
                    else if (objExpr.NodeType == ExpressionType.ListInit)
                    {
                        return new ContainsInParam
                        {
                            Flag = true,
                            Type = ExpressionType.ListInit,
                            Key = mcExpr,
                            Val = objExpr
                        };
                    }
                    else if (objExpr.NodeType == ExpressionType.MemberInit)
                    {
                        return new ContainsInParam
                        {
                            Flag = true,
                            Type = ExpressionType.MemberInit,
                            Key = mcExpr,
                            Val = objExpr
                        };
                    }
                    else
                    {
                        return new ContainsInParam
                        {
                            Flag = false,
                            Type = default(ExpressionType),
                            Key = default(Expression),
                            Val = default(Expression)
                        };
                    }
                }
            }
            else
            {
                return new ContainsInParam
                {
                    Flag = false,
                    Type = default(ExpressionType),
                    Key = default(Expression),
                    Val = default(Expression)
                };
            }
        }
        internal ToStringParam IsToStringFunc(MethodCallExpression mcExpr)
        {
            var expStr = mcExpr.ToString();
            if (expStr.Contains(".ToString(")
                && expStr.IndexOf(".") < expStr.LastIndexOf("."))
            {
                if (DC.Action == ActionEnum.Select)
                {
                    return new ToStringParam
                    {
                        Flag = true
                    };
                }
                else
                {
                    return new ToStringParam
                    {
                        Flag = true
                    };
                }
            }

            return new ToStringParam
            {
                Flag = false
            };
        }
        internal TrimParam IsTrimFunc(string expStr)
        {
            if (expStr.Contains(".Trim(")
                && expStr.IndexOf(".") < expStr.LastIndexOf("."))
            {
                return new TrimParam
                {
                    Flag = true,
                    Trim = FuncEnum.Trim
                };
            }
            else if (expStr.Contains(".TrimStart(")
                && expStr.IndexOf(".") < expStr.LastIndexOf("."))
            {
                return new TrimParam
                {
                    Flag = true,
                    Trim = FuncEnum.LTrim
                };
            }
            else if (expStr.Contains(".TrimEnd(")
                && expStr.IndexOf(".") < expStr.LastIndexOf("."))
            {
                return new TrimParam
                {
                    Flag = true,
                    Trim = FuncEnum.RTrim
                };
            }
            else
            {
                return new TrimParam
                {
                    Flag = false,
                    Trim = FuncEnum.None
                };
            }
        }

    }
}
