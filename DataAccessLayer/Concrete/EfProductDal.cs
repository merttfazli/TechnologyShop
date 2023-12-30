using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class EfProductDal : EfEntityRepositoryBase<Product, Context>, IProductDal
    {

        public List<ProductDto> AmountOfProductInBrands()
        {
            using Context context = new Context();
            var products = GetAllDto();
            var _products = products.GroupBy(x => x.BrandId).Select(x => new ProductDto
            {
                BrandCount = x.Count(),
                BrandName = context.Brands.FirstOrDefault(y => y.Id == x.Key).Name,
                BrandId = x.Key
            });
            return _products.ToList();
        }

        public List<ProductDto> AmountOfProducts()
        {
            using Context context = new Context();
            var products = GetAllDto();
            var _products = products.GroupBy(x => x.CategoryId).Select(x => new ProductDto
            {
                Count = x.Count(),
                CategoryName = context.Categories.FirstOrDefault(y => y.Id == x.Key).Name,
                CategoryId = x.Key
            });
            return _products.ToList();
        }

        public List<ProductDto> GetAllDto()
        {
            using (Context context = new Context())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.Id
                             join b in context.Brands
                             on p.BrandId equals b.Id
                             join cl in context.Colors
                             on p.ColorId equals cl.Id
                             select new ProductDto
                             {
                                 Product = p,
                                 BrandName = b.Name,
                                 CategoryName = c.Name,
                                 ColorName = cl.Name,
                                 CategoryId = c.Id,
                                 BrandId = b.Id,
                                 FeatureRelationDtos = context.FeatureRelations.Where(x => x.ProductId == p.Id).Select(x =>
                                 new FeatureRelationDto
                                 {
                                      Feature = context.Features.FirstOrDefault(y=>y.Id == x.FeatureId)

                                 }).ToList()
                             };
                return result.ToList();
            }
        }

        public List<ProductDto> GetAllProductByCategory(int id)
        {
            using (Context context = new Context())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             join cl in context.Colors on p.ColorId equals cl.Id
                             where c.Id == id  
                             select new ProductDto
                             {
                                 Product = p,
                                 BrandName = b.Name,
                                 CategoryName = c.Name,
                                 ColorName = cl.Name,
                                 CategoryId = c.Id,
                                 BrandId = b.Id,
                                 FeatureRelationDtos = context.FeatureRelations
                                     .Where(x => x.ProductId == p.Id)
                                     .Select(x => new FeatureRelationDto
                                     {
                                         Feature = context.Features.FirstOrDefault(y => y.Id == x.FeatureId)
                                     })
                                     .ToList()
                             };
                return result.ToList();
            }
        }
        public List<ProductDto> GetAllProductByBrand(int id)
        {
            using (Context context = new Context())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             join cl in context.Colors on p.ColorId equals cl.Id
                             where b.Id == id
                             select new ProductDto
                             {
                                 Product = p,
                                 BrandName = b.Name,
                                 CategoryName = c.Name,
                                 ColorName = cl.Name,
                                 CategoryId = c.Id,
                                 BrandId = b.Id,
                                 FeatureRelationDtos = context.FeatureRelations
                                     .Where(x => x.ProductId == p.Id)
                                     .Select(x => new FeatureRelationDto
                                     {
                                         Feature = context.Features.FirstOrDefault(y => y.Id == x.FeatureId)
                                     })
                                     .ToList()
                             };
                return result.ToList();
            }
        }

        public List<ProductDto> GetAllProductByPrice(int price)
        {
            using (Context context = new Context())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             join cl in context.Colors on p.ColorId equals cl.Id
                             where p.Price <= price
                             select new ProductDto
                             {
                                 Product = p,
                                 BrandName = b.Name,
                                 CategoryName = c.Name,
                                 ColorName = cl.Name,
                                 CategoryId = c.Id,
                                 BrandId = b.Id,
                                 FeatureRelationDtos = context.FeatureRelations
                                     .Where(x => x.ProductId == p.Id)
                                     .Select(x => new FeatureRelationDto
                                     {
                                         Feature = context.Features.FirstOrDefault(y => y.Id == x.FeatureId)
                                     })
                                     .ToList()
                             };
                return result.ToList();
            }
        }

        public List<ProductDto> GetAllProductBySearch(string key)
        {
            using (Context context = new Context())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             join cl in context.Colors on p.ColorId equals cl.Id
                             where p.Name.Contains(key)
                             select new ProductDto
                             {
                                 Product = p,
                                 BrandName = b.Name,
                                 CategoryName = c.Name,
                                 ColorName = cl.Name,
                                 CategoryId = c.Id,
                                 BrandId = b.Id,
                                 FeatureRelationDtos = context.FeatureRelations
                                     .Where(x => x.ProductId == p.Id)
                                     .Select(x => new FeatureRelationDto
                                     {
                                         Feature = context.Features.FirstOrDefault(y => y.Id == x.FeatureId)
                                     })
                                     .ToList()
                             };
                return result.ToList();
            }
        }
    }
}
