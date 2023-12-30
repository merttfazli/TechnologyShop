using BusinessLayer.Abstract;
using EntityLayer.Concrete.Dtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using UserUI.Models;

namespace UserUI.ViewComponents
{
    public class Filter : ViewComponent
    {
        IProductService _productService;

        public Filter(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke()
        {
            var products = _productService.GetAll();
            var amountOfProducts = _productService.CountOfProducts();
            var amountOfProductsInBrands = _productService.CountOfProductsInBrands();
            return View(new HomeModel
            {
                ProductDtos = products.Success ? products.Data : new List<ProductDto>(),
                AmaountOfProduct = amountOfProducts.Success ? amountOfProducts.Data : new List<ProductDto>(),
                AmountOfProductInBrand = amountOfProductsInBrands.Success ? amountOfProductsInBrands.Data : new List<ProductDto>(),
            });
        }
    }
}
