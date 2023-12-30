using BusinessLayer.Abstract;
using EntityLayer.Concrete.Dtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using UserUI.Models;

namespace UserUI.Controllers
{
    public class PriceController : Controller
    {
        IProductService _productService;
        IFeatureRelationService _featureRelationService;

        public PriceController(IProductService productService, IFeatureRelationService featureRelationService)
        {
            _productService = productService;
            _featureRelationService = featureRelationService;
        }
        [Route("price")]
        public IActionResult Index(int price)
        {
            var productByPrice = _productService.GetAllProductByPrice(price);
            var featureRelations = _featureRelationService.GetAll();

            return View(new PriceModel
            {
                ProductDtos = productByPrice.Success ? productByPrice.Data : new List<ProductDto>(),
                FeatureRelationDtos = featureRelations.Success ? featureRelations.Data : new List<FeatureRelationDto>(),
            });
        }
    }
}
