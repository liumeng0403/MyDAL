using System;

namespace MyDAL.Test.Options
{
    public class Single_PagingEdgeOption
        : PagingOption
    {

        // default --> Agent.Phone  Equal
        public string Phone { get; set; }

        // Agent.Phone  Equal
        [XQuery(Column = "Phone", Compare = CompareEnum.Equal)]
        public string PhoneEqual { get; set; }

        // Agent.Phone  NotEqual
        [XQuery(Column = "Phone", Compare = CompareEnum.NotEqual)]
        public string PhoneNotEqual { get; set; }

        // Agent.CreatedOn LessThan
        [XQuery(Column = "CreatedOn", Compare = CompareEnum.LessThan)]
        public DateTime CreatedOnLessThan { get; set; }

        // Agent.CreatedOn LessThanOrEqual
        [XQuery(Column = "CreatedOn", Compare = CompareEnum.LessThanOrEqual)]
        public DateTime CreatedOnLessThanOrEqual { get; set; }

        // Agent.CreatedOn GreaterThan
        [XQuery(Column = "CreatedOn", Compare = CompareEnum.GreaterThan)]
        public DateTime CreatedOnGreaterThan { get; set; }

        // Agent.CreatedOn GreaterThanOrEqual
        [XQuery(Column = "CreatedOn", Compare = CompareEnum.GreaterThanOrEqual)]
        public DateTime CreatedOnGreaterThanOrEqual { get; set; }

    }
}
