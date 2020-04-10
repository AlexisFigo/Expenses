
using System.ComponentModel.DataAnnotations;

namespace Expenses.Common.Models
{
    public class TripRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
