using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Models
{
    public class DocumentView
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Description { get; set; } = "";
        public string FilePath { get; set; } = "";
        public string FileType { get; set; } = "";
        public long? FileSize { get; set; }
        public long? CreatedDate { get; set; }
        public long? ModifiedDate { get; set; }
        public long SalespersonId { get; set; }
        public long CompanyId { get; set; }
        public long? PersonId { get; set; }

        public string SalespersonName { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string PersonName { get; set; } = "";
    }
}
