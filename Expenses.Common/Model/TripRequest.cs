
using System.ComponentModel.DataAnnotations;

namespace Expenses.Common.Model
{
    public class TripRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
