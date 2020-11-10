using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnterWell.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }

        public virtual ICollection<Item> Items{ get; set; }
        // public List<Item> Products { get; set; } = new List<Item>();

        public int TotalPrice { get; set; }
        public float TotalPriceTax { get; set; }
        public string CreatedBy { get; set; }
        public string CustomerName { get; set; }
    }
}