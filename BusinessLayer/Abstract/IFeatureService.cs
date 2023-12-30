using BusinessLayer.Utilities.Results;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IFeatureService
    {
        IResult Add(Feature feature);
        IResult Delete(Feature feature);
        IResult Update(Feature feature);
        IDataResult<List<Feature>> GetAll();
        IDataResult<Feature> GetById(int id);
        IDataResult<List<Feature>> GetByName(string name);
    }
}
