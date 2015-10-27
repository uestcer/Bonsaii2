namespace Bonsaii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DepartmentReserves")]
    public partial class DepartmentReserve
    {
        [Key]
        public int Id { get; set; }

      //  [Required]
        [StringLength(50)]
        [Display(Name = "×Ö¶ÎÃû")]
        public string RecordName { get; set; }

       // [Required]
        [StringLength(50)]
        [Display(Name = "ÃèÊö")]
        public string Description { get; set; }
    }
}
