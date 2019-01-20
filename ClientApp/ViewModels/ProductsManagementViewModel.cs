﻿using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class ProductsManagementViewModel : Prism.Mvvm.BindableBase, IPageViewModel
    {
        Models.ProductsRepository productsRepository;
        private bool _executing;
        private string _name;
        private string _quantity;
        private string _unitPrice;
        private string _quantityFrom;
        private string _quantityTo;
        private string _unitPriceFrom;
        private string _unitPriceTo;
        private int _selectedCollectionSize;
        private string _insertedProductName;
        private string _insertedProductPrice;
        private string _insertedProductQuantity;
        private List<Products> _outputList;
        private int _selectedCollectionSizeToUpdate;
        private string _selectedProduct;
        private int _updateQuantity;
        private decimal _updateUnitPrice;

        public List<Products> OutputProductsList { get => this._outputList; set => this.SetProperty(ref _outputList, value); }
        public string ProductName { get => this._name; set => this.SetProperty(ref _name, value); }
        public string ProductQuantity { get => this._quantity; set => this.SetProperty(ref _quantity, value); }
        public string ProductUnitPrice { get => this._unitPrice; set => this.SetProperty(ref _unitPrice, value); }
        public string ProductQuantityFrom { get => this._quantityFrom; set => this.SetProperty(ref _quantityFrom, value); }
        public string ProductQuantityTo { get => this._quantityTo; set => this.SetProperty(ref _quantityTo, value); }
        public string ProductUnitPriceFrom { get => this._unitPriceFrom; set => this.SetProperty(ref _unitPriceFrom, value); }
        public string ProductUnitPriceTo { get => this._unitPriceTo; set => this.SetProperty(ref _unitPriceTo, value); }
        public int SelectedCollectionSize { get => this._selectedCollectionSize; set => this.SetProperty(ref _selectedCollectionSize, value); }
        public string InsertedProductName { get => this._insertedProductName; set => this.SetProperty(ref _insertedProductName, value); }
        public string InsertedProductPrice { get => this._insertedProductPrice; set => this.SetProperty(ref _insertedProductPrice, value); }
        public string InsertedProductQuantity { get => this._insertedProductQuantity; set => this.SetProperty(ref _insertedProductQuantity, value); }
        public List<int> CollectionSizes { get; set; }
        public List<string> ProductsList { get; set; }
        public int SelectedCollectionSizeToUpdate { get => this._selectedCollectionSizeToUpdate; set => this.SetProperty(ref _selectedCollectionSizeToUpdate, value); }
        public string SelectedProduct { get => this._selectedProduct; set => this.SetProperty(ref _selectedProduct, value); }
        public int UpdateQuantity { get => this._updateQuantity; set => this.SetProperty(ref _updateQuantity, value); }
        public decimal UpdateUnitPrice { get => this._updateUnitPrice; set => this.SetProperty(ref _updateUnitPrice, value); }

        public Prism.Commands.DelegateCommand GetProductsCommand { get; set; }
        public Prism.Commands.DelegateCommand ClearValuesCommand { get; set; }
        public Prism.Commands.DelegateCommand InsertDataCommand { get; set; }

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
            productsRepository = new Models.ProductsRepository();
            CollectionSizes = productsRepository.GetCollections();

            GetProductsCommand = new Prism.Commands.DelegateCommand(SetDataGrid, () => !Executing);
            ClearValuesCommand = new Prism.Commands.DelegateCommand(ClearValues, () => !Executing);
            InsertDataCommand = new Prism.Commands.DelegateCommand(ExecuteUpdateProduct, () => !Executing);
        }

        public async void SetDataGrid()
        {
            Executing = true;

            Products product = new Products();
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
                System.Windows.MessageBox.Show(ex.ToString(), "Error", System.Windows.MessageBoxButton.OK);
            }

            Executing = false;
        }

        public void ExecuteUpdateProduct()
        {
            bool commandExecutedSuccessful = false;
            Products product;
            Validations.Parser parser = new Validations.Parser();
            product = parser.ParseInput(this.InsertedProductName, this.InsertedProductQuantity, this.InsertedProductPrice, this.SelectedCollectionSize);
            if (product == null)
                System.Windows.MessageBox.Show("Invalid input!", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            else
            {
                productsRepository = new Models.ProductsRepository();
                commandExecutedSuccessful = productsRepository.InsertProducts(product);
                if(commandExecutedSuccessful)
                    System.Windows.MessageBox.Show("Product has been added to database.", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                else
                    System.Windows.MessageBox.Show("Error during establishing connection to database!", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            ClearValues();
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
            this.InsertedProductName = null;
            this.InsertedProductPrice = null;
            this.InsertedProductQuantity = null;

            Executing = false;
        }
    }
}
