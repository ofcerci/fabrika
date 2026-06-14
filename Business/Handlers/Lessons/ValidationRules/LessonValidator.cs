using Business.Handlers.Lessons.Commands;
using FluentValidation;

namespace Business.Handlers.Lessons.ValidationRules
{
    public class CreateLessonValidator : AbstractValidator<CreateLessonCommand>
    {
        public CreateLessonValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ders adı boş olamaz.")
                .MaximumLength(100);

            RuleFor(x => x.TrainerId)
                .GreaterThan(0).WithMessage("Geçerli bir antrenör seçiniz.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Başlangıç saati boş olamaz.");

            RuleFor(x => x.EndTime)
                .NotEmpty()
                .GreaterThan(x => x.StartTime).WithMessage("Bitiş saati, başlangıç saatinden sonra olmalıdır.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Kapasite en az 1 olmalıdır.")
                .LessThanOrEqualTo(500).WithMessage("Kapasite 500'den fazla olamaz.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Konum boş olamaz.")
                .MaximumLength(100);
        }
    }

    public class DeleteLessonValidator : AbstractValidator<DeleteLessonCommand>
    {
        public DeleteLessonValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ders ID giriniz.");
        }
    }
}
