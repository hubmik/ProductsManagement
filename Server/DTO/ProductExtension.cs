using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Server.DTO
{
    [DataContract]
    public class ProductExtension
    {
        [DataMember]
        public int QuantityFrom { get; set; }
        [DataMember]
        public int QuantityTo { get; set; }
        [DataMember]
        public decimal UnitPriceFrom { get; set; }
        [DataMember]
        public decimal UnitPriceTo { get; set; }
    }
}