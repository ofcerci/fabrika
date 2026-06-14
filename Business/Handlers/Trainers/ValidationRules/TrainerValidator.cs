using Business.Handlers.Trainers.Commands;
using FluentValidation;

namespace Business.Handlers.Trainers.ValidationRules
{
    public class CreateTrainerValidator : AbstractValidator<CreateTrainerCommand>
    {
        public CreateTrainerValidator()
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

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Uzmanlık alanı boş olamaz.")
                .MaximumLength(100);

            RuleFor(x => x.HireDate)
                .NotEmpty()
                .LessThanOrEqualTo(System.DateTime.Today).WithMessage("İşe başlama tarihi bugün veya öncesi olmalıdır.");
        }
    }

    public class UpdateTrainerValidator : AbstractValidator<UpdateTrainerCommand>
    {
        public UpdateTrainerValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir antrenör ID giriniz.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Uzmanlık alanı boş olamaz.");
        }
    }

    public class DeleteTrainerValidator : AbstractValidator<DeleteTrainerCommand>
    {
        public DeleteTrainerValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir antrenör ID giriniz.");
        }
    }
}
