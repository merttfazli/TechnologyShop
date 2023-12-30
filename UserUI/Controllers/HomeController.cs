using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserUI.Models;

namespace UserUI.Controllers
{
    public class HomeController : Controller
    {
        IProductService _productService;
        IFeatureRelationService _featureRelationService;

        public HomeController(IProductService productService, IFeatureRelationService featureRelationService)
        {
            _productService = productService;
            _featureRelationService = featureRelationService;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var products = _productService.GetAll();
            var featureRelations = _featureRelationService.GetAll();
            return View(new HomeModel
            {
                ProductDtos = products.Success ? products.Data : new List<ProductDto>(),
                FeatureRelationDtos = featureRelations.Success ? featureRelations.Data : new List<FeatureRelationDto>(),
            });
        }
    }
}