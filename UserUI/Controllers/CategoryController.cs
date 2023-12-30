using BusinessLayer.Abstract;
using EntityLayer.Concrete.Dtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using UserUI.Models;

namespace UserUI.Controllers
{
    public class CategoryController : Controller
    {
        IProductService _productService;
        IFeatureRelationService _featureRelationService;

        public CategoryController(IProductService productService, IFeatureRelationService featureRelationService)
        {
            _productService = productService;
            _featureRelationService = featureRelationService;
        }

        [Route("kategoriler/{id}")]
        public IActionResult Index(int id)
        {
            var productByCategory = _productService.GetAllProductByCategory(id);
            var featureRelations = _featureRelationService.GetAll();

            return View(new CategoryModel
            {
                ProductDtos = productByCategory.Success ? productByCategory.Data : new List<ProductDto>(),
                FeatureRelationDtos = featureRelations.Success ? featureRelations.Data : new List<FeatureRelationDto>(),
            });
        }
    }
}
