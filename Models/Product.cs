using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace e_Commerce.Models
{
    public class Product
    {
        public int ProductId {get;set;}

        public string Name {get;set;}

        public string ImageUrl {get;set;}

        public string Description {get;set;}

        public int Quantity {get;set;}

        public List<Order> Ordered {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}