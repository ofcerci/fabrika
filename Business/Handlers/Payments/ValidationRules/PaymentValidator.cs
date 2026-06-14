using Business.Handlers.Payments.Commands;
using FluentValidation;

namespace Business.Handlers.Payments.ValidationRules
{
    public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentValidator()
        {
            RuleFor(x => x.MemberId)
                .GreaterThan(0).WithMessage("Geçerli bir üye ID giriniz.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalıdır.");

            RuleFor(x => x.PaymentMethod)
                .InclusiveBetween(0, 2).WithMessage("Geçerli bir ödeme yöntemi seçiniz. (0: Nakit, 1: Kredi Kartı, 2: Havale)");
        }
    }
}
