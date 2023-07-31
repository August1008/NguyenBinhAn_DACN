using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Entities
{
    public class Province
    {
        [Key]
        [StringLength(6)]
        public string ProvinceId { get; set; }
        [StringLength(128)]
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; }
    }
}
