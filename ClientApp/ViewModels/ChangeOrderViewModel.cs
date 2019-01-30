using ClientApp.Models;
using ClientApp.Views;
using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
        private bool _executing;
        private string _selectedOrderState;
        private int _selectedOrderId;
        internal enum OrderFlags
        {
            Ordered,
            Accepted
        }

        public Action CloseAction { get; set; }
        public DateTime CurrentDate { get; set; }
        public List<OrderedProductsStorage> OrderedProductsList { get => this._orderedProducts; set => this.SetProperty(ref this._orderedProducts, value); }
        public List<string> OrderStates { get => this._orderStates; set => this.SetProperty(ref this._orderStates, value); }
        public List<int> OrderIds { get => this._orderIds; set => this.SetProperty(ref this._orderIds, value); }
        public DateTime DeliveryDate { get => this._deliveryDate; set => this.SetProperty(ref this._deliveryDate, value); }
        public bool IsChangingEnabled { get => this._isChangingEnabled; set => this.SetProperty(ref this._isChangingEnabled, value); }
        public string SelectedOrderState { get => this._selectedOrderState; set => this.SetProperty(ref this._selectedOrderState, value); }
        public int SelectedOrderId
        {
            get => this._selectedOrderId;
            set
            {
                this.SetProperty(ref this._selectedOrderId, value);
                this.SetChangeableOrderedProducts();
                this.SetChangeableDate();
            }
        }
        public bool Executing
        {
            get => this._executing;
            set
            {
                if (SetProperty(ref this._executing, value))
                    AcceptCommand.RaiseCanExecuteChanged();
            }
        }

        public Prism.Commands.DelegateCommand AcceptCommand { get; set; }

        public ChangeOrderViewModel()
        {
            UserCredentials credentials = new UserCredentials();
            this.CurrentDate = credentials.CurrentTime;
            InitValues();
            SetChangeableOrderedProducts();
            AcceptCommand = new Prism.Commands.DelegateCommand(UpdateOrder, () => !Executing);            
        }

        public void UpdateOrder()
        {
            Executing = true;
            if (this.SelectedOrderState == OrderFlags.Ordered.ToString())
            {
                System.Windows.MessageBox.Show("Cannot set delivery date for order state \"ordered\"", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                Executing = false;
            }
            else
            {
                ProductsRepository productsRepository = new ProductsRepository();
                InvoiceGenerator invoiceGenerator = new InvoiceGenerator();
                InvoiceComponents invoiceComponents = new InvoiceComponents();
                Invoices invoice = new Invoices();
                invoice.InvoiceDate = this.CurrentDate;
                UpdatedOrder updatedOrder = new UpdatedOrder()
                {
                    OrderId = this.SelectedOrderId,
                    DeliveryDate = this.DeliveryDate,
                    OrderState = this.SelectedOrderState
                };
                                
                List<OrderedProductsStorage> ordProducts = OrderedProductsList;

                using (var context = new ApplicationDbContext())
                {
                    IQueryable<InvoiceComponents> cData = context.Orders
                        .Where(x => x.OrderId == this.SelectedOrderId)
                        .Include(x => x.Regions)
                        .Include(x => x.Customers)
                        .Select(x => new InvoiceComponents
                        {
                            CustomerCompanyName = x.Customers.CompanyName,
                            CustomerCountry = x.Regions.Country,
                            CustomerCity = x.Regions.City,
                            CustomerStreet = x.Regions.Street
                        });

                    invoiceComponents = cData.FirstOrDefault();
                }
                
                orderModifier.UpdateOrder(updatedOrder);
                invoiceGenerator.CreateInvoice(ordProducts, invoiceComponents, invoice, updatedOrder.OrderId);
                productsRepository.UpdateProducts(updatedOrder.OrderId);

                Executing = false;
                CloseAction();
            }
            Executing = false;
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

        private void SetChangeableDate()
        {
            if (this.SelectedOrderState != OrderFlags.Ordered.ToString())
                this.DeliveryDate = orderModifier.GetDeliveryDate(this.SelectedOrderId);
            else
                this.DeliveryDate = this.CurrentDate;
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
