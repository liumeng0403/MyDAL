using System.Threading.Tasks;
using Xunit;
using HPC.DAL.ModelTools;
using MyDAL.Test.ModelTools;
using System;

namespace MyDAL.Test.Tools
{
    public class _01_DeepClone
        : TestBase
    {

        [Fact]
        public void DeepOne()
        {
            xx = string.Empty;

            var obj = new ModelEntity
            {
                ValueField = 10,
                ReferenceField = "源值"
            };

            var cloneObj = obj.DeepClone();

            Console.WriteLine(obj.ValueField);   // 10
            Console.WriteLine(obj.ReferenceField);  // 源值

            Console.WriteLine(cloneObj.ValueField);  // 10
            Console.WriteLine(cloneObj.ReferenceField);  // 源值

            cloneObj.ReferenceField = "新值";

            Console.WriteLine(obj.ValueField);   // 10
            Console.WriteLine(obj.ReferenceField);  // 源值

            Console.WriteLine(cloneObj.ValueField);  // 10
            Console.WriteLine(cloneObj.ReferenceField);  // 新值

            xx = string.Empty;
        }

        [Fact]
        public void DeepTwo()
        {
            xx = string.Empty;

            var obj = new ModelEntity
            {
                ValueField = 10,
                ReferenceField = "源值10",
                ObjectField = new ModelEntity
                {
                    ValueField = 11,
                    ReferenceField = "源值11"
                }
            };

            var cloneObj = obj.DeepClone();

            Console.WriteLine(obj.ValueField);   // 10
            Console.WriteLine(obj.ReferenceField);  // 源值10
            Console.WriteLine(obj.ObjectField.ValueField);  // 11
            Console.WriteLine(obj.ObjectField.ReferenceField);  // 源值11

            Console.WriteLine(cloneObj.ValueField);  // 10
            Console.WriteLine(cloneObj.ReferenceField);  // 源值10
            Console.WriteLine(cloneObj.ObjectField.ValueField);  // 11
            Console.WriteLine(cloneObj.ObjectField.ReferenceField);  // 源值11

            cloneObj.ReferenceField = "新值10";
            cloneObj.ObjectField.ReferenceField = "新值11";

            Console.WriteLine(obj.ValueField);   // 10
            Console.WriteLine(obj.ReferenceField);  // 源值10
            Console.WriteLine(obj.ObjectField.ValueField);  // 11
            Console.WriteLine(obj.ObjectField.ReferenceField);  // 源值11

            Console.WriteLine(cloneObj.ValueField);  // 10
            Console.WriteLine(cloneObj.ReferenceField);  // 新值10
            Console.WriteLine(cloneObj.ObjectField.ValueField);  // 11
            Console.WriteLine(cloneObj.ObjectField.ReferenceField);  // 新值11

            xx = string.Empty;
        }

        [Fact]
        public void DeepThree()
        {
            xx = string.Empty;

            // 对象准备
            var obj = new ModelEntity
            {
                ValueField = 10,
                ReferenceField = "源值10",
                ObjectField = new ModelEntity
                {
                    ValueField = 11,
                    ReferenceField = "源值11",
                    ObjectField = new ModelEntity
                    {
                        ValueField = 12,
                        ReferenceField = "源值12"
                    }
                }
            };

            // 深度克隆
            var cloneObj = obj.DeepClone();

            // 源对象 值展示
            Console.WriteLine(obj.ValueField);   // 10
            Console.WriteLine(obj.ReferenceField);  // 源值10
            Console.WriteLine(obj.ObjectField.ValueField);  // 11
            Console.WriteLine(obj.ObjectField.ReferenceField);  // 源值11
            Console.WriteLine(obj.ObjectField.ObjectField.ValueField);  // 12
            Console.WriteLine(obj.ObjectField.ObjectField.ReferenceField);  // 源值12

            // 克隆对象 值展示
            Console.WriteLine(cloneObj.ValueField);  // 10
            Console.WriteLine(cloneObj.ReferenceField);  // 源值10
            Console.WriteLine(cloneObj.ObjectField.ValueField);  // 11
            Console.WriteLine(cloneObj.ObjectField.ReferenceField);  // 源值11
            Console.WriteLine(cloneObj.ObjectField.ObjectField.ValueField);  // 12
            Console.WriteLine(cloneObj.ObjectField.ObjectField.ReferenceField);  // 源值12

            // 变更 克隆对象 的值
            cloneObj.ReferenceField = "新值10";
            cloneObj.ObjectField.ReferenceField = "新值11";
            cloneObj.ObjectField.ObjectField.ReferenceField = "新值12";

            // 源对象 值展示
            Console.WriteLine(obj.ValueField);   // 10
            Console.WriteLine(obj.ReferenceField);  // 源值10
            Console.WriteLine(obj.ObjectField.ValueField);  // 11
            Console.WriteLine(obj.ObjectField.ReferenceField);  // 源值11
            Console.WriteLine(obj.ObjectField.ObjectField.ValueField);  // 12
            Console.WriteLine(obj.ObjectField.ObjectField.ReferenceField);  // 源值12

            // 克隆对象 值展示
            Console.WriteLine(cloneObj.ValueField);  // 10
            Console.WriteLine(cloneObj.ReferenceField);  // 新值10
            Console.WriteLine(cloneObj.ObjectField.ValueField);  // 11
            Console.WriteLine(cloneObj.ObjectField.ReferenceField);  // 新值11
            Console.WriteLine(cloneObj.ObjectField.ObjectField.ValueField);  // 12
            Console.WriteLine(cloneObj.ObjectField.ObjectField.ReferenceField);  // 新值12

            xx = string.Empty;
        }

    }
}
