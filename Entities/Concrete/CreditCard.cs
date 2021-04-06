﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CreditCard : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string CardNumber { get; set; }
        public byte ExpiryMonth { get; set; }
        public byte ExpiryYear { get; set; }
        public string Cvc { get; set; }
        public string Type { get; set; }
    }
}