using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public IResult Add(Product product)
        {
            var getProduct = _productDal.Get(x => x.Name == product.Name);
            if (getProduct != null)
                return new ErrorResult(Messages.ProductAlreadyExists);
            else
            {
                if (checkEquality(product))
                    return new ErrorResult(Messages.ProductAlreadyExists);
                else
                {
                    _productDal.Add(product);
                    return new SuccessResult(Messages.ProductAdded);
                }
            }
        }

        public IDataResult<List<ProductDto>> CountOfProducts()
        {
            var countOfProduct = _productDal.AmountOfProducts();
            return new SuccessDataResult<List<ProductDto>>(countOfProduct);
        }

        public IDataResult<List<ProductDto>> CountOfProductsInBrands()
        {
            var countOfProductInBrand = _productDal.AmountOfProductInBrands();
            return new SuccessDataResult<List<ProductDto>>(countOfProductInBrand);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }
        public IDataResult<List<ProductDto>> GetAll()
        {
            var products = _productDal.GetAllDto();
            if (products.Any())
                return new SuccessDataResult<List<ProductDto>>(products);
            else
                return new ErrorDataResult<List<ProductDto>>(Messages.ProductNotFound);
        }

        public IDataResult<List<ProductDto>> GetAllProductByBrand(int id)
        {
            var productsByBrand = _productDal.GetAllProductByBrand(id);
            return new SuccessDataResult<List<ProductDto>>(productsByBrand);
        }

        public IDataResult<List<ProductDto>> GetAllProductByCategory(int id)
        {
            var productsByCategory = _productDal.GetAllProductByCategory(id);
            return new SuccessDataResult<List<ProductDto>>(productsByCategory);

        }

        public IDataResult<List<ProductDto>> GetAllProductByPrice(int price)
        {
            var productsByPrice = _productDal.GetAllProductByPrice(price);
            return new SuccessDataResult<List<ProductDto>>(productsByPrice);
        }

        public IDataResult<List<ProductDto>> GetAllProductBySearch(string key)
        {
            var productsBySearch = _productDal.GetAllProductBySearch(key);
            return new SuccessDataResult<List<ProductDto>>(productsBySearch);
        }

        public IDataResult<Product> GetById(int id)
        {
            var product = _productDal.Get(x => x.Id == id);
            if (product != null)
                return new SuccessDataResult<Product>(product);
            else
                return new ErrorDataResult<Product>(Messages.ProductNotFound);
        }
        public IResult Update(Product product)
        {
            var getProduct = _productDal.Get(x => x.Name == product.Name && x.Id != product.Id);
            if (getProduct != null)
                return new ErrorResult(Messages.ProductAlreadyExists);
            else
            {
                var _product = _productDal.Get(x => x.Id == product.Id);
                product.Image = _product.Image;
                _productDal.Update(product);
                return new SuccessResult(Messages.ProductUpdated);
            }
        }
        private bool checkEquality(Product product)
        {
            var products = _productDal.GetAll();
            foreach (var getProduct in products)
            {
                if (getProduct.Name.Equals(product.Name))
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
}
