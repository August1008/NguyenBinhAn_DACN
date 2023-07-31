using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class ClassModel
    {
        public int ClassId { get; set; }
        [Display(Name="Class")]
        public string Name { get; set; }
    }
}
