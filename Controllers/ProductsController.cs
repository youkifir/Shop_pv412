using Microsoft.AspNetCore.Mvc;

namespace Shop_pv412.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ReadProducts()
        {
            //Get products from database
            return View(/*products*/);
        }
        [HttpGet]
        public IActionResult CreateProduct() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([Bind(
            "Name,Price,Description")]Product product)
        {
            if (ModelState.IsValid)
            {
                //Add product to database
            }
            else
            {
                //Return error
            }
            return RedirectToAction("ReadProducts", "Products");
        }
        [HttpGet]
        public IActionResult UpdateProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct([Bind(
            "Id,Name,Price,Description")]Product product)
        {
            if (ModelState.IsValid)
            {
                //Update Product
            }
            else
            {
                //Return Error
            }

            return RedirectToAction("ReadProducts", "Products");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return RedirectToAction("ReadProducts", "Products");
        }
    }
}
