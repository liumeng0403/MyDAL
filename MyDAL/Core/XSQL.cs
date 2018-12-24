using System;
using System.Collections.Generic;
using System.Text;
using Yunyong.DataExchange.Core.Enums;

namespace Yunyong.DataExchange.Core
{
    internal abstract class XSQL
    {

        protected static void Spacing(StringBuilder sb)
        {
            sb.Append(' ');
        }
        protected static void EscapeChar(StringBuilder sb)
        {
            sb.Append('/');
        }
        protected static void Percent(StringBuilder sb)
        {
            sb.Append('%');
        }
        protected static void Backquote(StringBuilder sb)
        {
            sb.Append('`');
        }
        protected static void SingleQuote(StringBuilder sb)
        {
            sb.Append('\'');
        }
        protected static void AT(StringBuilder sb)
        {
            sb.Append('@');
        }
        protected static void Dot(StringBuilder sb)
        {
            sb.Append('.');
        }
        protected static void Star(StringBuilder sb)
        {
            sb.Append('*');
        }
        protected static void Comma(StringBuilder sb)
        {
            sb.Append(',');
        }
        protected static void CRLF(StringBuilder sb)
        {
            sb.Append("\r\n");
        }
        protected static void Tab(StringBuilder sb)
        {
            sb.Append("\t");
        }
        protected static void LeftBracket(StringBuilder sb)
        {
            sb.Append('(');
        }
        protected static void RightBracket(StringBuilder sb)
        {
            sb.Append(')');
        }
        protected static void Equal(StringBuilder sb)
        {
            sb.Append('=');
        }
        protected static void End(StringBuilder sb, List<string> sqls)
        {
            sb.Append(';');
            sqls.Add(sb.ToString());
            sb.Clear();
        }

        /****************************************************************************************************************/

