using CourseApp.Web.Models.Catalogs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Web.Validators
{
    public class CourseUpdateInputValidator:AbstractValidator<UpdateViewModel>
    {
        public CourseUpdateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("isim alanı boş bırakılamaz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("description alanı boş bırakılamaz");
            RuleFor(x => x.Price).NotEmpty().WithMessage("fiyat alanı boş bırakılamaz");
            RuleFor(x => x.Feature.Duration).NotEmpty().WithMessage("süre alanı boş bırakılamaz");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("kategori adı seciniz");

        }
    }
}
