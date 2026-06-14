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
    public class GetPaymentsQuery : IRequest<IDataResult<IEnumerable<Payment>>>
    {
        public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, IDataResult<IEnumerable<Payment>>>
        {
            private readonly IPaymentRepository _paymentRepository;

            public GetPaymentsQueryHandler(IPaymentRepository paymentRepository)
            {
                _paymentRepository = paymentRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Payment>>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
            {
                var list = await _paymentRepository.GetListAsync();
                return new SuccessDataResult<IEnumerable<Payment>>(list);
            }
        }
    }
}
