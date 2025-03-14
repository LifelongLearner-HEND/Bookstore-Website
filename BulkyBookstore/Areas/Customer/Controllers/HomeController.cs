using System.Diagnostics;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BulkyBookstore.Models;
using BulkyWeb.Models;

namespace BulkyBookstore.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductRepository _productRepository;

    public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> productList = _productRepository.GetAll(includeProperties: "Category");
        return View(productList);
    }
    
    public IActionResult Details(int productId)
    {
        var product = _productRepository.Get(u => u.Id == productId, includeProperties: "Category");
        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}