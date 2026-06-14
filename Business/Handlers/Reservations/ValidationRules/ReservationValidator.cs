using Business.Handlers.Reservations.Commands;
using FluentValidation;

namespace Business.Handlers.Reservations.ValidationRules
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationValidator()
        {
            RuleFor(x => x.MemberId)
                .GreaterThan(0).WithMessage("Geçerli bir üye ID giriniz.");

            RuleFor(x => x.LessonId)
                .GreaterThan(0).WithMessage("Geçerli bir ders ID giriniz.");
        }
    }

    public class CancelReservationValidator : AbstractValidator<CancelReservationCommand>
    {
        public CancelReservationValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir rezervasyon ID giriniz.");
        }
    }
}
