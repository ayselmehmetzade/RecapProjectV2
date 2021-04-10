using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public string creditCardNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal Price { get; set; }
    }
}

