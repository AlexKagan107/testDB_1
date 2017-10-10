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
    public class specialsController : Controller
    {
        private dbEntities2 db = new dbEntities2();

        // GET: specials
        public async Task<ActionResult> Index()
        {
            return View(await db.special.ToListAsync());
        }

        // GET: specials/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            special special = await db.special.FindAsync(id);
            if (special == null)
            {
                return HttpNotFound();
            }
            return View(special);
        }

        // GET: specials/Create
        public ActionResult Create()
        {
            return View();
        }

        //test

        // POST: specials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "special1")] special special)
        {
            if (ModelState.IsValid)
            {
                db.special.Add(special);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(special);
        }

        // GET: specials/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            special special = await db.special.FindAsync(id);
            if (special == null)
            {
                return HttpNotFound();
            }
            return View(special);
        }

        // POST: specials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "special1")] special special)
        {
            if (ModelState.IsValid)
            {
                db.Entry(special).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(special);
        }

        // GET: specials/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            special special = await db.special.FindAsync(id);
            if (special == null)
            {
                return HttpNotFound();
            }
            return View(special);
        }

        // POST: specials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            special special = await db.special.FindAsync(id);
            db.special.Remove(special);
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
