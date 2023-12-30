using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class FeatureManager : IFeatureService
    {
        private readonly IFeatureDal _featureDal;

        public FeatureManager(IFeatureDal featureDal)
        {
            _featureDal = featureDal;
        }

        public IResult Add(Feature feature)
        {

            _featureDal.Add(feature);
            return new SuccessResult(Messages.FeatureAdded);

        }


        public IResult Delete(Feature feature)
        {
            _featureDal.Delete(feature);
            return new SuccessResult(Messages.FeatureDeleted);
        }

        public IDataResult<List<Feature>> GetAll()
        {
            var features = _featureDal.GetAll();
            if (features.Any())
                return new SuccessDataResult<List<Feature>>(features);
            else
                return new ErrorDataResult<List<Feature>>(Messages.FeatureNotFound);
        }

        public IDataResult<Feature> GetById(int id)
        {
            var feature = _featureDal.Get(x => x.Id == id);
            if (feature != null)
                return new SuccessDataResult<Feature>(feature);
            else
                return new ErrorDataResult<Feature>(Messages.FeatureNotFound);
        }

        public IDataResult<List<Feature>> GetByName(string name)
        {
            var features = _featureDal.GetAll(x=>x.Name==name);
            if (features.Any())
                return new SuccessDataResult<List<Feature>>(features);
            else
                return new ErrorDataResult<List<Feature>>(Messages.FeatureNotFound);
        }

        public IResult Update(Feature feature)
        {
            _featureDal.Update(feature);
            return new SuccessResult(Messages.FeatureUpdated);
        }
        
    }
}
