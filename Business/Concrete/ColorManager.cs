using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal colorDal;

        public ColorManager(IColorDal colorDal)
        {
            this.colorDal = colorDal;
        }


        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            colorDal.Add(color);
            return new SuccessResult(Messages.Added);
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color)
        {
            colorDal.Update(color);
            return new SuccessResult(Messages.Updated);
        }


        public IDataResult<List<Color>> GetAll()
        {
            colorDal.GetAll();
            return new SuccessDataResult<List<Color>>(colorDal.GetAll());
        }
    }
}
