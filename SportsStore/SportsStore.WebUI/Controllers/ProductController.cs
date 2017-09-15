using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        public int PageSize = 4;
        public ProductController(IProductsRepository productRespository)
        {
            this.repository = productRespository;
        }
        public ViewResult List(string category,int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel {
                Products = repository.Products.Where(p => p.Category == category || category == null).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(x => x.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int ProductID)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == ProductID);
            if(prod != null)
            {
                return File(prod.ImageData,prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}