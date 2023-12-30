using BusinessLayer.Utilities.Results;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProductService
    {
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);
        IDataResult<Product> GetById(int id);
        IDataResult<List<ProductDto>> GetAll();
        IDataResult<List<ProductDto>> CountOfProducts();
        IDataResult<List<ProductDto>> CountOfProductsInBrands();
        IDataResult<List<ProductDto>> GetAllProductByCategory(int id);
        IDataResult<List<ProductDto>> GetAllProductByBrand(int id);
        IDataResult<List<ProductDto>> GetAllProductByPrice(int price);
        IDataResult<List<ProductDto>> GetAllProductBySearch(string key);
    }
}
