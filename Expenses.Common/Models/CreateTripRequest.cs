using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Common.Models
{
    public class CreateTripRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string CityId { get; set; }

        public string UserId { get; set; }

        public string CultureInfo { get; set; }
    }
}
