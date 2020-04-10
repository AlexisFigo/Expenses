using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expenses.Common.Models
{
    public class TripResponse
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        public DateTime EndDate { get; set; }

        public DateTime EndDateLocal => EndDate.ToLocalTime();

        public string Description { get; set; }

        public CityResponse City { get; set; }

        public List<TripDetailsResponse> TripDetails { get; set; }

        public decimal Total => TripDetails.Sum(td => td.Cost);
    }
}
