using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Payments.Commands
{
    public class CreatePaymentCommand : IRequest<IResult>
    {
        public int MemberId { get; set; }
        public int? SubscriptionId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMethod { get; set; }
        public string ReferenceNo { get; set; }
        public string Notes { get; set; }

        public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, IResult>
        {
            private readonly IPaymentRepository _paymentRepository;

            public CreatePaymentCommandHandler(IPaymentRepository paymentRepository)
            {
                _paymentRepository = paymentRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
            {
                var payment = new Payment
                {
                    MemberId = request.MemberId,
                    SubscriptionId = request.SubscriptionId,
                    Amount = request.Amount,
                    PaidAt = DateTime.Now,
                    PaymentMethod = request.PaymentMethod,
                    Status = 1, // Completed
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
