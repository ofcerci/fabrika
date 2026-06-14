using Business.Handlers.Members.Commands;
using FluentValidation;

namespace Business.Handlers.Members.ValidationRules
{
    public class CreateMemberValidator : AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon boş olamaz.")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .LessThan(System.DateTime.Today).WithMessage("Doğum tarihi bugünden önce olmalıdır.");

            RuleFor(x => x.Gender)
                .InclusiveBetween(0, 2).WithMessage("Geçerli bir cinsiyet seçiniz.");
        }
    }

    public class UpdateMemberValidator : AbstractValidator<UpdateMemberCommand>
    {
        public UpdateMemberValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir üye ID giriniz.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon boş olamaz.")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .LessThan(System.DateTime.Today).WithMessage("Doğum tarihi bugünden önce olmalıdır.");
        }
    }

    public class DeleteMemberValidator : AbstractValidator<DeleteMemberCommand>
    {
        public DeleteMemberValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir üye ID giriniz.");
        }
    }
}
