using MyDAL.AdoNet;
using MyDAL.Cache;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.ExpressionX;
using MyDAL.Core.Helper;
using MyDAL.Core.MySql;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyDAL.Core
{
    internal abstract class Context
    {

        internal void Init(IDbConnection conn)
        {
            Conn = conn;
            UiConditions = new List<DicModelUI>();
            DbConditions = new List<DicModelDB>();
            AH = AttributeHelper.Instance;
            VH = new ValHandle(this);
            GH = GenericHelper.Instance;
            EH = new ExpressionHandleX(this);
            SC = StaticCache.Instance;
            PH = ParameterHelper.Instance;
            BDH = BatchDataHelper.Instance;
            SqlProvider = new MySqlProvider(this);
            DS = DataSource.Instance;
            DH = DicHandle.Instance;
            DH.DC = this;
        }

        /************************************************************************************************************************/

        internal AttributeHelper AH { get; private set; }
        internal GenericHelper GH { get; private set; }
        internal ParameterHelper PH { get; private set; }
        internal BatchDataHelper BDH { get; private set; }

        /************************************************************************************************************************/

        internal XDebug Hint { get; set; }

        internal ExpressionHandleX EH { get; private set; }
        internal ValHandle VH { get; private set; }
        internal DicHandle DH { get; private set; }

        /************************************************************************************************************************/

        internal CrudTypeEnum Crud { get; set; } = CrudTypeEnum.None;
        internal ActionEnum Action { get; set; } = ActionEnum.None;
        internal OptionEnum Option { get; set; } = OptionEnum.None;
        internal CompareEnum Compare { get; set; } = CompareEnum.None;

        /************************************************************************************************************************/

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

        internal bool IsParameter(DicModelUI item)
        {
            switch (item.Action)
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
        internal bool IsParameter(DicModelDB item)
        {
            switch (item.Action)
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

        internal void AddConditions(DicModelUI dic)
        {
            if (dic.CsValue!=null
                && dic.Option == OptionEnum.In
                && dic.CsValue.ToString().Contains(","))
            {
                var vals = dic.CsValue.ToString().Split(',').Select(it => it);
                var i = 0;
                foreach (var val in vals)
                {
                    //
                    i++;
                    var op = OptionEnum.None;
                    if (i == 1)
                    {
                        op = OptionEnum.In;
                    }
                    else
                    {
                        op = OptionEnum.InHelper;
                    }

                    //
                    var dicx = DicHandle.UiDicCopy(dic, val,dic.CsValueStr, op);
                    AddConditions(dicx);
                }
                UiConditions.Remove(dic);
            }
            else
            {
                //
                if(UiConditions.Count==0)
                {
                    dic.ID = 0;
                }
                else
                {
                    dic.ID = UiConditions.Max(it => it.ID) + 1;
                }

                //
                if(!string.IsNullOrWhiteSpace(dic.ParamRaw))
                {
                    dic.Param = $"{dic.ParamRaw}__{dic.ID}";
                }

                //
                UiConditions.Add(dic);
            }
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
            var key = SC.GetKey(type.FullName, Conn.Database);

            //
            var table = SqlProvider.GetTableName(type);
            SC.SetModelTableName(key, table);
            SC.SetModelType(key, type);
            SC.SetModelProperys(type, this);
            (SC.SetModelColumnInfos(key, this)).GetAwaiter().GetResult();
        }

        private void SetInsertValue<M>(M m, OptionEnum option, int index)
        {
            var key = SC.GetKey(m.GetType().FullName, Conn.Database);
            var props = SC.GetModelProperys(key);
            var columns = SC.GetColumnInfos(key);
            var fullName = typeof(M).FullName;

            foreach (var prop in props)
            {
                //var val = GH.GetTypeValue(prop, m);
                var val = VH.PropertyValue(prop, m);
                Option = option;
                Compare = CompareEnum.None;
                AddConditions(DH.InsertDic(fullName, prop.Name, val, prop.PropertyType, option, index));
            }
        }
        internal void GetProperties<M>(M m)
        {
            SetInsertValue(m, OptionEnum.Insert, 0);
        }
        internal void GetProperties<M>(IEnumerable<M> mList)
        {
            var i = 0;
            foreach (var m in mList)
            {
                SetInsertValue(m, OptionEnum.InsertTVP, i);
                i++;
            }
        }

    }
}
