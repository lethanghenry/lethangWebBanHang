using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class CategoriesController : Controller
    {
        /// <summary>
        /// Create database QuanLy db
        /// </summary>
        private QuanLy db = new QuanLy();

        /// <summary>
        /// List Categories
        /// </summary>
        /// <returns></returns>
        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        /// <summary>
        /// Search Category by Name Category
        /// </summary>
        /// <param name="searchNameCategory"></param>
        /// <returns></returns>
        public ActionResult search(string searchNameCategory)
        {
            var categories = from m in db.Categories select m;
            if (!String.IsNullOrEmpty(searchNameCategory))
            {
                categories = categories.Where(s => s.nameCategory.Contains(searchNameCategory));
            }
            return View(categories);
        }

        /// <summary>
        /// Show category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Categories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        /// <summary>
        /// Create a view
        /// </summary>
        /// <returns></returns>
        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create a view
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCategory,nameCategory")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        /// <summary>
        /// Edit a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        /// <summary>
        /// Edit Category by id
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCategory,nameCategory")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        /// <summary>
        ///  Delete Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete2(string id)
        {
            Category category = db.Categories.Find(id);
            List<Product> products = db.Products.Where(x => x.idCategory == category.idCategory).ToList();
            foreach(var item in products)
            {
                db.Products.Remove(item);
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
