using MyDAL.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyDAL.Core.Helper
{
    internal class GenericHelper : ClassInstance<GenericHelper>
    {
        public object GetTypeValue(PropertyInfo outerProp, object outerObj)
        {
            return outerProp.GetValue(outerObj);
        }
        public object GetTypeValue(object objVal)
        {
            return objVal;
        }

        public List<PropertyInfo> GetPropertyInfos(Type mType)
        {
            var props = mType.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
            return props;
        }

        public Assembly LoadAssembly(string fullClassName)
        {


            if (string.IsNullOrWhiteSpace(fullClassName))
            {
                return null;
            }

            //
            var ass = fullClassName.Substring(0, fullClassName.LastIndexOf('.'));
            var assD = $"{ass}.dll";
            var assE = $"{ass}.exe";
            var assemD = default(Assembly);
            var assemE = default(Assembly);

            //
            try
            {
                assemD = Assembly.LoadFrom(assD);
            }
            catch
            {
                assemD = LoadAssembly(ass);
            }
            if (assemD == null)
            {
                try
                {
                    assemE = Assembly.LoadFrom(assE);
                }
                catch
                {
                    assemE = LoadAssembly(ass);
                }
            }

            //
            if (assemD != null)
            {
                return assemD;
            }
            else if (assemE != null)
            {
                return assemE;
            }
            else
            {
                return null;
            }
        }

    }
}
