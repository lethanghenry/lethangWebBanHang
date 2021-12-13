using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class ClientController : Controller
    {
        private QuanLy db = new QuanLy();

        /// <summary>
        /// Show a view Index
        /// </summary>
        /// <returns></returns>
        // GET: Client
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// Function get max from list Product
        /// </summary>
        /// <returns></returns>
        public int showMax()
        {
            int max = 1;
            foreach (Product item in db.Products.ToList())
            {
                if (item.rateProduct > max)
                {
                    max = item.rateProduct.Value;
                }
            }
            return max;
        }

        /// <summary>
        /// function get id category
        /// </summary>
        /// <param name="searchCategory"></param>
        /// <returns></returns>
        public string showCategoryname(string searchCategory)
        {
            string idPro = "";
            foreach (Category item in db.Categories.ToList())
            {
                if (item.nameCategory.Equals(searchCategory))
                {
                    idPro = item.idCategory;
                }
            }
            return idPro;
        }

        /// <summary>
        /// Show List Product and List category
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="searchCate"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public ActionResult ListProduct(string searchString, string searchCate, int? sortOrder)
        {
            List<Category> listCategory = db.Categories.ToList();
            List<Product> listProduct = db.Products.ToList();
            List<UserProduct> listUserProduct = db.UserProducts.ToList();

            ViewBag.listCate = listCategory;
            ViewBag.listPro = listProduct;
            ViewBag.listUser = listUserProduct;

            List<Product> listProductMax = new List<Product>();
            for (int i = 0; i < listProduct.Count; i++)
            {
                if (listProduct[i].rateProduct == showMax())
                {
                    listProductMax.Add(listProduct[i]);
                }
            }
            ViewBag.listMax = listProductMax.ToList();

            var products = from m in db.Products
                           select m;
            if (!string.IsNullOrEmpty(searchString))
            {

                products = products.Where(m => m.nameProduct.Contains(searchString));

            }
            if (!String.IsNullOrEmpty(searchCate))
            {
                products = from m in db.Products
                           join n in db.Categories on m.idCategory equals n.idCategory
                           where n.nameCategory.Equals(searchCate)
                           select m;
            }
            string nameSort = "Default Sort";
            switch (sortOrder)
            {
                case 1:
                    products = products.OrderBy(s => s.nameProduct);
                    nameSort = "Default Sort";
                    break;
                case 2:
                    products = products.OrderBy(s => s.idProduct);
                    nameSort = "Sort by popularity";
                    break;
                case 3:
                    products = products.OrderBy(s => s.rateProduct);
                    nameSort = "Sort by average rating";
                    break;
                case 4:
                    products = products.OrderBy(s => s.idProduct);
                    nameSort = "Sort by newness";
                    break;
                case 5:
                    products = products.OrderBy(s => s.priceProduct);
                    nameSort = "Sort by Price:low to high";
                    break;
                case 6:
                    products = products.OrderByDescending(s => s.priceProduct);
                    nameSort = "Sort by Price:high to low";
                    break;
            }
            ViewBag.nameSortProduct = nameSort;
            return View(products.ToList());
        }

        /// <summary>
        /// Show detail one Product and related Category of Product
        /// </summary>
        /// <param name="searchCate"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DetailProduct(string searchCate, string id)
        {
            Product product = db.Products.Find(id);
            List<Category> listCategory = db.Categories.ToList();
            List<Product> listProduct = db.Products.ToList();
            List<UserProduct> listUserProduct = db.UserProducts.ToList();

            ViewBag.listCate = listCategory;
            ViewBag.listUser = listUserProduct;
            ViewBag.listPro = listProduct;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}