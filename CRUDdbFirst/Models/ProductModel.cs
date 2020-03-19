using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUDdbFirst.Models
{
    public class ProductModel
    {
        [Display (Name ="Product ID")]
        [Required(ErrorMessage ="This Field is Required")]
        public int ProductID { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "This Field is Required")]
        public string ProductName { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = "This Field is Required")]
        public decimal Price { get; set; }
        [Display(Name = "Count")]
        [Required(ErrorMessage = "This Field is Required")]
        public int count { get; set; }
    }
}