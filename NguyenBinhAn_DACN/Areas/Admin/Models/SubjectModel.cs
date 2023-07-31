using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenBinhAn_DACN.Areas.Admin.Validations;
using NguyenBinhAn_DACN.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenBinhAn_DACN.Areas.Admin.Models
{
    public class SubjectModel
    {
        [Required]
        [ExistValidation]
        [Display(Name ="Subject Code")]
        public string SubjectId { get; set; }
        [Display(Name ="Subject Name")]
        public string SubjectName { get; set; }
        public int NumberOfLesson { get; set; }
        public int Credit { get; set; }

        public SubjectModel()
        {
            
        }
        public int setLessonByCredit()
        {
            switch (Credit)
            {
                case 1: return 6;
                case 2: return 7;
                case 3: return 9;
            }
            return 0;
        }
    }
}
