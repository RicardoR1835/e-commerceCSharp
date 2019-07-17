using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace e_Commerce.Models
{
    public class Order
    {
        public int OrderId {get;set;}

        public int AmountOrdered {get;set;}

        public int CustomerId {get;set;}

        public int ProductId {get;set;}

        public Customer CustomerOrdering {get;set;}

        public Product ProductOrdered {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;
       
    }
}