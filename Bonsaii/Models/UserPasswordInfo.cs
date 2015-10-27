namespace Bonsaii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("UserPasswordInfo")]
    public partial class UserPasswordInfo
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(256)]
        [Display(Name="�û���")]
        [RegularExpression("^1[2,3,4,5,6,7,8][0-9]{9}$", ErrorMessage = "������Ϸ���{0}")]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name="��ҵ���")]
   
        public string CompanyId { get; set; }
        
        [Column(TypeName = "image")]
        [Display(Name="Ӫҵִ��")]
        public byte[] BusinessLicense { get; set; }

  
        [HiddenInput(DisplayValue = false)]
        [StringLength(50)]
        public string BusinessLicenseType { get; set; }

        [Required]
        [StringLength(12)]
        [Display(Name="��ϵ�绰")]
        public string TelNumber { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime SubmitTime { get; set; }
    }
}
