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

namespace Business.Handlers.Subscriptions.Queries
{
    public class GetSubscriptionsQuery : IRequest<IDataResult<IEnumerable<Subscription>>>
    {
        public class GetSubscriptionsQueryHandler : IRequestHandler<GetSubscriptionsQuery, IDataResult<IEnumerable<Subscription>>>
        {
            private readonly ISubscriptionRepository _subscriptionRepository;

            public GetSubscriptionsQueryHandler(ISubscriptionRepository subscriptionRepository)
            {
                _subscriptionRepository = subscriptionRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Subscription>>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
            {
                var list = await _subscriptionRepository.GetListAsync();
                return new SuccessDataResult<IEnumerable<Subscription>>(list);
            }
        }
    }
}
