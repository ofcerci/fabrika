using Business.Handlers.Attendances.Commands;
using FluentValidation;

namespace Business.Handlers.Attendances.ValidationRules
{
    public class CreateAttendanceValidator : AbstractValidator<CreateAttendanceCommand>
    {
        public CreateAttendanceValidator()
        {
            RuleFor(x => x.MemberId)
                .GreaterThan(0).WithMessage("Geçerli bir üye ID giriniz.");
        }
    }

    public class CheckOutValidator : AbstractValidator<CheckOutCommand>
    {
        public CheckOutValidator()
        {
            RuleFor(x => x.AttendanceId)
                .GreaterThan(0).WithMessage("Geçerli bir giriş kaydı ID giriniz.");
        }
    }
}
