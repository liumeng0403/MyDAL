using HPC.DAL.Core.Extensions;
using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace HPC.DAL.Core.Common
{
    /// <summary>
    /// 深度复制 / Surrogate
    /// </summary>
    internal class NonSerialiazableTypeSurrogateSelector
        : ISerializationSurrogate, ISurrogateSelector
    {
        /// <summary>
        /// _nextSelector
        /// </summary>
        ISurrogateSelector _nextSelector;

        #region ISerializationSurrogate / 实现
        /// <summary>
        /// GetObjectData
        /// </summary>
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            FieldInfo[] fieldInfos = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var fi in fieldInfos)
            {
                if (IsKnownType(fi.FieldType))
                {
                    info.AddValue(fi.Name, fi.GetValue(obj));
                }
                else if (fi.FieldType.IsClass)
                {
                    info.AddValue(fi.Name, fi.GetValue(obj));
                }
            }
        }

        /// <summary>
        /// SetObjectData
        /// </summary>
        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            FieldInfo[] fieldInfos = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var fi in fieldInfos)
            {
                if (IsKnownType(fi.FieldType))
                {
                    if (fi.FieldType.IsNullable())
                    {
                        Type argumentValueForTheNullableType = GetFirstArgumentOfGenericType(fi.FieldType);
                        fi.SetValue(obj, info.GetValue(fi.Name, argumentValueForTheNullableType));
                    }
                    else
                    {
                        fi.SetValue(obj, info.GetValue(fi.Name, fi.FieldType));
                    }
                }
                else if (fi.FieldType.IsClass)
                {
                    fi.SetValue(obj, info.GetValue(fi.Name, fi.FieldType));
                }
            }
            return obj;
        }
        #endregion

        #region ISurrogateSelector / 实现
        /// <summary>
        /// ChainSelector
        /// </summary>
        public void ChainSelector(ISurrogateSelector selector)
        {
            this._nextSelector = selector;
        }

        /// <summary>
        /// GetNextSelector
        /// </summary>
        public ISurrogateSelector GetNextSelector()
        {
            return _nextSelector;
        }

        /// <summary>
        /// GetSurrogate
        /// </summary>
        public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
        {
            if (IsKnownType(type))
            {
                selector = null;
                return null;
            }
            else if (type.IsClass || type.IsValueType)
            {
                selector = this;
                return this;
            }
            else
            {
                selector = null;
                return null;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 是否为已知类型 / String,Primitive,Serializable
        /// </summary>
        private bool IsKnownType(Type type)
        {
            return type == typeof(string) || type.IsPrimitive || type.IsSerializable;
        }

        /// <summary>
        /// GetFirstArgumentOfGenericType
        /// </summary>
        private Type GetFirstArgumentOfGenericType(Type type)
        {
            return type.GetGenericArguments()[0];
        }
        #endregion
    }
}
