using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Dtos
{
    public class ProductDto:IDto
    {
        public Product Product { get; set; }
        public List<FeatureRelationDto> FeatureRelationDtos { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public int Count { get; set; }
        public int BrandCount { get; set; }
    }
}
