using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Web.Data.Entitis
{
    public class CountriesEntity
    {
        public string Id { get; set; }

        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<CitiesEntity> Cities { get; set; }
    }
}
