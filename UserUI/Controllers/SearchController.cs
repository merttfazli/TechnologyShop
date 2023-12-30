using BusinessLayer.Abstract;
using EntityLayer.Concrete.Dtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using UserUI.Models;

namespace UserUI.Controllers
{
    public class SearchController : Controller
    {
        IProductService _productService;
        IFeatureRelationService _featureRelationService;

        public SearchController(IProductService productService, IFeatureRelationService featureRelationService)
        {
            _productService = productService;
            _featureRelationService = featureRelationService;
        }
        [Route("search")]
        public IActionResult Index(string key)
        {
            var productBySearch = _productService.GetAllProductBySearch(key);
            var featureRelations = _featureRelationService.GetAll();

            return View(new SearchModel
            {
                ProductDtos = productBySearch.Success ? productBySearch.Data : new List<ProductDto>(),
                FeatureRelationDtos = featureRelations.Success ? featureRelations.Data : new List<FeatureRelationDto>(),
            });
        }
    }
}
