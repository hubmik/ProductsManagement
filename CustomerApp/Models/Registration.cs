using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApp.Models
{
    public class Registration
    {
        public bool SignUp(int id, RegisterViewModel viewModel)
        {
            List<Regions> list = null;
            using (var context = new ApplicationDbContext())
            {
                list = context.Regions.ToList();
                foreach (var item in list)
                {
                    if (item.Country == viewModel.Country && item.City == viewModel.City && item.Street == viewModel.Street)
                        return false;
                }
                Regions region = new Regions();
                region.Customers = new Customers();
                region.Customers.CompanyName = viewModel.CompanyName;
                region.Customers.UserId = id;
                region.Country = viewModel.Country;
                region.City = viewModel.City;
                region.Street = viewModel.Street;
                region.IsDefault = true;
                context.Regions.Add(region);
                context.SaveChanges();

                return true;
            }
        }
    }
}