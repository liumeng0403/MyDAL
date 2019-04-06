using MyDAL.UserFacade.Delete;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using MyDAL.UserFacade.Update;

namespace MyDAL.Interfaces
{
    internal interface IWhereD<M>
        where M : class
    {
        WhereD<M> WHERE { get; }
    }

    internal interface IWhereU<M>
        where M : class
    {
        WhereU<M> WHERE { get;  }
    }

    internal interface IWhereQ<M>
        where M : class
    {
        WhereQ<M> WHERE { get; }
    }

    internal interface IWhereX
    {
        WhereX WHERE { get; }
    }
}
