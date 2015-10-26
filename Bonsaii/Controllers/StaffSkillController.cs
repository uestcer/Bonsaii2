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
    public class StaffSkillController : BaseController
    {
        

        // GET: StaffSkill
        public ActionResult Index()
        {
            var q = from ss in db.StaffSkills
                    join s in db.Staffs on ss.StaffNumber equals s.StaffNumber
                    join sp in db.SkillParameters on ss.SkillNumber equals sp.SkillNumber
                    select new StaffSkillView{ Id=ss.Id,BillType = ss.BillType, BillNumber = ss.BillNumber, StaffNumber = ss.StaffNumber, StaffName=s.Name,SkillNumber=ss.SkillNumber,SkillName=sp.SkillName,SkillGrade=ss.SkillGrade,SkillRemark=ss.SkillRemark }
                     ;
            return View(q);
        }
        public JsonResult Index1()
        {
            List<Object> obj = new List<Object>();
            var Staffs = db.Staffs.ToList();
            foreach (var temp in Staffs)
            {



                obj.Add(new { number = temp.StaffNumber,name=temp.Name });


               // return Json(obj);
            }
            return Json(obj);
        }

        // GET: StaffSkill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffSkill staffSkill = db.StaffSkills.Find(id);
            if (staffSkill == null)
            {
                return HttpNotFound();
            }
            var skillParameters = db.SkillParameters.Where(sp => sp.SkillNumber.Equals(staffSkill.SkillNumber));
            var staffs = db.Staffs.Where(s => s.StaffNumber.Equals(staffSkill.StaffNumber));
            foreach (var temp in skillParameters)
            {
                staffSkill.SkillName = temp.SkillName;
            }
            foreach (var temp in staffs)
            {
                staffSkill.StaffName = temp.Name;
            }
            return View(staffSkill);
        }

        // GET: StaffSkill/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffSkill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BillType,BillNumber,StaffNumber,StaffName,SkillNumber,SkillName,SkillGrade,SkillRemark")] StaffSkill staffSkill)
        {
            if (ModelState.IsValid)
            {
                db.StaffSkills.Add(staffSkill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffSkill);
        }

        // GET: StaffSkill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffSkill staffSkill = db.StaffSkills.Find(id);
           
            if (staffSkill == null)
            {
                return HttpNotFound();
            } 
          
             var skillParameters = db.SkillParameters.Where(sp => sp.SkillNumber.Equals(staffSkill.SkillNumber));
             var staffs = db.Staffs.Where(s => s.StaffNumber.Equals(staffSkill.StaffNumber));
             foreach(var temp in skillParameters)
            {
                staffSkill.SkillName = temp.SkillName;
            }
             foreach (var temp in staffs)
             {
                 staffSkill.StaffName = temp.Name;
             }
            return View(staffSkill);
        }

        // POST: StaffSkill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BillType,BillNumber,StaffNumber,StaffName,SkillNumber,SkillName,SkillGrade,SkillRemark")] StaffSkill staffSkill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffSkill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffSkill);
        }

        // GET: StaffSkill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffSkill staffSkill = db.StaffSkills.Find(id);
            if (staffSkill == null)
            {
                return HttpNotFound();
            }
            var skillParameters = db.SkillParameters.Where(sp => sp.SkillNumber.Equals(staffSkill.SkillNumber));
            var staffs = db.Staffs.Where(s => s.StaffNumber.Equals(staffSkill.StaffNumber));
            foreach (var temp in skillParameters)
            {
                staffSkill.SkillName = temp.SkillName;
            }
            foreach (var temp in staffs)
            {
                staffSkill.StaffName = temp.Name;
            }
            return View(staffSkill);
        }

        // POST: StaffSkill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffSkill staffSkill = db.StaffSkills.Find(id);
            db.StaffSkills.Remove(staffSkill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult SearchStaff(string staffNumber)
        {
            List<Object> obj = new List<Object>();
            var Staffs = from s in db.Staffs select s;
            if (!String.IsNullOrEmpty(staffNumber))
            {

                var tmp = db.Staffs.Where(s => s.StaffNumber.Equals(staffNumber));
                if (tmp.Count() != 0)
                {
                    Staffs = Staffs.Where(s => s.StaffNumber.Equals(staffNumber));
                    foreach (var temp in Staffs)
                    {



                        obj.Add(new { text = temp.Name });


                        return Json(obj);
                    }
                }
                else
                {
                    obj.Add(new { text = "" });
                    return Json(obj);
                }


            }
            return Json(obj);
        }
        public JsonResult SearchSkill(string skillNumber)
        {
            List<Object> obj = new List<Object>();
            var SkillParameters = from s in db.SkillParameters select s;
            if (!String.IsNullOrEmpty(skillNumber))
            {

                var tmp = db.SkillParameters.Where(s => s.SkillNumber.Equals(skillNumber));
                if (tmp.Count() != 0)
                {
                    SkillParameters = SkillParameters.Where(s => s.SkillNumber.Equals(skillNumber));
                    foreach (var temp in SkillParameters)
                    {



                        obj.Add(new { text = temp.SkillName });


                        return Json(obj);
                    }
                }
                else
                {
                    obj.Add(new { text = "" });
                    return Json(obj);
                }


            }
            return Json(obj);
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
