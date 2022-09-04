using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models.ViewModels
{
    public class EmployeeRetriveVM
    {
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        [Required, StringLength(40), Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        //[Required, StringLength(150)]
        public string Address { get; set; }
        [Required, StringLength(4), Display(Name = "Post Code")]
        public string PostCode { get; set; }
        public string Image { get; set; }

    }
}