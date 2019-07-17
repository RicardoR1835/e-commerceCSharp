using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace e_Commerce.Models
{
    public class Customer
    {
        public int CustomerId {get;set;}

        public string Name {get;set;}

        public List<Order> Ordered {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;
       
    }
}