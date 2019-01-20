using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerApp.ViewModels
{
    public class CartSummaryViewModel
    {
        private readonly IEnumerable<Deliveries> _deliveries;
        private readonly IEnumerable<Regions> _regions;

        public CartSummaryViewModel()
        {
            ShippingDetails shippingDetails = new ShippingDetails();
            _deliveries = shippingDetails.GetDeliveries();
            _regions = shippingDetails.GetCustomerData(SessionProcess.SessionIdentifier);
        }

        public int RegionId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Delivery Type")]
        public int SelectedDeliveryId { get; set; }

        public IEnumerable<SelectListItem> RegionItems => new SelectList(_regions, "RegionId", Enumerable.Range(1, _regions.Count()));

        public IEnumerable<SelectListItem> DeliveryItems => new SelectList(_deliveries, "DeliveryId", "DeliveryType");
    }
}