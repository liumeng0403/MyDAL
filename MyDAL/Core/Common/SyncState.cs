using System;
using System.Threading;

namespace MyDAL.Core.Common
{
    internal class SyncState
        : ClassInstance<SyncState>
    {
        //
        public SyncState()
        {
            var seedStr = DateTime.Now.Ticks.ToString();
            var seed = seedStr.Substring(seedStr.Length - 9, 9);
            _MockClient = new Random(Convert.ToInt32(seed)).Next(1, 100);
        }

        //
        internal int Start { get; set; }
        internal int LimitI { get; set; }
        internal TimeSpan Elapsed { get; set; }

        //
        internal string Result { get; set; }
        internal bool SuccessFlag { get; set; }

        //
        internal bool Wait { get; set; }
        internal SyncState WaitFlag { get; set; }
        internal void WaitSimple()
        {
            Thread.Sleep(1);
        }

        //
        private int _MockClient { get; set; }
        internal void MockClientSimple()
        {
            Thread.Sleep(this._MockClient);
        }
    }
}
