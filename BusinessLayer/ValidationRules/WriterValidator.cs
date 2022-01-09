using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
   public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar Adını Boş Geçemezsiniz");

            RuleFor(x => x.WriterSurname).NotEmpty().WithMessage("Yazar Soyadı Alanı boş geçilemez");

            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında Alanı boş geçilemez");


            RuleFor(x => x.WriterSurname).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapınız");

            RuleFor(x => x.WriterSurname).MaximumLength(50).WithMessage("Lütfen 50 karakterden fazla değer girişi yapmayınız");

        }
    }
}
