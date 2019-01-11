namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employees
    {
        [Key]
        public int EmployeeId { get; set; }

        public int RegionId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string JobPosition { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime? DismissDate { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        [ForeignKey(nameof(RegionId))]
        public virtual Regions Region { get; set; }
    }
}
