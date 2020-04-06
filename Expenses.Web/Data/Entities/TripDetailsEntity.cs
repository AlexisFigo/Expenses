using System;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Web.Data.Entities
{
    public class TripDetailsEntity
    {
        public int Id { get; set; }
        [Display(Name = "Expenses type")]
        public ExpensesTypeEntity ExpensesType { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [Display(Name = "Date local")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime DateLocal => Date.ToLocalTime();

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Cost { get; set; }

        [Display(Name = "Voucher")]
        public string VoucherPath { get; set; }

        public TripsEntity trip { get; set; }
    }
}
