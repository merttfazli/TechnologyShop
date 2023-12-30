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
    public class FeatureRelationManager : IFeatureRelationService
    {
        private readonly IFeatureRelationDal _featureRelationDal;

        public FeatureRelationManager(IFeatureRelationDal featureRelationDal)
        {
            _featureRelationDal = featureRelationDal;
        }

        public IResult Add(FeatureRelation featureRelation)
        {
            var features = _featureRelationDal.GetAll(x=>x.ProductId==featureRelation.ProductId);
            
                var result =_featureRelationDal.Inserter(featureRelation);
                return new SuccessResult(result);
        }

        public IResult Delete(FeatureRelation featureRelation)
        {
            _featureRelationDal.Delete(featureRelation);
            return new SuccessResult(Messages.FeatureRelationDeleted);
        }

        public IDataResult<List<FeatureRelationDto>> GetAll()
        {
            var featureRelations = _featureRelationDal.GetAllDtos();
            if (featureRelations.Any())
                return new SuccessDataResult<List<FeatureRelationDto>>(featureRelations);
            else
                return new ErrorDataResult<List<FeatureRelationDto>>(Messages.FeatureRelationNotFound);

        }

        public IDataResult<FeatureRelation> GetById(int id)
        {
            var featureRelation = _featureRelationDal.Get(x => x.FeatureId == id);
            if (featureRelation != null)
                return new SuccessDataResult<FeatureRelation>(featureRelation);
            else
                return new ErrorDataResult<FeatureRelation>(Messages.FeatureRelationNotFound);
        }

        public IResult Update(FeatureRelation featureRelation)
        {
            _featureRelationDal.Update(featureRelation);
            return new SuccessResult(Messages.FeatureRelationUpdated);
        }
        //private bool checkEquality(FeatureRelation featureRelation)
        //{
        //    var featureRelations = _featureRelationDal.GetAllDtos();
        //    foreach (var getfeatureRelations in featureRelations)
        //    {
        //        if (getfeatureRelations.Feature.Name.Equals(featureRelation.))
        //            return true;
        //        else
        //            return false;
        //    }
        //    return true;
        //}
    }
}
