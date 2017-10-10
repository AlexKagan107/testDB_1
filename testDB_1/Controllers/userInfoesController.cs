using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testDB_1.Models;

namespace testDB_1.Controllers
{
    public class userInfoesController : Controller
    {
        private dbEntities2 db = new dbEntities2();

        // GET: userInfoes
        public async Task<ActionResult> Index()
        {
            var userInfo = db.userInfo.Include(u => u.Doctor).Include(u => u.User);
            return View(await userInfo.ToListAsync());
        }

        // GET: userInfoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userInfo userInfo = await db.userInfo.FindAsync(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // GET: userInfoes/Create
        public ActionResult Create()
        {
            ViewBag.docId = new SelectList(db.Doctor, "doctorId", "name");
            ViewBag.userId = new SelectList(db.User, "userId", "password");
            return View();
        }

        // POST: userInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,userId,name_,address,telnum,docId")] userInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.userInfo.Add(userInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.docId = new SelectList(db.Doctor, "doctorId", "name", userInfo.docId);
            ViewBag.userId = new SelectList(db.User, "userId", "password", userInfo.userId);
            return View(userInfo);
        }

        // GET: userInfoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userInfo userInfo = await db.userInfo.FindAsync(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.docId = new SelectList(db.Doctor, "doctorId", "name", userInfo.docId);
            ViewBag.userId = new SelectList(db.User, "userId", "password", userInfo.userId);
            return View(userInfo);
        }

        // POST: userInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,userId,name_,address,telnum,docId")] userInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.docId = new SelectList(db.Doctor, "doctorId", "name", userInfo.docId);
            ViewBag.userId = new SelectList(db.User, "userId", "password", userInfo.userId);
            return View(userInfo);
        }

        // GET: userInfoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userInfo userInfo = await db.userInfo.FindAsync(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: userInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            userInfo userInfo = await db.userInfo.FindAsync(id);
            db.userInfo.Remove(userInfo);
            await db.SaveChangesAsync();
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
