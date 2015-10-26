using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bonsaii.Models;

namespace Bonsaii.Controllers
{
    public class DepartmentController : BaseController
    {
        // GET: Department
        public ActionResult Index(string sortOrder)
        {
                    var q = from d in db.Departments
                    join x in db.Departments on d.ParentDepartmentId equals x.Number
                    select new DepartmentViewModel { Number = d.Number, Name = d.Name, Remark = d.Remark, ParentDepartmentName = x.Name, StaffSize = d.StaffSize }                    
                    ;

                    ViewBag.NumberSort = String.IsNullOrEmpty(sortOrder) ? "Number desc" : "";
                    ViewBag.NameSort = String.IsNullOrEmpty(sortOrder);
                    return View(q);
        }

        // GET: Department/Details/5
        public ActionResult Details(string id)
        {
            
          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            var q = from d in db.Departments
                      join x in db.Departments on d.ParentDepartmentId equals x.Number where d.Number==id
                    select new DepartmentViewModel { Number = d.Number, Name = d.Name,Remark = d.Remark,ParentDepartmentName = x.Name,StaffSize=x.StaffSize }                    
                    ;

            DepartmentViewModel department = q.FirstOrDefault();

            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            //实现下拉列表
            List<SelectListItem> item = db.Departments.ToList().Select(c => new SelectListItem
            {
                Value = c.Number,//保存的值
                Text = c.Name//显示的值
            }).ToList();

            //增加一个“无”选项
            //SelectListItem i = new SelectListItem();
            //i.Value = "1111";
            //i.Text = "无";
            //item.Add(i);

            //传值到页面
            ViewBag.List = item;
         
            return View();
        }

        // POST: Department/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            //实现下拉列表
            List<SelectListItem> item = db.Departments.ToList().Select(c => new SelectListItem
            {
                Value = c.Number,//保存的值
                Text = c.Name//显示的值
            }).ToList();

            //增加一个“无”选项
            SelectListItem i = new SelectListItem();
            i.Value = "";
            i.Text = " ";
            item.Add(i);

            //传值到页面
            ViewBag.List = item;

            //模型状态错误（为空）
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                department.Number = (new Random().Next(1111, 9999)).ToString();
                
                var itemId = (from p in db.Departments where p.Number  == department.ParentDepartmentId select p.Name).FirstOrDefault();

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(string id)
        {
           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department.ParentDepartmentId == null) department.ParentDepartmentId = "1111";

            //实现下拉列表
            var item = db.Departments.ToList().Select(c => new SelectListItem
            {
                Value = c.Number,//保存的值
                Text = c.Name//显示的值
            }).ToList();

            //SelectListItem i = new SelectListItem();
            //i.Value = "";
            //i.Text = " ";
            //item.Add(i);

            //传值到页面
            ViewBag.List = item;


            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        // POST: Department/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {

            //如果公司的上级部门编号ParentDepartmentId为空，将它置为null
            if (department.ParentDepartmentId == "") department.ParentDepartmentId = null;

            //实现下拉列表
            List<SelectListItem> item = db.Departments.ToList().Select(c => new SelectListItem
            {
                Value = c.Number,//保存的值
                Text = c.Name//显示的值
            }).ToList();

            SelectListItem i = new SelectListItem();
            i.Value = "";
            i.Text = " ";
            item.Add(i);

           ViewBag.List = item;//传值到页面

            //模型状态错误（为空）
            if (ModelState.IsValid)
            {
                Department d = db.Departments.Find(department.Number);
                if (d != null)
                {
                    d.Name = department.Name;
                    d.ParentDepartmentId = department.ParentDepartmentId;
                    d.Remark = department.Remark;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
                
            }
            else
            {
                //自带的ValidationSummary提示
                ModelState.AddModelError("", "修改失败");
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(string id)
        {
           // db = new BonsaiiDbContext(base.ConnectionString);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
           // db = new BonsaiiDbContext(base.ConnectionString);
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
