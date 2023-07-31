using NguyenBinhAn_DACN.Data;
using System.ComponentModel.DataAnnotations;

namespace NguyenBinhAn_DACN.Areas.Admin.Validations
{
    public class TeacherExist : ValidationAttribute
    {
        private ApplicationDbContext _context;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            _context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if (_context.Teachers.Find(value.ToString()) != null)
            {
                return new ValidationResult("Teacher Exist !");
            }
            return ValidationResult.Success;
        }
    }
}
