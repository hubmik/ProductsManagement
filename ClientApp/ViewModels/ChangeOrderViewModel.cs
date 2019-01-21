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
        OrderModifier orderModifier = new OrderModifier();
        private List<OrderedProductsStorage> _orderedProducts;
        private List<string> _orderStates;
        private List<int> _orderIds;
        private DateTime _deliveryDate;
        private bool _isChangingEnabled;
        private string _selectedOrderState;
        private int _selectedOrderId;
        internal enum OrderFlags
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
        public bool IsChangingEnabled { get => this._isChangingEnabled; set => this.SetProperty(ref this._isChangingEnabled, value); }
        public string SelectedOrderState
        {
            get => this._selectedOrderState;
            set
            {
                this.SetProperty(ref this._selectedOrderState, value);
            }
        }

        public int SelectedOrderId
        {
            get => this._selectedOrderId;
            set
            {
                this.SetProperty(ref this._selectedOrderId, value);
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
            ProductsRepository productsRepository = new ProductsRepository();

            UpdatedOrder updatedOrder = new UpdatedOrder()
            {
                OrderId = this.SelectedOrderId,
                DeliveryDate = this.DeliveryDate,
                OrderState = this.SelectedOrderState
            };
            orderModifier.UpdateOrder(updatedOrder);
            //orderedProductsList = productsRepository.QueryUpdateProducts(updatedOrder.OrderId);
            productsRepository.UpdateProducts(updatedOrder.OrderId);
            CloseAction();
        }

        private void InitValues()
        {
            this.DeliveryDate = this.CurrentDate;
            this.OrderIds = orderModifier.GetOrderIds(UserCredentials.SessionKey);
            this.SelectedOrderId = this.OrderIds.FirstOrDefault();
            this.OrderStates = orderModifier.GetOrderStates(this.SelectedOrderId);
            this.SelectedOrderState = orderModifier.GetStateOfSpecifiedOrder(this.SelectedOrderId);
            this.IsChangingEnabled = CanChangeValue();
        }

        private void SetChangeableOrderedProducts()
        {
            this.SelectedOrderState = orderModifier.SelectStateForSpecifiedOrder(this.SelectedOrderId);
            this.OrderStates = orderModifier.GetOrderStates(this.SelectedOrderId);
            this.OrderedProductsList = orderModifier.GetOrderedProducts(this.SelectedOrderId);
            this.IsChangingEnabled = orderModifier.GetStateOfSpecifiedOrder(this.SelectedOrderId) != OrderFlags.Ordered.ToString() 
                ? false : true;
        }
        private bool CanChangeValue()
        {
            if (this.SelectedOrderState != OrderFlags.Ordered.ToString())
                return false;

            return true;
        }
    }
}
