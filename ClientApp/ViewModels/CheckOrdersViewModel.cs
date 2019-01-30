using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel;
using ClientApp.Models;
using ClientApp.Views;

namespace ClientApp.ViewModels
{
    public class CheckOrdersViewModel : Prism.Mvvm.BindableBase, IPageViewModel
    {
        public event EventHandler<SelectedDateEventArgs> Date;
        ChangeOrderView ChangeOrderView { get; set; }
        
        private List<Orders> _ordersList;
        private DateTime _orderDeliveryDate;

        public DateTime StartDate { get => DateTime.UtcNow; }
        public DateTime OrderDeliveryDate { get => this._orderDeliveryDate; set => this.SetProperty(ref this._orderDeliveryDate, value); }
        public List<Orders> OrdersList { get => this._ordersList; set => this.SetProperty(ref this._ordersList, value); }
        public UpdatedOrder UpdatedOrderValues { get; set; }
        public string Name => "Check Orders";

        public Prism.Commands.DelegateCommand ChangeOrderStateCommand { get; set; }
        public Prism.Commands.DelegateCommand ChangeDateCommand { get; set; }

        public CheckOrdersViewModel()
        {
            ChangeOrderStateCommand = new Prism.Commands.DelegateCommand(UpdateOrder);
            InitOrders();
        }

        public void InitOrders()
        {
            UsersImplements usersImplements = new UsersImplements();
            List<Orders> ordersList = usersImplements.OrdersForSpecifiedEmployee(UserCredentials.SessionKey);

            foreach (var item in ordersList)
            {
                if(item.DeliveryDate.HasValue)
                    item.DeliveryDate = DateTime.Parse(string.Format("{0:MM/dd/yyyy}", item.DeliveryDate));
            }

            this.OrdersList = ordersList;
        }

        protected virtual void OnDeliveryDateChange(SelectedDateEventArgs e)
        {
            Date?.Invoke(this, e);
        }

        public void UpdateOrder()
        {
            this.ChangeOrderView = new ChangeOrderView();
            this.ChangeOrderView.ShowDialog();
        }
    }
}
