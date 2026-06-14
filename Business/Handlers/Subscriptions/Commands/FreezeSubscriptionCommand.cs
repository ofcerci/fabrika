using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Subscriptions.Commands
{
    public class FreezeSubscriptionCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public DateTime FrozenStartDate { get; set; }
        public DateTime FrozenEndDate { get; set; }

        public class FreezeSubscriptionCommandHandler : IRequestHandler<FreezeSubscriptionCommand, IResult>
        {
            private readonly ISubscriptionRepository _subscriptionRepository;

            public FreezeSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository)
            {
                _subscriptionRepository = subscriptionRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(FreezeSubscriptionCommand request, CancellationToken cancellationToken)
            {
                var subscription = await _subscriptionRepository.GetAsync(s => s.Id == request.Id);
                if (subscription == null)
                    return new ErrorResult(Messages.Unknown);

                var frozenDays = (request.FrozenEndDate - request.FrozenStartDate).Days;

                subscription.Status = 2; // Frozen
                subscription.FrozenStartDate = request.FrozenStartDate;
                subscription.FrozenEndDate = request.FrozenEndDate;
                subscription.EndDate = subscription.EndDate.AddDays(frozenDays);

                _subscriptionRepository.Update(subscription);
                await _subscriptionRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
