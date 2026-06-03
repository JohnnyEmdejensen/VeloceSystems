using System.ComponentModel.DataAnnotations;

namespace VeloceCRM.Entity
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public long Created { get; set; }
        public long? Updated { get; set; }
        [MaxLength(64)]
        public string LicenseKey { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

    }
}
