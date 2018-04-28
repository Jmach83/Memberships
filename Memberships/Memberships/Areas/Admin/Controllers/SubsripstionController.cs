using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Memberships.Entities;
using Memberships.Models;

namespace Memberships.Areas.Admin.Controllers
{
    public class SubsripstionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Subsripstion
        public async Task<ActionResult> Index()
        {
            return View(await db.Subscriptions.ToListAsync());
        }

        // GET: Admin/Subsripstion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subsripstion subsripstion = await db.Subscriptions.FindAsync(id);
            if (subsripstion == null)
            {
                return HttpNotFound();
            }
            return View(subsripstion);
        }

        // GET: Admin/Subsripstion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Subsripstion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,RegistrationCode")] Subsripstion subsripstion)
        {
            if (ModelState.IsValid)
            {
                db.Subscriptions.Add(subsripstion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(subsripstion);
        }

        // GET: Admin/Subsripstion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subsripstion subsripstion = await db.Subscriptions.FindAsync(id);
            if (subsripstion == null)
            {
                return HttpNotFound();
            }
            return View(subsripstion);
        }

        // POST: Admin/Subsripstion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,RegistrationCode")] Subsripstion subsripstion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subsripstion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(subsripstion);
        }

        // GET: Admin/Subsripstion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subsripstion subsripstion = await db.Subscriptions.FindAsync(id);
            if (subsripstion == null)
            {
                return HttpNotFound();
            }
            return View(subsripstion);
        }

        // POST: Admin/Subsripstion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Subsripstion subsripstion = await db.Subscriptions.FindAsync(id);
            db.Subscriptions.Remove(subsripstion);
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
