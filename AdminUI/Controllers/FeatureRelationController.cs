using AdminUI.Models;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AdminUI.Controllers
{
    public class FeatureRelationController : Controller
    {
        private IFeatureRelationService _featureRelationService;
        private IFeatureService _featureService;
        private IProductService _productService;

        public FeatureRelationController(IFeatureRelationService featureRelationService, IFeatureService featureService, IProductService productService)
        {
            _featureRelationService = featureRelationService;
            _featureService = featureService;
            _productService = productService;
        }

        [Route("feature-products/{id}")]
        public IActionResult Index(int id)
        {
            var featureRelations = _featureRelationService.GetAll();
            var features = _featureService.GetAll();
            var product = _productService.GetById(id);
            return View(new FeatureRelationModel
            {
                Features = features.Success ? features.Data : new List<Feature>(),
                FeatureRelationDtos = featureRelations.Success ? featureRelations.Data : new List<FeatureRelationDto>(),
                FeatureRelation = new FeatureRelation() { ProductId=id},
                FeatureRelationDto = new FeatureRelationDto(),
                Product = product.Success ? product.Data : new Product(),
                Feature = new Feature(),
                Result = new ResultModel()
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("feature-products")]
        public IActionResult Save(FeatureRelation featureRelation)
        {
            if (ModelState.IsValid)
            {
                if (featureRelation.Id == 0)
                {
                    var data = _featureRelationService.Add(featureRelation);
                    var featureRelations = _featureRelationService.GetAll();
                    var features = _featureService.GetAll();
                    var product = _productService.GetById(featureRelation.ProductId);
                    ModelState.Clear();
                    return View("Index", new FeatureRelationModel
                    {
                        Features = features.Success ? features.Data : new List<Feature>(),
                        FeatureRelationDtos = featureRelations.Success ? featureRelations.Data : new List<FeatureRelationDto>(),
                        FeatureRelation = new FeatureRelation() {ProductId=featureRelation.ProductId},
                        FeatureRelationDto = new FeatureRelationDto(),
                        Product = product.Success ? product.Data : new Product(),
                        Feature = new Feature(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message },
                    });
                }
                else
                {
                    var data = _featureRelationService.Update(featureRelation);
                    var featureRelations = _featureRelationService.GetAll();
                    var features = _featureService.GetAll();
                    var product = _productService.GetById(featureRelation.ProductId);
                    ModelState.Clear();
                    return View("Index", new FeatureRelationModel
                    {
                        Features = features.Success ? features.Data : new List<Feature>(),
                        FeatureRelationDtos = featureRelations.Success ? featureRelations.Data : new List<FeatureRelationDto>(),
                        FeatureRelation = new FeatureRelation() { ProductId = featureRelation.ProductId },
                        FeatureRelationDto = new FeatureRelationDto(),
                        Product = product.Success ? product.Data : new Product(),
                        Feature = new Feature(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message },
                    });
                }
            }
            else
            {
                var featureRelations = _featureRelationService.GetAll();
                var features = _featureService.GetAll();
                var product = _productService.GetById(featureRelation.ProductId);
                ModelState.Clear();
                return View("Index", new FeatureRelationModel
                {
                    Features = features.Success ? features.Data : new List<Feature>(),
                    FeatureRelationDtos = featureRelations.Success ? featureRelations.Data : new List<FeatureRelationDto>(),
                    FeatureRelation = new FeatureRelation() { ProductId = featureRelation.ProductId },
                    FeatureRelationDto = new FeatureRelationDto(),
                    Product = product.Success ? product.Data : new Product(),
                    Feature = new Feature(),
                    Result = new ResultModel()
                });
            }
        }
        [Route("feature-products/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var featureRelation = _featureRelationService.GetById(id);
            if (featureRelation.Success)
            {
                var data = _featureRelationService.Delete(featureRelation.Data);
            }
            return Redirect($"~/feature-products/{featureRelation.Data.ProductId}");

        }
        [Route("get-features")]
        public IActionResult GetFeatures(int featureId)
        {
            var feature = _featureService.GetById(featureId);
            if (feature.Success)
            {
                var choices = _featureService.GetByName(feature.Data.Name);
                if (choices.Success)
                    return Json(choices.Data);
            }
            return StatusCode(400);
        }
    }
}
