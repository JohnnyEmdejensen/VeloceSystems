using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeloceCRM.Entity
{
    public class Document : BaseEntity
    {
        [MaxLength(128)]
        public string Name { get; set; } = "";
        [MaxLength(256)]
        public string Subject { get; set; } = "";
        [MaxLength(1024)]
        public string Description { get; set; } = "";
        [MaxLength(1024)]
        public string FilePath { get; set; } = "";
        [MaxLength(16)]
        public string FileType { get; set; } = "";
        public long? FileSize { get; set; }
        public long? CreatedDate { get; set; }
        public long? ModifiedDate { get; set; }
        public long SalespersonId { get; set; }
        public long CompanyId { get; set; }
        public long? PersonId { get; set; }

    }
}
