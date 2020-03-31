using System.ComponentModel.DataAnnotations;

namespace Expenses.Web.Data.Entities
{
    public class CitiesEntity
    {
        public string Id { get; set; }

        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public CountriesEntity Countrie { get; set; }
    }
}
