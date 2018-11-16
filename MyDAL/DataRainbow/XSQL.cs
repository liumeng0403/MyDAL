using System.Text;
using Yunyong.DataExchange.Core.Enums;

namespace Yunyong.DataExchange.DataRainbow
{
    internal abstract class XSQL
    {
        
        protected static void Spacing(StringBuilder sb)
        {
            sb.Append(' ');
        }
        protected static void Backquote(StringBuilder sb)
        {
            sb.Append('`');
        }
        protected static void AT(StringBuilder sb)
        {
            sb.Append('@');
        }
        protected static void Dot(StringBuilder sb)
        {
            sb.Append('.');
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

        /****************************************************************************************************************/

        protected static string Action(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.None:
                    return "";
                case ActionEnum.Insert:
                    return "";
                case ActionEnum.Update:
                    return "";
                case ActionEnum.Select:
                    return "";
                case ActionEnum.From:
                    return "";
                case ActionEnum.InnerJoin:
                    return " inner join ";
                case ActionEnum.LeftJoin:
                    return " left join ";
                case ActionEnum.On:
                    return " on ";
                case ActionEnum.Where:
                    return " \r\n where ";
                case ActionEnum.And:
                    return " \r\n \t and ";
                case ActionEnum.Or:
                    return " \r\n \t or ";
                case ActionEnum.OrderBy:
                    return "";
            }
            return " ";
        }
        protected static string MultiAction(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.And:
                    return " && ";
                case ActionEnum.Or:
                    return " || ";
            }
            return " ";
        }
        protected static string Option(OptionEnum option)
        {
            switch (option)
            {
                case OptionEnum.None:
                    return "<<<<<";
                case OptionEnum.Insert:
                    return "";
                case OptionEnum.InsertTVP:
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
                case FuncEnum.Column:
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

    }
}
