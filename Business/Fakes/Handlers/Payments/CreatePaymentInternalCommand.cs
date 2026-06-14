using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Fakes.Handlers.Payments
{
    public class CreatePaymentInternalCommand : IRequest<IResult>
    {
        public int MemberId { get; set; }
        public int? SubscriptionId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMethod { get; set; }
        public string ReferenceNo { get; set; }
        public string Notes { get; set; }

        public class CreatePaymentInternalCommandHandler : IRequestHandler<CreatePaymentInternalCommand, IResult>
        {
            private readonly IPaymentRepository _paymentRepository;

            public CreatePaymentInternalCommandHandler(IPaymentRepository paymentRepository)
            {
                _paymentRepository = paymentRepository;
            }

            public async Task<IResult> Handle(CreatePaymentInternalCommand request, CancellationToken cancellationToken)
            {
                var payment = new Payment
                {
                    MemberId = request.MemberId,
                    SubscriptionId = request.SubscriptionId,
                    Amount = request.Amount,
                    PaidAt = DateTime.Now,
                    PaymentMethod = request.PaymentMethod,
                    Status = 1,
                    ReferenceNo = request.ReferenceNo,
                    Notes = request.Notes
                };

                _paymentRepository.Add(payment);
                await _paymentRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
