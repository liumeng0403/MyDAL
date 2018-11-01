using MyDAL.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDAL.Core.Helper
{
    internal class BatchDataHelper 
    {
        
        internal async Task<int> StepProcess<M>(IEnumerable<M> modelList, int stepNum, Func<IEnumerable<M>,Task<int>> func)
        {
            //
            var result = 0;
            var total = modelList.Count();
            var limit = default(int);
            if (stepNum <= 0)
            {
                limit = 100;
            }
            else
            {
                limit = stepNum;
            }
            var start = 0;

            //
            do
            {
                var models = modelList.Skip(start).Take(limit);
                if (func != null)
                {
                    result += await func(models);
                }
                if (start < (total - limit))
                {
                    start = start + limit;
                }
                else
                {
                    start = total;
                }
            }
            while (start < total);

            //
            return result;
        }

    }
}
