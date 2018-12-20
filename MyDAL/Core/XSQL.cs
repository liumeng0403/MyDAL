using MyDAL.Core;
using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Core
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
        protected static void End(StringBuilder sb,List<string> sqls)
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
                    Left(sb);Spacing(sb);Join(sb);
                    return;
                case ActionEnum.On:
                    On(sb);
                    return;
                case ActionEnum.Where:
                    Where(sb);
                    return;
                case ActionEnum.And:
                    Tab(sb);And(sb);
                    return;
                case ActionEnum.Or:
                    Tab(sb);Or(sb);
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
        protected static string Option(OptionEnum option)
        {
            switch (option)
            {
                case OptionEnum.None:
                    return "<<<<<";
                case OptionEnum.Insert:
                    return "";
                case OptionEnum.Set:
                    return "=";
                case OptionEnum.ChangeAdd:
                    return "+";
                case OptionEnum.ChangeMinus:
                    return "-";
                case OptionEnum.Column:
                    return "";
                case OptionEnum.ColumnAs:
                    break;
                case OptionEnum.Compare:
                    return "";
                case OptionEnum.Like:
                    return " like ";
                case OptionEnum.Count:
                    return " count";
                case OptionEnum.Sum:
                    return " sum";
                case OptionEnum.OneEqualOne:
                    return "";
                case OptionEnum.IsNull:
                    return " is null ";
                case OptionEnum.IsNotNull:
                    return " is not null ";
                case OptionEnum.Asc:
                    return " asc ";
                case OptionEnum.Desc:
                    return " desc ";
            }
            return " ";
        }
        protected static string Compare(CompareEnum compare)
        {
            switch (compare)
            {
                case CompareEnum.None:
                    return " ";
                case CompareEnum.Equal:
                    return "=";
                case CompareEnum.NotEqual:
                    return "<>";
                case CompareEnum.LessThan:
                    return "<";
                case CompareEnum.LessThanOrEqual:
                    return "<=";
                case CompareEnum.GreaterThan:
                    return ">";
                case CompareEnum.GreaterThanOrEqual:
                    return ">=";
                case CompareEnum.Like:
                    return " like ";
                case CompareEnum.In:
                    return " in ";
                case CompareEnum.NotIn:
                    return " not in ";
            }
            return " ";
        }
        protected static string Function(FuncEnum func)
        {
            switch (func)
            {
                case FuncEnum.None:
                    return "";
                case FuncEnum.CharLength:
                    return " char_length";
                case FuncEnum.DateFormat:
                    return " date_format";
                case FuncEnum.Trim:
                    return " trim";
                case FuncEnum.LTrim:
                    return " ltrim";
                case FuncEnum.RTrim:
                    return " rtrim";
                case FuncEnum.In:
                    return " in ";
                case FuncEnum.NotIn:
                    return " not in ";
            }
            return " ";
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
