using ClientApp.Models;
using ClientApp.Views;
using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class ChangeOrderViewModel : Prism.Mvvm.BindableBase
    {
        private List<OrderedProductsStorage> _orderedProducts;
        private List<string> _orderStates;
        private List<int> _orderIds;
        private DateTime _deliveryDate;
        private string _selectedOrderState;
        private int _selectedId;
        private enum OrderFlags
        {
            Ordered,
            Accepted,
            Done
        }

        public Action CloseAction { get; set; }
        public DateTime CurrentDate { get; set; }
        public List<OrderedProductsStorage> OrderedProductsList { get => this._orderedProducts; set => this.SetProperty(ref this._orderedProducts, value); }
        public List<string> OrderStates { get => this._orderStates; set => this.SetProperty(ref this._orderStates, value); }
        public List<int> OrderIds { get => this._orderIds; set => this.SetProperty(ref this._orderIds, value); }
        public DateTime DeliveryDate { get => this._deliveryDate; set => this.SetProperty(ref this._deliveryDate, value); }
        public string SelectedOrderState { get => this._selectedOrderState; set => this.SetProperty(ref this._selectedOrderState, value); }
        public int SelectedId
        {
            get => this._selectedId; set
            {
                this.SetProperty(ref this._selectedId, value);
                this.SetChangeableOrderedProducts();
            }
        }

        public Prism.Commands.DelegateCommand AcceptCommand { get; set; }

        public ChangeOrderViewModel()
        {
            UserCredentials credentials = new UserCredentials();
            this.CurrentDate = credentials.CurrentTime;
            InitValues();
            SetChangeableOrderedProducts();
            AcceptCommand = new Prism.Commands.DelegateCommand(UpdateOrder);            
        }

        public void UpdateOrder()
        {
            UpdatedOrder updatedOrder = new UpdatedOrder()
            {
                OrderId = this.SelectedId,
                DeliveryDate = this.DeliveryDate,
                OrderState = this.SelectedOrderState
            };
            CheckOrdersViewModel checkOrdersVM = new CheckOrdersViewModel(updatedOrder);
            CloseAction();
        }

        private void InitValues()
        {
            OrderModifier orderModifier = new OrderModifier();
            this.DeliveryDate = this.CurrentDate;
            this.OrderIds = orderModifier.GetOrderIds(UserCredentials.SessionKey);
            this.OrderStates = orderModifier.GetOrderStates();
            this.SelectedOrderState = OrderFlags.Ordered.ToString();
            this.SelectedId = this.OrderIds.FirstOrDefault();
        }

        private void SetChangeableOrderedProducts()
        {
            OrderModifier orderModifier = new OrderModifier();
            this.OrderedProductsList = orderModifier.GetOrderedProducts(this.SelectedId);
        }
    }
}
