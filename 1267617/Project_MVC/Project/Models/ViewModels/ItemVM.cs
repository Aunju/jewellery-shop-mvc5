using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models.ViewModels
{
    public class ItemVM
    {
        public int Id { get; set; }
        [Required, StringLength(50), Display(Name = "Name")]
        public string ItemName { get; set; }
        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StoreDate { get; set; }
        [Display(Name = "Picture")]
        public string PicturePath { get; set; }
        //fk
        [ForeignKey("Category"), Display(Name = "Category")]
        public int CategoryId { get; set; }
        public bool Available { get; set; }
        public HttpPostedFileBase Picture { get; set; }
    }
}