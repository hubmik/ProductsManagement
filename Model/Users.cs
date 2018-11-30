namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50)]
        public string Login { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(50)]
        public string AccessToken { get; set; }

        [InverseProperty(nameof(Model.Customers.User))]
        public virtual ICollection<Customers> Customers { get; set; }

        [InverseProperty(nameof(Model.Employees.User))]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
