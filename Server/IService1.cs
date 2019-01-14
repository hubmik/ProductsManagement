using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        void AddProduct(Products product);

        [OperationContract]
        void DeleteProduct(Products product);

        [OperationContract]
        void UpdateProduct(Products product);

        [OperationContract]
        List<DTO.ProductExtension> ProductExtensions();

        [OperationContract]
        List<Products> GetProducts(Products product, DTO.ProductExtension productExtension);

        [OperationContract]        
        List<Customers> GetCustomerData(int customerId);

        [OperationContract]
        bool IsUserAuthenticated(string accessKey);

        [OperationContract]
        Employees GetUserCredentials(string accessKey);

        [OperationContract]
        List<Orders> TakeOrders(string accessKey);

        [OperationContract]
        List<DTO.OrderContext> EmployeesOrders(string accessKey);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
