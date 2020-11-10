using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnterWell.Models;
using EnterWell.Models.Services;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;

namespace EnterWell.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        
        //[Import(typeof(ITax))]
        //public ITax _CalculateTax;

        // GET: Invoice
        public ActionResult Index()
        {
            return View(db.Invoices.ToList());
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductName, Amount, ProductPrice, TotalPrice, TaxType")] Item item)
        {
            Invoice invoice = new Invoice();
            try
            {
                if (ModelState.IsValid)
                {
                    item.TotalPrice = item.Amount * item.ProductPrice;
                    invoice.CreatedAt = DateTime.UtcNow;
                    invoice.CreatedBy = System.Web.HttpContext.Current.User.Identity.GetUserName();

                    invoice.TotalPrice = item.TotalPrice;
                    invoice.DueDate = DateTime.UtcNow;

                    string selectedValue = Request.Form["TaxType"].ToString();


                    if (selectedValue == "Croatia")
                    {
                        invoice.TotalPriceTax = (float)invoice.TotalPrice * 1.25f;
                    }
                    else
                    {
                        invoice.TotalPriceTax = (float)invoice.TotalPrice * 1.17f;
                    }

                    // _CalculateTax.CalculateTax(selectedValue);

                    List<Item> list = new List<Item>();
                    list.Add(item);
                    invoice.Items = list;
                    
                    db.Invoices.Add(invoice);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                string jsonException = JsonConvert.SerializeObject(exception);
                ModelState.AddModelError("", jsonException);
            }

            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public ActionResult Add(int? Id)
        {
            if (Id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(Id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "ProductName, Amount, ProductPrice, TotalPrice, TaxType")] Item item)
        {
            Invoice invoice = new Invoice();
            try
            {
                if (ModelState.IsValid)
                {
                    //List<Item> list = new List<Item>();
                    //list.Add(item);
                    //invoice.Items = list;

                    db.Items.Add(item);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                string jsonException = JsonConvert.SerializeObject(exception);
                ModelState.AddModelError("", jsonException);
            }
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Invoice invoice = db.Invoices.Find(id);
                db.Invoices.Remove(invoice);
                db.SaveChanges();
            }
            catch (Exception exception)
            {
                string jsonException = JsonConvert.SerializeObject(exception);
                ModelState.AddModelError("", jsonException);
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