        protected static void Action(ActionEnum action, StringBuilder sb)
        {
            switch (action)
            {
                case ActionEnum.None:
                case ActionEnum.Insert:
                case ActionEnum.Update:
                case ActionEnum.Select:
                case ActionEnum.From:
                case ActionEnum.OrderBy:
                    return;
                case ActionEnum.InnerJoin:
                    Inner(sb); Spacing(sb); Join(sb);
                    return;
                case ActionEnum.LeftJoin:
                    Left(sb); Spacing(sb); Join(sb);
                    return;
                case ActionEnum.On:
                    On(sb);
                    return;
                case ActionEnum.Where:
                    Where(sb);
                    return;
                case ActionEnum.And:
                    Tab(sb); And(sb);
                    return;
                case ActionEnum.Or:
                    Tab(sb); Or(sb);
                    return;
                default:
                    throw new Exception($"{XConfig.EC._014} -- [[{action}]] 不能解析!!!");
            }
        }
        protected static void MultiAction(ActionEnum action, StringBuilder sb)
        {
            if (action == ActionEnum.And)
            {
                Spacing(sb); sb.Append("&&"); Spacing(sb);
            }
            else if (action == ActionEnum.Or)
            {
                Spacing(sb); sb.Append("||"); Spacing(sb);
            }
            else
            {
                throw new Exception($"{XConfig.EC._010} -- [[{action}]] 不能解析!!!");
            }
        }
        protected static void Option(OptionEnum option, StringBuilder sb)
        {
            switch (option)
            {
                case OptionEnum.None:
                case OptionEnum.Insert:
                case OptionEnum.Column:
                case OptionEnum.ColumnAs:
                case OptionEnum.Compare:
                case OptionEnum.OneEqualOne:
                    return;
                case OptionEnum.Set:
                    sb.Append("=");
                    return;
                case OptionEnum.ChangeAdd:
                    sb.Append("+");
                    return;
                case OptionEnum.ChangeMinus:
                    sb.Append("-");
                    return;
                case OptionEnum.Like:
                    sb.Append(" like ");
                    return;
                case OptionEnum.IsNull:
                    sb.Append(" is null ");
                    return;
                case OptionEnum.IsNotNull:
                    sb.Append(" is not null ");
                    return;
                case OptionEnum.Asc:
                    sb.Append(" asc ");
                    return;
                case OptionEnum.Desc:
                    sb.Append(" desc ");
                    return;
                default:
                    throw new Exception($"{XConfig.EC._022} - [[{option}]] 不能解析!!!");
            }
        }
        protected static void Compare(CompareEnum compare, StringBuilder sb)
        {
            switch (compare)
            {
                case CompareEnum.None:
                    return;
                case CompareEnum.Equal:
                    sb.Append("=");
                    return;
                case CompareEnum.NotEqual:
                    sb.Append("<>");
                    return;
                case CompareEnum.LessThan:
                    sb.Append("<");
                    return;
                case CompareEnum.LessThanOrEqual:
                    sb.Append("<=");
                    return;
                case CompareEnum.GreaterThan:
                    sb.Append(">");
                    return;
                case CompareEnum.GreaterThanOrEqual:
                    sb.Append(">=");
                    return;
                case CompareEnum.Like:
                    sb.Append(" like ");
                    return;
                case CompareEnum.In:
                    sb.Append(" in ");
                    return;
                case CompareEnum.NotIn:
                    sb.Append(" not in ");
                    return;
                default:
                    throw new Exception($"{XConfig.EC._023} - [[{compare}]] 不能解析!!!");
            }
        }
        protected static void Function(FuncEnum func, StringBuilder sb)
        {
            switch (func)
            {
                case FuncEnum.None:
                    return;
                case FuncEnum.CharLength:
                    sb.Append(" char_length");
                    return;
                case FuncEnum.DateFormat:
                    sb.Append(" date_format");
                    return;
                case FuncEnum.Trim:
                    sb.Append(" trim");
                    return;
                case FuncEnum.LTrim:
                    sb.Append(" ltrim");
                    return;
                case FuncEnum.RTrim:
                    sb.Append(" rtrim");
                    return;
                case FuncEnum.In:
                    sb.Append(" in ");
                    return;
                case FuncEnum.NotIn:
                    sb.Append(" not in ");
                    return;
                case FuncEnum.Count:
                    sb.Append(" count");
                    return;
                case FuncEnum.Sum:
                    sb.Append(" sum");
                    return;
                default:
                    throw new Exception($"{XConfig.EC._008} - [[{func}]] 不能解析!!!");
            }
        }

        /****************************************************************************************************************/

        protected static void InsertInto(StringBuilder sb)
        {
            CRLF(sb);
            sb.Append("insert into");
        }
        protected static void Delete(StringBuilder sb)
        {
            sb.Append("delete");
        }
        protected static void Update(StringBuilder sb)
        {
            sb.Append("update");
        }
        protected static void Select(StringBuilder sb)
        {
            sb.Append("select");
        }

        protected static void From(StringBuilder sb)
        {
            CRLF(sb);
            sb.Append("from");
        }
        protected static void Inner(StringBuilder sb)
        {
            sb.Append("inner");
        }
        protected static void Left(StringBuilder sb)
        {
            sb.Append("left");
        }
        protected static void Join(StringBuilder sb)
        {
            sb.Append("join");
        }
        protected static void On(StringBuilder sb)
        {
            sb.Append("on");
        }

        protected static void Where(StringBuilder sb)
        {
            sb.Append("where");
        }
        protected static void And(StringBuilder sb)
        {
            sb.Append("and");
        }
        protected static void Or(StringBuilder sb)
        {
            sb.Append("or");
        }

        protected static void Values(StringBuilder sb)
        {
            CRLF(sb);
            sb.Append("values");
        }
        protected static void Set(StringBuilder sb)
        {
            CRLF(sb);
            sb.Append("set");
        }

        protected static void As(StringBuilder sb)
        {
            Spacing(sb);
            sb.Append("as");
            Spacing(sb);
        }
        protected static void Escape(StringBuilder sb)
        {
            sb.Append("escape");
        }

    }
}
