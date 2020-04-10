using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Common.Models
{
    public class TripDetailsResponse
    {
        public int Id { get; set; }

        public ExpensesTypeResponse ExpensesType { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public decimal Cost { get; set; }

        public string VoucherPath { get; set; }

        public string VoucherFullPath => string.IsNullOrEmpty(VoucherPath)
            ? "https://SoccerWeb4.azurewebsites.net//images/noimage.png"
            : $"https://SoccerWeb4.azurewebsites.net{VoucherPath.Substring(1)}";

    }
}
