using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bonsaii.Models
{
    public class DepartmentViewModel
    {        
        [Display(Name = "部门ID")]
        public string Number { get; set; }
        
        [Display(Name = "部门名称")]
        public string Name { get; set; }

        [Display(Name = "上级部门")]
        public string ParentDepartmentName { get; set; }
        [Display(Name = "编制人数")]
        public int StaffSize { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

        [StringLength(50)]
        [Display(Name = "字段名")]
        public string RecordName { get; set; }

        [StringLength(50)]
        [Display(Name = "描述")]
        public string Description { get; set; }
    }
}