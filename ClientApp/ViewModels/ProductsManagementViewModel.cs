using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class ProductsManagementViewModel : Prism.Mvvm.BindableBase, IPageViewModel
    {
        private bool _executing;
        private string _name;
        private string _quantity;
        private string _unitPrice;
        private string _quantityFrom;
        private string _quantityTo;
        private string _unitPriceFrom;
        private string _unitPriceTo;
        private List<Model.Products> _outputList;

        public List<Model.Products> OutputProductsList { get => this._outputList; set => this.SetProperty(ref _outputList, value); }
        public string ProductName { get => this._name; set => this.SetProperty(ref _name, value); }
        public string ProductQuantity { get => this._quantity; set => this.SetProperty(ref _quantity, value); }
        public string ProductUnitPrice { get => this._unitPrice; set => this.SetProperty(ref _unitPrice, value); }
        public string ProductQuantityFrom { get => this._quantityFrom; set => this.SetProperty(ref _quantityFrom, value); }
        public string ProductQuantityTo { get => this._quantityTo; set => this.SetProperty(ref _quantityTo, value); }
        public string ProductUnitPriceFrom { get => this._unitPriceFrom; set => this.SetProperty(ref _unitPriceFrom, value); }
        public string ProductUnitPriceTo { get => this._unitPriceTo; set => this.SetProperty(ref _unitPriceTo, value); }

        public Prism.Commands.DelegateCommand GetProductsCommand { get; set; }
        public Prism.Commands.DelegateCommand ClearValuesCommand { get; set; }

        public string Name => "Products Management";

        public bool Executing
        {
            get => this._executing;
            set
            {
                if (SetProperty(ref this._executing, value))
                    GetProductsCommand.RaiseCanExecuteChanged();
            }
        }

        public ProductsManagementViewModel()
        {
            GetProductsCommand = new Prism.Commands.DelegateCommand(SetDataGrid, () => !Executing);
            ClearValuesCommand = new Prism.Commands.DelegateCommand(ClearValues, () => !Executing);
        }

        public async void SetDataGrid()
        {
            Executing = true;

            Model.Products product = new Model.Products();
            WcfService.ProductExtension productExtension = new WcfService.ProductExtension();

            Validations.Parser parser = new Validations.Parser();
            parser.ParseInput(product, productExtension, this.ProductName, this.ProductQuantity, this.ProductUnitPrice, this.ProductQuantityFrom,
                this.ProductQuantityTo, this.ProductUnitPriceFrom, this.ProductUnitPriceTo);

            try
            {
                using (var client = new WcfService.Service1Client())
                {
                    OutputProductsList = await client.GetProductsAsync(product, productExtension);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "", System.Windows.MessageBoxButton.OK);
            }

            Executing = false;
        }

        public void ClearValues()
        {
            Executing = true;

            this.ProductName = null;
            this.ProductQuantity = null;
            this.ProductQuantityFrom = null;
            this.ProductQuantityTo = null;
            this.ProductUnitPrice = null;
            this.ProductUnitPriceFrom = null;
            this.ProductUnitPriceTo = null;
            //this.OutputProductsList = null;

            Executing = false;
        }
    }
}
