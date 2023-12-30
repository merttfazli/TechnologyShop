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
    public interface IFeatureRelationService
    {
        IResult Add(FeatureRelation featureRelation);
        IResult Delete(FeatureRelation featureRelation);
        IResult Update(FeatureRelation featureRelation);
        IDataResult<FeatureRelation> GetById(int id);
        IDataResult<List<FeatureRelationDto>> GetAll();
    }
}
