using MyDAL.Test;

namespace MyDAL.Test.NetCore
{
    public class NetCore_2_1
    {
        public async void Test()
        {
            //
            // WhereEdge
            //
            await new WhereEdge._01_WhereObject().test();
            await new WhereEdge._02_WhereOption().test();
            await new WhereEdge._03_WhereBoolDefault().test();
            await new WhereEdge._04_WhereMethodParam().MethodParam();
            await new WhereEdge._04_WhereMethodParam().MethodListParam();
            await new WhereEdge._05_WherePropertyVariable().Property();
            await new WhereEdge._06_WhereDI().test();
            await new WhereEdge._07_WhereNULL().WhereTestx();
            await new WhereEdge._08_WhereDollarString().test();
            await new WhereEdge._09_WhereMultiCondition().test();
            await new WhereEdge._10_WhereDateTime().test();

            //
            // ShortcutAPI
            //
            await new ShortcutAPI._01_FirstOrDefault().test();
            await new ShortcutAPI._01_FirstOrDefaultAsync().test();
            await new ShortcutAPI._02_BussinessUnitOption().test();
            await new ShortcutAPI._02_ListAsync().test();
            await new ShortcutAPI._03_MemoryTest().test();
            await new ShortcutAPI._03_PagingListAsync().test();
            await new ShortcutAPI._05_AllAsync().test();
            await new ShortcutAPI._07_ExistAsync().test();
            await new ShortcutAPI._09_CountAsync().test();

            //
            // QueryVmColumn
            //
            await new QueryVmColumn._01_FirstOrDefaultAsync().test();
            //await new QueryVmColumn.
            //await new QueryVmColumn.
            //await new QueryVmColumn.
            //await new QueryVmColumn.
            //await new QueryVmColumn.

        }
    }
}
