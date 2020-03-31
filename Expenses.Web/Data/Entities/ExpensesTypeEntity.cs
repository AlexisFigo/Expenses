using System.ComponentModel.DataAnnotations;

namespace Expenses.Web.Data.Entities
{
    public class ExpensesTypeEntity
    {
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }
    }
}
