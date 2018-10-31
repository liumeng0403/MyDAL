using System.Collections.Generic;
using System.Data;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Cache;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Helper;
using Yunyong.DataExchange.Core.MySql;

namespace Yunyong.DataExchange.Core.Bases
{
    internal abstract class Context
    {

        /************************************************************************************************************************/

        internal void Init(IDbConnection conn)
        {
            Conn = conn;
            UiConditions = new List<DicModelUI>();
            DbConditions = new List<DicModelDB>();
            AH = new AttributeHelper(this);
            VH = new CsValueHelper(this);
            GH = GenericHelper.Instance;
            EH = new XExpression(this);
            SC = new StaticCache(this);
            PH = new ParameterHelper(this);
            BDH = BatchDataHelper.Instance;
            SqlProvider = new MySqlProvider(this);
            DS = DataSource.Instance;
            DH = new DicModelHelper(this);
        }

        /************************************************************************************************************************/

        internal AttributeHelper AH { get; private set; }
        internal GenericHelper GH { get; private set; }
        internal ParameterHelper PH { get; private set; }
        internal BatchDataHelper BDH { get; private set; }

        /************************************************************************************************************************/

        internal XExpression EH { get; private set; }
        internal CsValueHelper VH { get; private set; }
        internal DicModelHelper DH { get; private set; }

        /************************************************************************************************************************/

        internal CrudTypeEnum Crud { get; set; } = CrudTypeEnum.None;
        internal ActionEnum Action { get; set; } = ActionEnum.None;
        internal OptionEnum Option { get; set; } = OptionEnum.None;
        internal CompareEnum Compare { get; set; } = CompareEnum.None;
        internal FuncEnum Func { get; set; } = FuncEnum.None;

        /************************************************************************************************************************/

        internal int DicID { get; set; } = 1;
        internal List<DicModelUI> UiConditions { get; private set; }
        internal List<DicModelDB> DbConditions { get; private set; }

        /************************************************************************************************************************/

        internal IDbConnection Conn { get; private set; }
        internal IDbTransaction Tran { get; set; }

        /************************************************************************************************************************/

        internal MySqlProvider SqlProvider { get; set; }
        internal Operator OP { get; set; }
        internal Impler IP { get; set; }

        /************************************************************************************************************************/

        internal StaticCache SC { get; private set; }
        internal DataSource DS { get; private set; }

        /************************************************************************************************************************/

        internal bool IsInParameter(DicModelUI ui)
        {
            if (ui.CsValue != null
                && (ui.Option == OptionEnum.In || ui.Option == OptionEnum.NotIn)
                && ui.CsValue.ToString().Contains(","))
            {
                return true;
            }
            return false;
        }
        internal bool IsParameter(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.Insert:
                case ActionEnum.Update:
                case ActionEnum.Where:
                case ActionEnum.And:
                case ActionEnum.Or:
                    return true;
            }
            return false;
        }
        internal bool IsFilterCondition(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.Where:
                case ActionEnum.And:
                case ActionEnum.Or:
                    return true;
            }
            return false;
        }
        internal bool IsSingleTableOption(CrudTypeEnum crud)
        {
            switch (crud)
            {
                case CrudTypeEnum.Query:
                case CrudTypeEnum.Update:
                case CrudTypeEnum.Delete:
                    return true;
            }
            return false;
        }

        /************************************************************************************************************************/

        internal OptionEnum GetChangeOption(ChangeEnum change)
        {
            switch (change)
            {
                case ChangeEnum.Add:
                    return OptionEnum.ChangeAdd;
                case ChangeEnum.Minus:
                    return OptionEnum.ChangeMinus;
                default:
                    return OptionEnum.ChangeAdd;
            }
        }

        internal void AddConditions(DicModelUI dic)
        {
            DH.UniqueDicContext(dic, UiConditions);
            if (!IsInParameter(dic))
            {
                UiConditions.Add(dic);
            }

            //
            Compare = CompareEnum.None;
            Func = FuncEnum.None;
        }

        internal void ResetConditions()
        {
            UiConditions = new List<DicModelUI>();
            DbConditions = new List<DicModelDB>();
        }

        internal void SetMTCache<M>()
        {
            //
            var type = typeof(M);
            var key = SC.GetModelKey(type.FullName);

            //
            var table = SqlProvider.GetTableName<M>();
            SC.SetModelTableName(key, table);
            SC.SetModelType(key, type);
            SC.SetModelProperys(type, this);
            (SC.SetModelColumnInfos(key, this)).GetAwaiter().GetResult();
        }

    }
}
