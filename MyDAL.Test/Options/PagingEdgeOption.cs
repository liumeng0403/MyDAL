namespace MyDAL.Test.Options
{
    public class Single_PagingEdgeOption
        : PagingOption
    {

        // default --> Agent.Phone  Equal
        public string Phone { get; set; }

        // Agent.Phone  Equal
        [XQuery(Column = "Phone",Compare = CompareEnum.Equal)]
        public string PhoneEqual { get; set; }

        // Agent.Phone  NotEqual
        [XQuery(Column = "Phone",Compare = CompareEnum.NotEqual)]
        public string PhoneNotEqual { get; set; }

    }
}
