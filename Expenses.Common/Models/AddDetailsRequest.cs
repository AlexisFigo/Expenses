using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Common.Models
{
    public class AddDetailsRequest
    {
     
        public string ExpensesTypeId { get; set; }

        public DateTime Date { get; set; }

        public decimal Cost { get; set; }

        public byte[] VoucherPath { get; set; }

        public string TripId { get; set; }

        public string CultureInfo { get; set; }
    }
}
