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

        private List<WcfService.OrderContext> _ordersList;
        private DateTime _orderDeliveryDate;

        public DateTime StartDate { get => DateTime.UtcNow; }
        public DateTime OrderDeliveryDate { get => this._orderDeliveryDate; set => this.SetProperty(ref this._orderDeliveryDate, value); }
        public List<WcfService.OrderContext> OrdersList { get => this._ordersList; set => this.SetProperty(ref this._ordersList, value); }
        public UpdatedOrder UpdatedOrderValues { get; set; }
        public string Name => "Check Orders";

        public Prism.Commands.DelegateCommand ChangeOrderStateCommand { get; set; }
        public Prism.Commands.DelegateCommand ChangeDateCommand { get; set; }

        public CheckOrdersViewModel()
        {
            ChangeOrderStateCommand = new Prism.Commands.DelegateCommand(UpdateOrder);
            InitOrders();
        }

        public CheckOrdersViewModel(UpdatedOrder updatedValues)
        {
            UpdatedOrder order = updatedValues;            
            UpdatedOrderValues = order;
        }

        public async void InitOrders()
        {
            List<WcfService.OrderContext> ordersList = null;
            UsersImplements usersImplements = new UsersImplements();

            ordersList = await usersImplements.OrdersForEmployees();
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
