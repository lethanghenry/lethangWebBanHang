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
    public class UserProductsController : Controller
    {
        /// <summary>
        /// Create a database QuanLy db
        /// </summary>
        private QuanLy db = new QuanLy();

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <returns></returns>
        // GET: UserProducts
        public ActionResult Index()
        {

            return View(db.UserProducts.ToList());
        }

        /// <summary>
        /// Search a User by Name User
        /// </summary>
        /// <param name="searchNameUser"></param>
        /// <returns></returns>
        public ActionResult search(string searchNameUser)
        {
            var userProducts = from m in db.UserProducts select m;
            if (!String.IsNullOrEmpty(searchNameUser))
            {
                userProducts = userProducts.Where(s => s.nameUser.Contains(searchNameUser));
            }
            return View(userProducts);
        }

        /// <summary>
        /// Create a view
        /// </summary>
        /// <returns></returns>
        // GET: UserProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create a User
        /// </summary>
        /// <param name="userProduct"></param>
        /// <returns></returns>
        // POST: UserProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUser,reviewUser,nameUser,emailUser")] UserProduct userProduct)
        {
            if (ModelState.IsValid)
            {
                db.UserProducts.Add(userProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userProduct);
        }

        /// <summary>
        /// Create a Edit view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserProducts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProduct userProduct = db.UserProducts.Find(id);
            if (userProduct == null)
            {
                return HttpNotFound();
            }
            return View(userProduct);
        }

        /// <summary>
        /// Edit a User
        /// </summary>
        /// <param name="userProduct"></param>
        /// <returns></returns>
        // POST: UserProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUser,reviewUser,nameUser,emailUser")] UserProduct userProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userProduct);
        }

        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete2(string id)
        {
            UserProduct userProduct = db.UserProducts.Find(id);
            List<Product> products = db.Products.Where(x => x.idUser == userProduct.idUser).ToList();
            foreach (var item in products)
            {
                db.Products.Remove(item);
            }
            db.UserProducts.Remove(userProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
