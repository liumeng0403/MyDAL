namespace MyDAL.Test.NetCore
{
    public class NetCore_2_1
    {
        public async void Test()
        {
            //
            // WhereEdge
            //
            await new WhereEdge._03_WhereBoolDefault().Test();
            await new WhereEdge._04_WhereMethodParam().MethodParam();
            await new WhereEdge._04_WhereMethodParam().MethodListParam();
            await new WhereEdge._05_WherePropertyVariable().Property();
            await new WhereEdge._06_WhereDI().test();
            await new WhereEdge._08_WhereDollarString().test();
            await new WhereEdge._09_WhereMultiCondition().test();
            await new WhereEdge._10_WhereDateTime().test();

            //
            // ShortcutAPI
            //
            new ShortcutAPI._01_FirstOrDefault().Test();
            await new ShortcutAPI._01_QueryOneAsync().test();
            await new ShortcutAPI._02_QueryListAsync().test();
            await new ShortcutAPI._02_QueryListAsync_History().test();
            await new ShortcutAPI._03_MemoryTest().Test();
            await new ShortcutAPI._03_PagingListAsync().test();
            await new ShortcutAPI._07_ExistAsync().Test();
            await new ShortcutAPI._09_CountAsync().Test();

            //
            // QueryVmColumn
            //
            await new QueryVmColumn._01_QueryOneAsync().test();
            await new QueryVmColumn._02_QueryListAsync().test();
            await new QueryVmColumn._02_QueryListAsync_History().test();
            await new QueryVmColumn._03_QueryPagingAsync().test();
            await new QueryVmColumn._03_QueryPagingAsync_History().test();
            await new QueryVmColumn._06_TopAsync().test();

            // 
            // QueryVM
            //
            await new QueryVM._02_QueryListAsync().test();
            await new QueryVM._02_QueryListAsync_History().test();
            await new QueryVM._03_QueryPagingAsync().test();
            await new QueryVM._03_QueryPagingAsync_History().test();
            await new QueryVM._06_TopAsync().test();

            //
            // QuerySingleColumn
            //
            await new QuerySingleColumn._01_QueryOneAsync().test();
            await new QuerySingleColumn._02_QueryListAsync().test();
            await new QuerySingleColumn._02_QueryListAsync_History().test();
            await new QuerySingleColumn._03_QueryPagingAsync().test();
            await new QuerySingleColumn._03_QueryPagingAsync_History().test();
            await new QuerySingleColumn._06_TopAsync().test();

            //
            // QueryM
            //
            //await new QueryM.
            //await new QueryM.
            //await new QueryM.
            //await new QueryM.
            //await new QueryM.
            //await new QueryM.


        }
    }
}
