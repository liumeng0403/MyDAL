using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using HPC.DAL.Core.Extensions;
using HPC.DAL.Core.Models.ExpPara;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HPC.DAL.Core.Helper
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
                        if (memType == XConfig.TC.String)
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
