using AutoMapper;
using Ilk_MVC_Proyekt.Context;
using Ilk_MVC_Proyekt.Entites;
using Ilk_MVC_Proyekt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ilk_MVC_Proyekt.Controllers
{
	public class ProductController : Controller
	{
		private readonly ProductContext _context;
		private readonly IMapper _mapper;
		public ProductController(ProductContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public IActionResult Index()
		{
			ViewBag.Products = _context.Products.ToList();
			ViewBag.Categories = _context.Categories.ToList();

			return View();
		}
		public IActionResult AddProduct(ProductAddDTO productModel)
		{
			var product = _mapper.Map<Product>(productModel);
			_context.Products.Add(product);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
        public IActionResult Update(int id)
        {
            var Product = _context.Products.FirstOrDefault(cat => cat.ProductId == id);
            ViewBag.Product = Product;

            return View();
        }
        public IActionResult DeleteProduct(int id)
		{
			var product = _context.Products.FirstOrDefault(cat => cat.ProductId == id);
			_context.Products.Remove(product);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult UpdateProduct(int id, string name, int stock, int price)
		{
			var product = new Product()
			{
				ProductId = id,
				ProductName = name,
				ProductStock = stock,
				ProductPrice = price

			};

			_context.Products.Update(product);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
