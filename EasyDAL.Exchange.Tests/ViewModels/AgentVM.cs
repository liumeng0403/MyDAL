using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Tests.ViewModels
{
    public class AgentVM
    {

        // query
        /****************************************************************************************************/

        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid UserId { get; set; }
        public string PathId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        
        // join
        /****************************************************************************************************/
        
        public string nn { get; set; }
        public Guid yy { get; set; }
        public Guid xx { get; set; }
        public string zz { get; set; }
        public int mm { get; set; }

        // none
        /****************************************************************************************************/

        public string XXXX { get; set; }
        public string YYYY { get; set; }

    }
}
