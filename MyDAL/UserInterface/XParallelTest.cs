using MyDAL.Core.Common;
using MyDAL.Core.Extensions;
using MyDAL.UserInterface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyDAL
{
    public class XParallelTest
    {

        private void Process()
        {
            try
            {

                //
                if (!IsSubTask)
                {
                    Console.WriteLine("======================= Start ================================");
                }
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
                                var api = default(Func<None, None>);
                                if (IsHttpFunc)
                                {
                                    api = TargetFunc;
                                }
                                else
                                {
                                    api = TargetFunc.DeepClone();
                                }
                                var timer = new Stopwatch();
                                timer.Start();
                                res = api(req);
                                timer.Stop();
                                sync.Elapsed = timer.Elapsed;
                                if (IsHttpFunc)
                                {
                                    sync.Result = res.String;
                                }
                                else
                                {
                                    sync.Result = res.String;
                                }
                                if (IsLookTask)
                                {
                                    var re = sync.Result?.Substring(0, sync.Result.Length > 50 ? 50 : sync.Result.Length);
                                    Console.WriteLine($"并行个数:{Parallelism},批次:{sync.Start / Parallelism},序号:{sync.LimitI},耗时:{sync.Elapsed},结果:{re}");
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
                    if (start < (TotalCount - Parallelism))
                    {
                        start = start + Parallelism;
                    }
                    else
                    {
                        start = TotalCount;
                    }
                }
                while (start < TotalCount);
                swallow.Stop();
                if (!IsSubTask)
                {
                    Console.WriteLine("======================= Mid ================================");
                    var totalTime = new TimeSpan(0, 0, 0, 0, 0);
                    foreach (var elap in SyncBags)
                    {
                        totalTime = totalTime.Add(elap.Elapsed);
                    }
                    var avgT = totalTime.TotalMilliseconds * 1.00 / TotalCount;
                    var avgS = swallow.Elapsed.TotalMilliseconds * 1.00 / TotalCount;
                    var msg = TotalCount == 1 ? "一次" : "万次";
                    Console.WriteLine($"{msg}吞吐,单计量,总时间:{totalTime},平均每次时间:{avgT} ms.");
                    Console.WriteLine($"{msg}吞吐,总计量,总时间:{swallow.Elapsed},平均每次时间:{avgS} ms.");
                    Console.WriteLine("======================= End ================================");
                }
            }
            catch
            {
                Console.WriteLine(string.Format($"{TargetFunc.ToString()}异常,并行压测程序已退出!!!"));
            }
        }

        /***************************************************************************************************************************/

        private bool IsDebug { get; set; } = false;
        private int TotalCount
        {
            get
            {
                if (IsDebug)
                {
                    return 1;
                }
                else
                {
                    return 10000;
                }
            }
        }
        private int Parallelism { get; set; }
        private ConcurrentBag<SyncState> SyncBags { get; set; }
        private SyncState Simple { get; set; }
        private List<Task> TList { get; set; }
        private HttpAsync Remoter
        {
            get
            {
                return new HttpAsync
                {
                    Token = Token,
                    URL = URL,
                    RequestMethod = RequestMethod,
                    JsonContent = JsonContent
                };
            }
        }
        private Func<None, None> _targetFunc { get; set; }

        public bool IsLookTask { get; set; } = false;
        public bool IsSubTask { get; set; } = false;
        public None Request { get; set; } = new None();
        public None Response { get; set; } = new None();

        public bool IsHttpFunc { get; set; } = false;
        public string Token { get; set; }
        public string URL { get; set; }
        public string RequestMethod { get; set; }
        public string JsonContent { get; set; }

        public Func<None, None> TargetFunc
        {
            get
            {
                if (IsHttpFunc)
                {
                    return Remoter.GetRemoteData;
                }
                else
                {
                    return _targetFunc;
                }
            }
            set
            {
                _targetFunc = value;
            }
        }

        /***************************************************************************************************************************/

        /// <summary>
        /// ApiDebug
        /// </summary>
        public void ApiDebug()
        {
            IsDebug = true;
            Parallelism = 1;
            Process();
            IsDebug = false;
        }

        /***************************************************************************************************************************/

        /// <summary>
        /// 万吞吐:并行度:一
        /// </summary>
        public void Parallel_1_10000()
        {
            Parallelism = 1;
            Process();
        }
        /// <summary>
        /// 万吞吐:并行度:五
        /// </summary>
        public void Parallel_5_10000()
        {
            Parallelism = 5;
            Process();
        }
        /// <summary>
        /// 万吞吐:并行度:十
        /// </summary>
        public void Parallel_10_10000()
        {
            Parallelism = 10;
            Process();
        }
        /// <summary>
        /// 万吞吐:并行度:二十
        /// </summary>
        public void Parallel_20_10000()
        {
            Parallelism = 20;
            Process();
        }
        /// <summary>
        /// 万吞吐:并行度:三十
        /// </summary>
        public void Parallel_30_10000()
        {
            Parallelism = 30;
            Process();
        }
        /// <summary>
        /// 万吞吐:并行度:四十
        /// </summary>
        public void Parallel_40_10000()
        {
            Parallelism = 40;
            Process();
        }
        /// <summary>
        /// 万吞吐:并行度:五十
        /// </summary>
        public void Parallel_50_10000()
        {
            Parallelism = 50;
            Process();
        }
        /// <summary>
        /// 万吞吐:并行度:六十
        /// </summary>
        public void Parallel_60_10000()
        {
            Parallelism = 60;
            Process();
        }
        /// <summary>
        /// 万吞吐:并行度:七十
        /// </summary>
        public void Parallel_70_10000()
        {
            Parallelism = 70;
            Process();
        }
        /// <summary>
        /// 万吞吐:并行度:八十
        /// </summary>
        public void Parallel_80_10000()
        {
            Parallelism = 80;
            Process();
        }
        /// <summary>
        /// 万吞吐:并行度:九十
        /// </summary>
        public void Parallel_90_10000()
        {
            Parallelism = 90;
            Process();
        }
        /// <summary>
        /// 万吞吐:并行度:一百
        /// </summary>
        public void Parallel_100_10000()
        {
            Parallelism = 100;
            Process();
        }

    }
}
