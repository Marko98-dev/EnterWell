using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnterWell.Models
{
    public class Item
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public int Amount { get; set; }
        public int ProductPrice { get; set; }
        public int TotalPrice { get; set; }
        public Tax TaxType { get; set; }

        public virtual Invoice Invoice { get; set; }
    }

    public enum Tax
    {
        Croatia,
        BiH
    }
}