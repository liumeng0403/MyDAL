using EasyDAL.Exchange.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyDAL.Exchange.Helper
{
    public class GenericHelper : ClassInstance<GenericHelper>
    {


        public RM GetPropertyValue<M, RM>(M m, string properyName)
        {
            try
            {
                if (m is ExpandoObject)
                {
                    var dic = m as IDictionary<string, object>;
                    return (RM)dic[properyName];
                }

                return (RM)m.GetType().GetProperty(properyName).GetValue(m, null);
            }
            catch (Exception ex)
            {
                throw new Exception("方法GetPropertyValue<M, RM>出错:" + ex.Message);
            }
        }

        public string GetPropertyValue<M>(M m, string properyName)
        {
            try
            {
                if (m is ExpandoObject)
                {
                    var dic = m as IDictionary<string, object>;
                    return ConvertType(dic[properyName]);
                }

                return m.GetType().GetProperty(properyName).GetValue(m, null).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("方法GetPropertyValue<M>出错:" + "请向方法 [GenericHelper.ConvertType] 中添加类型解析 " + ex.Message);
            }
        }

        public void SetPropertyValue<M>(M m, string propertyName, object value)
        {
            try
            {
                if (value != null)
                {
                    m.GetType().GetProperty(propertyName).SetValue(m, value, null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("方法SetPropertyValue<M>出错:" + ex.Message);
            }
        }

        public List<PropertyInfo> GetPropertyInfos<M>(M m)
        {
            if (m == null)
            {
                return new List<PropertyInfo>();
            }
            var props = m.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
            return props;
        }

        private string ConvertType(object value)
        {
            if (value.GetType() == typeof(DateTime))
            {
                return Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (value.GetType() == typeof(Guid))
            {
                return value.ToString();
            }

            return value.ToString();
        }
    }
}
