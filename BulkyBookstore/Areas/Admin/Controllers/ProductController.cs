using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Bulky.Models.ViewModels.ProductVM;


namespace BulkyBookstore.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class ProductController : Controller
{

    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IProductRepository db, ICategoryRepository categoryRepo, IWebHostEnvironment webHostEnvironment)
    {
        _productRepo = db;
        _categoryRepo = categoryRepo;
        _webHostEnvironment = webHostEnvironment;
    }
    // GET
    public IActionResult Index()
    {
        List<Product> objProductList = _productRepo.GetAll(includeProperties:"Category").ToList();
        return View(objProductList);
    }

    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            CategoryList = _categoryRepo.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            Product = new Product()
        };
        if (id == null)
        {
            return View(productVM);
        }
        else
        {
            productVM.Product = _productRepo.Get(u=>u.Id == id);
            return View(productVM);
        }
    }

    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");
                string fullPath = Path.Combine(productPath, fileName);

                if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    // delete the old image
                    string oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                    
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                
                productVM.Product.ImageUrl = @"\images\product\" + fileName;
            }

            if (productVM.Product.Id == 0)
            {
                _productRepo.Add(productVM.Product);
            }
            else
            {
                _productRepo.Update(productVM.Product);
            }
            _productRepo.Save();
            TempData["success"] = "Product updated Successfully";
            return RedirectToAction("Index", "Product");
        }
        else
        {
            productVM.CategoryList = _categoryRepo.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(productVM);
        }
    }

    [HttpPost]
    public IActionResult Edit(Product obj, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");
                string fullPath = Path.Combine(productPath, fileName);  
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            _productRepo.Update(obj);
            _productRepo.Save();
            TempData["success"] = "Product updated Successfully";
            return RedirectToAction("Index", "Product");
        }
        return View();
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Product? obj = _productRepo.Get(c=>c.Id == id);

        if (obj == null)
        {
            return NotFound();
        }

        _productRepo.Remove(obj);
        _productRepo.Save();
        TempData["success"] = "Product deleted Successfully";
        return RedirectToAction("Index");
    }
    
    #region API Calls
    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = _productRepo.GetAll(includeProperties:"Category");
        return Json(new {data = productList});
    }
    
            [HttpDelete]
            public IActionResult Delete(int? id)
            {
                var productToBeDeleted = _productRepo.Get(u => u.Id == id);
                if (productToBeDeleted == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }
    
                string productPath = @"images\products\product-" + id;
                string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);
    
                if (Directory.Exists(finalPath)) {
                    string[] filePaths = Directory.GetFiles(finalPath);
                    foreach (string filePath in filePaths) {
                        System.IO.File.Delete(filePath);
                    }
    
                    Directory.Delete(finalPath);
                }
    
    
                _productRepo.Remove(productToBeDeleted);
                _productRepo.Save();
    
                return Json(new { success = true, message = "Delete Successful" });
            }
    #endregion
}
