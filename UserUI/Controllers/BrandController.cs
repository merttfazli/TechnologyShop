using BusinessLayer.Abstract;
using EntityLayer.Concrete.Dtos;
using Microsoft.AspNetCore.Mvc;
using UserUI.Models;

namespace UserUI.Controllers
{
    public class BrandController : Controller
    {
        IProductService _productService;
        IFeatureRelationService _featureRelationService;

        public BrandController(IProductService productService, IFeatureRelationService featureRelationService)
        {
            _productService = productService;
            _featureRelationService = featureRelationService;
        }

        [Route("marka")]
        public IActionResult Index(int brandId)
        {
            var productByBrand = _productService.GetAllProductByBrand(brandId);
            var featureRelations = _featureRelationService.GetAll();

            return View(new BrandModel
            {
                ProductDtos = productByBrand.Success ? productByBrand.Data : new List<ProductDto>(),
                FeatureRelationDtos = featureRelations.Success ? featureRelations.Data : new List<FeatureRelationDto>(),
            });
        }
    }
}
