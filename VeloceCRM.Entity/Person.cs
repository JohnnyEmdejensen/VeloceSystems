using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VeloceCRM.Entity
{
    public class Person : BaseEntity
    {
        [MaxLength(64)]
        public string Firstname { get; set; } = string.Empty;
        [MaxLength(64)]
        public string? Middlename { get; set; }
        [MaxLength(64)]
        public string Surname { get; set; } = string.Empty;
        [MaxLength(16)]
        public string Initials { get; set; } = string.Empty;
        [MaxLength(16)]
        public string? Phone { get; set; }
        [MaxLength(16)]
        public string Mobile { get; set; } = string.Empty;
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;
        public long CompanyId { get; set; }
        public long LocationId { get; set; }
        public long TitleId { get; set; }
        public byte[]? Image { get; set; }

        [NotMapped]
        public string? Fullname {  get; set; }
        [NotMapped]
        public Entity.Location? Location { get; set; }
        public void SetFullName()
        {
            if (string.IsNullOrEmpty(Middlename))
            {
                Fullname = $"{Firstname} {Surname}";
            }
            else
            {
                Fullname = $"{Firstname} {Middlename} {Surname}";
            }
        }
    }
}
