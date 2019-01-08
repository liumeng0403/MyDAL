using MyDAL.Core.Enums;

namespace MyDAL.Core.Bases
{
    internal class DicBase
    {
        //
        internal int ID { get; set; }
        internal bool IsDbSet { get; set; }
        internal CrudEnum Crud { get; set; }
        internal ActionEnum Action { get; set; }
        internal OptionEnum Option { get; set; }
        internal CompareXEnum Compare { get; set; }
        internal FuncEnum Func { get; set; }

        //
        internal ActionEnum GroupAction { get; set; }
        internal DicBase GroupRef { get; set; }
    }
}
