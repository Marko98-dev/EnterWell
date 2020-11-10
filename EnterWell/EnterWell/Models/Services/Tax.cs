using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterWell.Models.Services
{
    [Export(typeof(ITax))]
    public class Tax : ITax
    {
        public decimal CalculateTax(string selectedValue)
        {
            Invoice invoice = new Invoice();

            if (selectedValue == "Croatia")
            {
                invoice.TotalPriceTax = (float)invoice.TotalPrice * 1.25f;
            }
            else
            {
                invoice.TotalPriceTax = (float)invoice.TotalPrice * 1.17f;
            }
            throw new NotImplementedException();
        }
    }
}