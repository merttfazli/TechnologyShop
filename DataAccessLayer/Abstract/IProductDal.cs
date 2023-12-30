using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        public List<ProductDto> GetAllDto();
        public List<ProductDto> GetAllProductByCategory(int id);
        public List<ProductDto> GetAllProductByBrand(int id);
        public List<ProductDto> GetAllProductByPrice(int price);
        public List<ProductDto> GetAllProductBySearch(string key);
        public List<ProductDto> AmountOfProducts();
        public List<ProductDto> AmountOfProductInBrands();
    }
}
