using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Entities
{
    public class District
    {
        [Key]
        [StringLength(6)]
        public string DistrictId { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        public string ProvinceId { get; set; }
        public Province Province { get; set; }
        public ICollection<Ward> Wards { get; set; }

    }
}
