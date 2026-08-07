using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Entity
{
    public class CanvasCaseCompanyLink : BaseEntity
    {
        public long CanvasCaseId { get; set; }
        public long CompanyId { get; set; }

    }
}
