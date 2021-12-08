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



        // GET: Client
        public ActionResult Index()
        {

            return View();
        }

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
            switch (sortOrder)
            {
                case 1:
                    products = products.OrderBy(s => s.nameProduct);
                    break;
                case 2:
                    products = products.OrderBy(s => s.idProduct);
                    break;
                case 3:
                    products = products.OrderBy(s => s.rateProduct);
                    break;
                case 4:
                    products = products.OrderBy(s => s.idProduct);
                    break;
                case 5:
                    products = products.OrderBy(s => s.priceProduct);
                    break;
                case 6:
                    products = products.OrderByDescending(s => s.priceProduct);
                    break;
            }



            return View(products.ToList());
        }

    
        public ActionResult DetailProduct(string searchCate, string id)
        {
            
            Product product = db.Products.Find(id);
            List<Category> listCategory = db.Categories.ToList();
            List<Product> listProduct = db.Products.ToList();
            List<UserProduct> listUserProduct = db.UserProducts.ToList();

            //List<Product> listSearchByID = new List<Product>();
            //foreach (var item in listProduct)
            //{
            //    if(item.idCategory.Equals(searchCate))
            //    {
            //        listSearchByID.Add(item);
            //    }    
                    
            //}    

            ViewBag.listCate = listCategory;
            //ViewBag.listPro = listProduct.ToList();
            ViewBag.listUser = listUserProduct;

            if (product == null)
            {
                return HttpNotFound();
            }


            var products = from m in db.Products
                           select m;

            if (!String.IsNullOrEmpty(searchCate))
            {
                products = from m in db.Products
                           join n in db.Categories on m.idCategory equals n.idCategory
                           where n.nameCategory.Equals(searchCate)
                           select m;
            }
            ViewBag.listPro = products.ToList();
            return View(product);
        }
    }
}