using NguyenBinhAn_DACN.Data;
using System.ComponentModel.DataAnnotations;

namespace NguyenBinhAn_DACN.Areas.Admin.Validations
{
    public class ExistValidation : ValidationAttribute
    {
        private ApplicationDbContext _context;

        protected override ValidationResult IsValid(object value,ValidationContext context)
        {
            _context = (ApplicationDbContext)context.GetService(typeof(ApplicationDbContext));
            if(_context.Subjects.Find(value.ToString()) != null)
            {
                return new ValidationResult("Key exist");
            }
            return ValidationResult.Success;
        }
    }
}
