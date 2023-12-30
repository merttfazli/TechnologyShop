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
using Color = EntityLayer.Concrete.Color;

namespace BusinessLayer.Concrete
{
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }
        public IResult Add(Color color)
        {
            var getColor = _colorDal.Get(x => x.Name == color.Name);
            if (getColor != null)
                return new ErrorResult(Messages.ColorAlreadyExists);
            else
            {
                if (checkEquality(color))
                    return new ErrorResult(Messages.ColorAlreadyExists);
                else
                {
                    _colorDal.Add(color);
                    return new SuccessResult(Messages.ColorAdded);
                }
            }
        }
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }
        public IDataResult<List<Color>> GetAll()
        {
            var colors = _colorDal.GetAll();
            if (colors.Any())
                return new SuccessDataResult<List<Color>>(colors);
            else
                return new ErrorDataResult<List<Color>>(Messages.ColorNotFound);
        }
        public IDataResult<Color> GetById(int id)
        {
            var color = _colorDal.Get(x => x.Id == id);
            if (color != null)
                return new SuccessDataResult<Color>(color);
            else
                return new ErrorDataResult<Color>(Messages.ColorNotFound);
        }
        public IResult Update(Color color)
        {
            var getColor = _colorDal.Get(x => x.Name == color.Name && x.Id != color.Id);
            if (getColor != null)
                return new ErrorResult(Messages.ColorAlreadyExists);
            else
            {
                _colorDal.Update(color);
                return new SuccessResult(Messages.ColorUpdated);
            }
        }
        private bool checkEquality(Color color)
        {
            var colors = _colorDal.GetAll();
            foreach (var getColor in colors)
            {
                if (getColor.Name.Equals(color.Name))
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
}
