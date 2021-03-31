using Core.DataAccess.EntityFramework;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramewrok
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RecapContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetail(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (RecapContext context = new RecapContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join c1 in context.Colors
                             on c.ColorId equals c1.ColorId
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 Description = c.Description,
                                 BrandName = b.BrandName,
                                 ColorName = c1.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 ColorId = c.ColorId,
                                 BrandId = b.BrandId,
                                 ModelYear = c.ModelYear


                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();


            }
        }

        public List<CarDto> GetCars()
        {
            List<CarDto> result = new List<CarDto>();
            DateTime date = DateTime.Now;
            using (RecapContext context = new RecapContext())
            {

                var colors = context.Colors.ToList();
                var brands = context.Brands.ToList();
                var cars = context.Cars.ToList();
                var rentals = context.Rentals.Where(r => r.RentDate <= date && r.ReturnDate == null).ToList();

                foreach (var i in cars)
                {
                    var model = i.CreateMapped<Car, CarDto>();
                    var color = colors.FirstOrDefault(c => c.ColorId == i.ColorId);
                    var brand = brands.FirstOrDefault(b => b.BrandId == i.BrandId);

                    model.IsRented = rentals.Any(r => r.CarId == i.Id);
                    model.BrandText = brand == null ? "" : brand.BrandName;
                    model.ColorText = color == null ? "" : color.ColorName;

                    result.Add(model);
                }


            }

            return result;
        }
    }
}
