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
    public class ProductsController : Controller
    {
        /// <summary>
        /// Create a database QuanLy db
        /// </summary>
        private QuanLy db = new QuanLy();

        /// <summary>
        /// List Product
        /// </summary>
        /// <returns></returns>
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.UserProduct);
            return View(products.ToList());
        }

        /// <summary>
        /// Search Product by Id
        /// </summary>
        /// <param name="searchNameProduct"></param>
        /// <returns></returns>
        public ActionResult search(string searchNameProduct)
        {
            var products = from m in db.Products select m;
            if (!String.IsNullOrEmpty(searchNameProduct))
            {
                products = products.Where(s => s.nameProduct.Contains(searchNameProduct));
            }
            return View(products);
        }

        /// <summary>
        /// Create a view
        /// </summary>
        /// <returns></returns>
        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.idCategory = new SelectList(db.Categories, "idCategory", "nameCategory");
            ViewBag.idUser = new SelectList(db.UserProducts, "idUser", "reviewUser");
            return View();
        }

        /// <summary>
        /// Create a Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product)
        {
            if (ModelState.IsValid)
            {
                product.pictureProduct = "";
                var f = Request.Files["ImageFile"];
                if(f!=null&&f.ContentLength>0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/dataImage/"+FileName);
                    f.SaveAs(UploadPath);
                    product.pictureProduct = FileName;
                    
                }    
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCategory = new SelectList(db.Categories, "idCategory", "nameCategory", product.idCategory);
            ViewBag.idUser = new SelectList(db.UserProducts, "idUser", "reviewUser", product.idUser);
            return View(product);
        }

        /// <summary>
        /// Create a view Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategory = new SelectList(db.Categories, "idCategory", "nameCategory", product.idCategory);
            ViewBag.idUser = new SelectList(db.UserProducts, "idUser", "reviewUser", product.idUser);
            return View(product);
        }

        /// <summary>
        /// Edit a Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProduct,nameProduct,pictureProduct,priceProduct,rateProduct,qualityProduct,descriptionProduct,weightProduct,dismensionProduct,idCategory,idUser")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.pictureProduct = "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/dataImage/" + FileName);
                    f.SaveAs(UploadPath);
                    product.pictureProduct = FileName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCategory = new SelectList(db.Categories, "idCategory", "nameCategory", product.idCategory);
            ViewBag.idUser = new SelectList(db.UserProducts, "idUser", "reviewUser", product.idUser);
            return View(product);
        }

        /// <summary>
        /// Delete by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete2(string id)
        {
            Product product = db.Products.Find(id);           
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
