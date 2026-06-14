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
    public class GetMemberActiveSubscriptionQuery : IRequest<IDataResult<Subscription>>
    {
        public int MemberId { get; set; }

        public class GetMemberActiveSubscriptionQueryHandler : IRequestHandler<GetMemberActiveSubscriptionQuery, IDataResult<Subscription>>
        {
            private readonly ISubscriptionRepository _subscriptionRepository;

            public GetMemberActiveSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository)
            {
                _subscriptionRepository = subscriptionRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Subscription>> Handle(GetMemberActiveSubscriptionQuery request, CancellationToken cancellationToken)
            {
                var subscription = await _subscriptionRepository.GetActiveMemberSubscription(request.MemberId);
                return new SuccessDataResult<Subscription>(subscription);
            }
        }
    }
}
