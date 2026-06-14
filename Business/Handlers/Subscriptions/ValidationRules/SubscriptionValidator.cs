using Business.Handlers.Subscriptions.Commands;
using FluentValidation;

namespace Business.Handlers.Subscriptions.ValidationRules
{
    public class CreateSubscriptionValidator : AbstractValidator<CreateSubscriptionCommand>
    {
        public CreateSubscriptionValidator()
        {
            RuleFor(x => x.MemberId)
                .GreaterThan(0).WithMessage("Geçerli bir üye ID giriniz.");

            RuleFor(x => x.PackageId)
                .GreaterThan(0).WithMessage("Geçerli bir paket ID giriniz.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Başlangıç tarihi boş olamaz.");

            RuleFor(x => x.PricePaid)
                .GreaterThanOrEqualTo(0).WithMessage("Ödenen tutar negatif olamaz.");
        }
    }

    public class FreezeSubscriptionValidator : AbstractValidator<FreezeSubscriptionCommand>
    {
        public FreezeSubscriptionValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir abonelik ID giriniz.");

            RuleFor(x => x.FrozenStartDate)
                .NotEmpty().WithMessage("Dondurma başlangıç tarihi boş olamaz.");

            RuleFor(x => x.FrozenEndDate)
                .NotEmpty()
                .GreaterThan(x => x.FrozenStartDate).WithMessage("Dondurma bitiş tarihi, başlangıç tarihinden sonra olmalıdır.");
        }
    }
}
