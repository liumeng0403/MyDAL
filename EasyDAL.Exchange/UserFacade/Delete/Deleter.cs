using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.UserFacade.Delete
{
    public class Deleter<M>: Operator
    {
        internal Deleter(DbContext dc)
        {
            DC = dc;
        }

        /// <summary>
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.Id == m.Id </param>
        public DeleteFilter<M> Where(Expression<Func<M, bool>> func)
        {
            WhereHandle(func, CrudTypeEnum.Delete);
            return new DeleteFilter<M>(DC);
        }


    }
}
