using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CustomerApp.Models
{
    public abstract class OrderEmployeeAssigner
    {
        private int LastOrderEmployeeId()
        {
            int ord;
            using (var context = new ApplicationDbContext())
            {
                if (context.Orders.Count() == 0)
                    return context.Employees.FirstOrDefault(x => x.JobPosition == "Administrator").EmployeeId;

                ord = context.Orders
                    .OrderByDescending(x => x.OrderDate)
                    .FirstOrDefault().EmployeeId;
            }

            return ord;
        }

        private List<int> GetEmployeesId()
        {
            List<int> employeesId = null;
            using (var context = new ApplicationDbContext())
            {
                IQueryable<int> employees = context.Employees
                    .Where(x => x.JobPosition == "Administrator")
                    .Select(x => x.EmployeeId);
                employeesId = employees.ToList();
            }

            return employeesId;
        }

        protected int AssignEmployeeToOrder()
        {
            int id = LastOrderEmployeeId();
            List<int> employeesId = GetEmployeesId();
            int index = employeesId.IndexOf(id);
            id = index == employeesId.Count - 1 ? employeesId.First() : employeesId[++index];

            return id;
        }
    }
}