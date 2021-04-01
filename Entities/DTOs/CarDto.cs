using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CarDto : Car, IDto
    {
        public string BrandText { get; set; }
        public string ColorText { get; set; }
        public bool IsRented { get; set; }
        public string ReturnDate { get; set; }

    }
}
