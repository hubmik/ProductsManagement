using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Server.DTO
{
    [DataContract]
    public class OrderContext
    {
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }
        [DataMember]
        public string DeliveryDate { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string DeliveryType { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}