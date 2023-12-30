using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class EfFeatureRelationDal : EfEntityRepositoryBase<FeatureRelation, Context>, IFeatureRelationDal
    {
        public List<FeatureRelationDto> GetAllDtos()
        {
            using (Context context = new Context())
            {
                var result = from fr in context.FeatureRelations
                             join p in context.Products
                             on fr.ProductId equals p.Id
                             join f in context.Features
                             on fr.FeatureId equals f.Id
                             select new FeatureRelationDto
                             {
                                 Feature = f,
                                 ProductName = p.Name,
                             };
                return result.ToList();
            }
        }


        public string Inserter(FeatureRelation featureRelation)
        {
            bool state = false;
            using (Context context = new Context())
            {

                var features = context.FeatureRelations.Where(x => x.ProductId == featureRelation.ProductId).Count();
                var feature = context.Features.FirstOrDefault(x => x.Id == featureRelation.FeatureId);

                if (feature != null)
                {

                    var result = context.FeatureRelations.Where(x => x.ProductId == featureRelation.ProductId).Select(x => x.FeatureId).ToList();

                    var feautres = context.Features.Where(x => x.Id > 0 && result.Contains(x.Id));

                    foreach (var feaut in feautres)
                    {
                        if (feature.Name == feaut.Name)
                        {
                            var _relation = context.FeatureRelations.FirstOrDefault(x => x.ProductId == featureRelation.ProductId && x.FeatureId == feaut.Id);
                            context.FeatureRelations.RemoveRange(_relation);
                            state = true;
                        }
                    }
                }
                if (state || features < 2)
                {
                    context.FeatureRelations.Add(featureRelation);
                    context.SaveChanges();
                    return state ? "Güncellendi" : "Eklendi";
                }
                return "Maksimum 2 özellik eklenebilir.";
            }
        }
    }
}
