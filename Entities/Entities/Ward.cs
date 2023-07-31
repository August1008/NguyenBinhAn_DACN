using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Entities
{
    public class Ward
    {
        [Key]
        [StringLength(6)]
        public string WardId { get; set; }
        [StringLength(1228)]
        public string Name { get; set; }
        public string DistrictId { get; set; }
        public District District { get; set; }
    }
}
