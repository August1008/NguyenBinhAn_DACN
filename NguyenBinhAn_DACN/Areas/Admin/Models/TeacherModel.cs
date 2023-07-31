using NguyenBinhAn_DACN.Areas.Admin.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace NguyenBinhAn_DACN.Areas.Admin.Models
{
    public class TeacherModel
    {
        [Display(Name ="Teacher Code")]
        [TeacherExist]
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public string EducationLevel { get; set; }
        public int LevelId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityCardNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string WardId { get; set; }
        public string DistrictId { get; set; }
        public string ProvinceId { get; set; }

        public string Address { get; set; }
        public string getPasswordFromBirthDay()
        {
            return "" + BirthDay.Day + "" + (BirthDay.Month >= 10 ? BirthDay.Month.ToString():"0"+BirthDay.Month.ToString()) + "" + BirthDay.Year;
        }
        public string BirthDayString { get; set; }
    }
}
