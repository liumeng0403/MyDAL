using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Extensions;

namespace Yunyong.DataExchange
{
    public class XParallelTest<Req, Res>
        where Req : class, new()
        where Res : class, new()
    {

        private void Process()
        {
            try
            {

                //
                Console.WriteLine("======================= Start ================================");
                var start = 0;
                SyncBags = new ConcurrentBag<SyncState>();
                var swallow = new Stopwatch();
                swallow.Start();
                do
                {
                    //
                    TList = new List<Task>();
                    Simple = SyncState.Instance;
                    Simple.Wait = true;
                    for (var i = 0; i < Parallelism; i++)
                    {
                        var task = Task.Factory.StartNew(state =>
                        {
                            var sync = state as SyncState;
                            while (sync.WaitFlag.Wait)
                            {
                                sync.WaitSimple();
                            }
                            sync.MockClientSimple();
                            if (TargetFunc != null)
                            {
                                var res = Response.DeepClone();
                                var req = Request.DeepClone();
                                var api = TargetFunc.DeepClone();
                                var timer = new Stopwatch();
                                timer.Start();
                                res = api(req);
                                timer.Stop();
                                sync.Elapsed = timer.Elapsed;
                                sync.Result = res.JsonSerialize().JsonDeserialize<SyncState>();
                                if (IsLookTask)
                                {
                                    Console.WriteLine($"并行个数:{Parallelism},并行起点:{sync.Start},并行序号:{sync.LimitI},调用时间:{sync.Elapsed},调用状况:{sync.Result.SuccessFlag}");
                                }
                            }
                            SyncBags.Add(sync);
                        }, new SyncState
                        {
                            Start = start,
                            LimitI = i,
                            WaitFlag = Simple
                        }, TaskCreationOptions.LongRunning);
                        task.ContinueWith(state =>
                        {
                            state.Exception.Handle(e =>
                            {
                                return true;
                            });
                        }, TaskContinuationOptions.OnlyOnFaulted);
                        TList.Add(task);
                    }

                    //
                    Simple.Wait = false;
                    while (TList.Count > 0)
                    {
                        var t = TList.FirstOrDefault();
                        if (t != null)
                        {
                            if (t.Status == TaskStatus.RanToCompletion)
                            {
                                TList.Remove(t);
                            }
                        }
                        SyncState.Instance.WaitSimple();
                    }

                    //
                    if (start < (XConfig.PC.TotalCount - Parallelism))
                    {
                        start = start + Parallelism;
                    }
                    else
                    {
                        start = XConfig.PC.TotalCount;
                    }
                }
                while (start < XConfig.PC.TotalCount);
                swallow.Stop();
                Console.WriteLine("======================= Mid ================================");
                var totalTime = new TimeSpan(0, 0, 0, 0, 0);
                foreach (var elap in SyncBags)
                {
                    totalTime = totalTime.Add(elap.Elapsed);
                }
                var avgT = totalTime.TotalMilliseconds * 1.00 / XConfig.PC.TotalCount;
                var avgS = swallow.Elapsed.TotalMilliseconds * 1.00 / XConfig.PC.TotalCount;
                Console.WriteLine($"万次吞吐,单计量,总时间:{totalTime},平均每次时间:{avgT} ms.");
                Console.WriteLine($"万次吞吐,总计量,总时间:{swallow.Elapsed},平均每次时间:{avgS} ms.");
                Console.WriteLine("======================= End ================================");
            }
            catch
            {
                Console.WriteLine(string.Format($"{TargetFunc.ToString()}异常,并行压测程序已退出!!!"));
            }
        }

        /***************************************************************************************************************************/

        private int Parallelism { get; set; }
        private ConcurrentBag<SyncState> SyncBags { get; set; }
        private SyncState Simple { get; set; }
        private List<Task> TList { get; set; }
        public bool IsLookTask { get; set; } = false;
        public Req Request { get; set; }
        public Res Response { get; set; }
        public Func<Req, Res> TargetFunc { get; set; }

        /***************************************************************************************************************************/

        /// <summary>
        /// ApiDebug
        /// </summary>
        public void ApiDebug()
        {
            XConfig.PC.IsDebug = true;
            Parallelism = 1;
            Process();
            XConfig.PC.IsDebug = false;
        }

        /***************************************************************************************************************************/

        /// <summary>
        /// 万请求:并行度:一
        /// </summary>
        public void Parallel_1_10000()
        {
            Parallelism = 1;
            Process();
        }
        /// <summary>
        /// 万请求:并行度:五
        /// </summary>
        public void Parallel_5_10000()
        {
            Parallelism = 5;
            Process();
        }
        /// <summary>
        /// 万请求:并行度:十
        /// </summary>
        public void Parallel_10_10000()
        {
            Parallelism = 10;
            Process();
        }
        /// <summary>
        /// 万请求:并行度:二十
        /// </summary>
        public void Parallel_20_10000()
        {
            Parallelism = 20;
            Process();
        }
        /// <summary>
        /// 万请求:并行度:三十
        /// </summary>
        public void Parallel_30_10000()
        {
            Parallelism = 30;
            Process();
        }
        /// <summary>
        /// 万请求:并行度:四十
        /// </summary>
        public void Parallel_40_10000()
        {
            Parallelism = 40;
            Process();
        }
        /// <summary>
        /// 万请求:并行度:五十
        /// </summary>
        public void Parallel_50_10000()
        {
            Parallelism = 50;
            Process();
        }
        /// <summary>
        /// 万请求:并行度:六十
        /// </summary>
        public void Parallel_60_10000()
        {
            Parallelism = 60;
            Process();
        }
        /// <summary>
        /// 万请求:并行度:七十
        /// </summary>
        public void Parallel_70_10000()
        {
            Parallelism = 70;
            Process();
        }
        /// <summary>
        /// 万请求:并行度:八十
        /// </summary>
        public void Parallel_80_10000()
        {
            Parallelism = 80;
            Process();
        }
        /// <summary>
        /// 万请求:并行度:九十
        /// </summary>
        public void Parallel_90_10000()
        {
            Parallelism = 90;
            Process();
        }
        /// <summary>
        /// 万请求:并行度:一百
        /// </summary>
        public void Parallel_100_10000()
        {
            Parallelism = 100;
            Process();
        }

    }
}
