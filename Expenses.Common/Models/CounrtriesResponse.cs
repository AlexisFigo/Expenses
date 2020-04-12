using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Common.Models
{
    public class CounrtriesResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<CityResponse> Cities { get; set; }
    }
}
