using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using e_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Controllers
{
    
    public class HomeController : Controller
    {
        private HomeContext dbContext;
     
        // here we can "inject" our context service into the constructorcopy
        public HomeController(HomeContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            var AllProducts = dbContext.Products
            .ToList();
            var AllOrders = dbContext.Orders
            .Include(ord => ord.CustomerOrdering)
            .Include(ord => ord.ProductOrdered)
            .ToList();
            var AllCustomers = dbContext.Customers.ToList();
            ViewBag.Products = AllProducts;
            ViewBag.Orders = AllOrders;
            ViewBag.Customers = AllCustomers;
            return View();
        }

        [HttpGet("products")]
        public IActionResult Products()
        {
            var Products = dbContext.Products
            .ToList();
            ViewBag.Products = Products;
            return View("Products");
        }

        [HttpPost("create/product")]
        public IActionResult CreateProduct(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
            }
            var Products = dbContext.Products
            .ToList();
            ViewBag.Products = Products;
            return View("Products");
        }

        [HttpGet("orders")]
        public IActionResult Orders()
        {
            var AllOrders = dbContext.Orders
            .Include(ord => ord.CustomerOrdering)
            .ThenInclude(cust => cust.Ordered)
            .Include(ord => ord.ProductOrdered)
            .ThenInclude(prod => prod.Ordered)
            .ToList();
            var Customers = dbContext.Customers
            .ToList();
            var Products = dbContext.Products
            .ToList();
            ViewBag.Orders = AllOrders;
            ViewBag.Customers = Customers;
            ViewBag.Products = Products;
            return View("Order");
        }

        [HttpPost("create/order")]
        public IActionResult CreateOrder(Order formOrder)
        {
            if (ModelState.IsValid)
            {
                Order newOrder = new Order();
                Customer thisCustomer = dbContext.Customers
                .FirstOrDefault(cust => cust.CustomerId == formOrder.CustomerId);
                Product thisProduct = dbContext.Products
                .FirstOrDefault(prod => prod.ProductId == formOrder.ProductId);
                newOrder.CustomerId = thisCustomer.CustomerId;
                newOrder.ProductId = thisProduct.ProductId;
                newOrder.CustomerOrdering = thisCustomer;
                newOrder.ProductOrdered = thisProduct;
                newOrder.AmountOrdered = formOrder.AmountOrdered;
                if((thisProduct.Quantity - newOrder.AmountOrdered) < 0)
                {
                    ModelState.AddModelError("AmountOrdered", $"There is only {thisProduct.Quantity} left, order less.");
                    var Orders = dbContext.Orders
                    .Include(ord => ord.CustomerOrdering)
                    .ThenInclude(cust => cust.Ordered)
                    .Include(ord => ord.ProductOrdered)
                    .ThenInclude(prod => prod.Ordered)
                    .ToList();
                    var Custs = dbContext.Customers
                    .ToList();
                    var Prods = dbContext.Products
                    .ToList();
                    ViewBag.Orders = Orders;
                    ViewBag.Customers = Custs;
                    ViewBag.Products = Prods;
                    return View("Order");
                }
                thisProduct.Quantity -= newOrder.AmountOrdered;
                dbContext.Add(newOrder);
                dbContext.SaveChanges();
                return RedirectToAction("Orders");
            }
            var AllOrders = dbContext.Orders
            .Include(ord => ord.CustomerOrdering)
            .ThenInclude(cust => cust.Ordered)
            .Include(ord => ord.ProductOrdered)
            .ThenInclude(prod => prod.Ordered)
            .ToList();
            var Customers = dbContext.Customers
            .ToList();
            var Products = dbContext.Products
            .ToList();
            ViewBag.Orders = AllOrders;
            ViewBag.Customers = Customers;
            ViewBag.Products = Products;
            return View("Order");
        }

        [HttpGet("customers")]
        public IActionResult Customers()
        {
            var Customers = dbContext.Customers
            .ToList();
            ViewBag.Customers = Customers;
            return View("Customers");
        }

        [HttpPost("create/customer")]
        public IActionResult CreateCustomer(Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newCustomer);
                dbContext.SaveChanges();
                return RedirectToAction("Customers");
            }
            var Customers = dbContext.Customers
            .ToList();
            ViewBag.Customers = Customers;
            return View("Customers");
        }

        [HttpGet("destroy/customer/{id}")]
        public IActionResult DestroyCustomer(int id)
        {
            Customer thisCustomer = dbContext.Customers
            .FirstOrDefault(cust => cust.CustomerId == id);
            dbContext.Remove(thisCustomer);
            dbContext.SaveChanges();
            return RedirectToAction("Customers");
        }

    }
}
