using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClientApp.ViewModels
{
    public class CheckOrdersViewModel : Prism.Mvvm.BindableBase, IPageViewModel
    {
        private List<WcfService.OrderContext> _ordersList;

        public List<WcfService.OrderContext> OrdersList { get => this._ordersList; set => this.SetProperty(ref this._ordersList, value); }
        public string Name => "Check Orders";

        public CheckOrdersViewModel()
        {
            InitOrders();
        }

        public async void InitOrders()
        {
            List<WcfService.OrderContext> ordersList = null;
            IQueryable<Orders> query = null;
            string s = Models.UserCredentials.SessionKey;
            
            Models.UsersImplements usersImplements = new Models.UsersImplements();
            ordersList = await usersImplements.OrdersForEmployees();
            this.OrdersList = ordersList;
            //ordersList = await usersImplements.OrdersForEmployees();
            //this.OrdersList = ordersList;
        }
    }
}
