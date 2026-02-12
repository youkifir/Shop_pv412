using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_pv412.Services;

namespace Shop_pv412.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IServiceProduct _serviceProduct;
        public ProductsController(IServiceProduct serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }

        //https://localhost:[port]/Products/ReadProducts
        //HTTP METHOD: GET
        [HttpGet]
        public async Task<IActionResult> ReadProducts()
        {
            var products = await _serviceProduct.GetAllAsync();
            return View(products);
        }

        //GET: /Products/Create
        [HttpGet]
        [Authorize(Roles = "admin,moderator")]
        public IActionResult CreateProduct()
        {
            return View();
        }

        //POST: /Products/Create
        [HttpPost]
        [Authorize(Roles = "admin,moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([Bind(
            "Name,Price,Description")]Product product)
        {
            if (ModelState.IsValid)
            {
                await _serviceProduct.CreateAsync(product);
                return RedirectToAction("ReadProducts", "Products");
            }

            return BadRequest("Invalid product data...");
        }

        //GET: /Products/Update
        [HttpGet]
        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> UpdateProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = await _serviceProduct.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        //POST: /Products/Update
        [HttpPost]
        [Authorize(Roles = "admin,moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(int id, [Bind(
            "Id,Name,Price,Description")]Product product)
        {
            if (ModelState.IsValid)
            {
                await _serviceProduct.UpdateAsync(id, product);
                return RedirectToAction("ReadProducts", "Products");
            }
            return BadRequest("Invalid product data...");
        }

        //GET: /Products/Delete
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,moderator")]
        public IActionResult GetDeleteProduct(int id)
        {
            return View("DeleteProduct", id);
        }

        //POST: /Products/Delete
        [HttpPost]
        [Authorize(Roles ="admin,moderator")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _serviceProduct.DeleteAsync(id);
            return RedirectToAction("ReadProducts", "Products");
        }

        //GET: /Products/Details
        [HttpGet]
        public async Task<IActionResult> DetailsProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = await _serviceProduct.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}