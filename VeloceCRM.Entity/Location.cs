using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VeloceCRM.Entity
{
    public class Location : BaseEntity
    {
        private string street = string.Empty;
        private string house = string.Empty;
        private string? floor;
        private string? door;

        [MaxLength(128)]
        public string Street 
        { 
            get => street;
            set
            {
                street = value;
            }
        }
        [MaxLength(16)]
        public string House 
        { 
            get => house;
            set
            {
                house = value;
            }
        }
        [MaxLength(16)]
        public string? Floor 
        { 
            get => floor;
            set
            {
                floor = value;
            }
        }
        [MaxLength(16)]
        public string? Door 
        { 
            get => door;
            set
            {
                door = value;
            }
        }
        public long PostalzoneId { get; set; }
        [NotMapped]
        public string? Address { get; set; }
        [NotMapped]
        public Entity.Postalzone? Postalzone { get; set; }

        public void SetAddress()
        {
            Address = street + " " + house;
            if (floor != null && door != null)
            {
                Address += ", " + floor + ". " + door;
            }
            if (floor != null && door == null)
            {
                Address += ", " + floor + ". sal";
            }
        }
    }   
}
