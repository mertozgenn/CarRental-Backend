using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    class CarImageValidator : AbstractValidator<CarImage>
    {
        public CarImageValidator()
        {
            RuleFor(c => c.CarId).NotEmpty();
            RuleFor(c => c.ImagePath).Must(IsImage).WithMessage("Bu format desteklenmiyor");
        }

        private bool IsImage(string filePath)
        {
            string[] imageTypes = { ".jpg", ".jpeg", ".png", };
            foreach (var type in imageTypes)
            {
                if (filePath.Substring(filePath.LastIndexOf('.')).Equals(type))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
