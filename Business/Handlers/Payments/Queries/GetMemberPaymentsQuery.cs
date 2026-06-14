using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Payments.Queries
{
    public class GetMemberPaymentsQuery : IRequest<IDataResult<IEnumerable<Payment>>>
    {
        public int MemberId { get; set; }

        public class GetMemberPaymentsQueryHandler : IRequestHandler<GetMemberPaymentsQuery, IDataResult<IEnumerable<Payment>>>
        {
            private readonly IPaymentRepository _paymentRepository;

            public GetMemberPaymentsQueryHandler(IPaymentRepository paymentRepository)
            {
                _paymentRepository = paymentRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Payment>>> Handle(GetMemberPaymentsQuery request, CancellationToken cancellationToken)
            {
                var list = await _paymentRepository.GetMemberPayments(request.MemberId);
                return new SuccessDataResult<IEnumerable<Payment>>(list);
            }
        }
    }
}
