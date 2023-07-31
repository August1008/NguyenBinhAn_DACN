using NguyenBinhAn_DACN.Data;
using System.ComponentModel.DataAnnotations;

namespace NguyenBinhAn_DACN.Areas.Admin.Validations
{
    public class StudentExist : ValidationAttribute
    {
        private  ApplicationDbContext _context;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            _context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if(_context.Students.Find(value.ToString()) != null)
            {
                return new ValidationResult("Student Exist !");
            }
            return ValidationResult.Success;
        }
    }
}
