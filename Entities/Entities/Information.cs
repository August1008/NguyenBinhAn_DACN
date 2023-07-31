using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class Information
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InformationId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [StringLength(12)]
        public string IdentityCardNumber { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(6)]
        public string Gender { get; set; }

        public int AddressId    { get; set; }
        public Address Address { get; set; }
    }
}
