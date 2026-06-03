using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VeloceCRM.Entity
{
    public class User : BaseEntity
    {
        private string firstname = string.Empty;
        private string? middlename;
        private string surname = string.Empty;

        [MaxLength(64)]
        public string Firstname 
        { 
            get => firstname;
            set
            {
                firstname = value;
                SetFullName();
            }
        }
        [MaxLength(64)]
        public string? Middlename 
        { 
            get => middlename;
            set
            {
                middlename = value;
                SetFullName();
            }
        }
        [MaxLength(64)]
        public string Surname 
        { 
            get => surname;
            set
            {
                surname = value;
                SetFullName();
            }
        }
        [MaxLength(8)]
        public string Initialis { get; set; } = string.Empty;
        [MaxLength(16)]
        public string? Phone { get; set; }
        [MaxLength(16)]
        public string? Mobile { get; set; }
        [MaxLength(256)]
        public string? Email { get; set; }
        public long LocationId { get; set; }
        [MaxLength(64)]
        public string Login { get; set; } = string.Empty;
        [MaxLength(256)]
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool ForceNewPassword { get; set; }

        [NotMapped]
        public string? Fullname { get; set; }
        [NotMapped]
        public string? Authenticationstatus { get; set; }

        private void SetFullName()
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
