using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zip.Installments.App.ViewModel
{
    public class OrdersViewModel
    {
        [DisplayName("Enter Total Order Amount ($)")]
        public decimal OrderAmount { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
    }
}
