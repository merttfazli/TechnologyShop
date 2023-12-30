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
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand brand)
        {
            var getBrand = _brandDal.Get(x => x.Name == brand.Name);
            if (getBrand != null)
                return new ErrorResult(Messages.BrandAlreadyExists);
            else
            {
                if (checkEquality(brand))
                    return new ErrorResult(Messages.BrandAlreadyExists);
                else
                {
                    _brandDal.Add(brand);
                    return new SuccessResult(Messages.BrandAdded);
                }
            }
        }
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            var brands = _brandDal.GetAll();
            if (brands.Any())
                return new SuccessDataResult<List<Brand>>(brands);
            else
                return new ErrorDataResult<List<Brand>>(Messages.BrandNotFound);
        }

        public IDataResult<Brand> GetById(int id)
        {
            var brand = _brandDal.Get(x => x.Id == id);
            if (brand != null)
                return new SuccessDataResult<Brand>(brand);
            else
                return new ErrorDataResult<Brand>(Messages.BrandNotFound);
        }

        public IResult Update(Brand brand)
        {
            var getBrand = _brandDal.Get(x => x.Name == brand.Name && x.Id != brand.Id);
            if (getBrand != null)
                return new ErrorResult(Messages.BrandAlreadyExists);
            else
            {
                _brandDal.Update(brand);
                return new SuccessResult(Messages.BrandUpdated);
            }
        }
        private bool checkEquality(Brand brand)
        {
            var brands = _brandDal.GetAll();
            foreach (var getBrand in brands)
            {
                if (getBrand.Name.Equals(brand.Name))
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
}
