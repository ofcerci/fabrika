using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Fakes.Handlers.Subscriptions
{
    public class CreateSubscriptionInternalCommand : IRequest<IResult>
    {
        public int MemberId { get; set; }
        public int PackageId { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationDays { get; set; }
        public decimal PricePaid { get; set; }
        public string Notes { get; set; }

        public class CreateSubscriptionInternalCommandHandler : IRequestHandler<CreateSubscriptionInternalCommand, IResult>
        {
            private readonly ISubscriptionRepository _subscriptionRepository;

            public CreateSubscriptionInternalCommandHandler(ISubscriptionRepository subscriptionRepository)
            {
                _subscriptionRepository = subscriptionRepository;
            }

            [CacheRemoveAspect()]
            public async Task<IResult> Handle(CreateSubscriptionInternalCommand request, CancellationToken cancellationToken)
            {
                var subscription = new Subscription
                {
                    MemberId = request.MemberId,
                    PackageId = request.PackageId,
                    StartDate = request.StartDate,
                    EndDate = request.StartDate.AddDays(request.DurationDays),
                    PricePaid = request.PricePaid,
                    Status = 1,
                    Notes = request.Notes,
                    CreatedAt = DateTime.Now
                };

                _subscriptionRepository.Add(subscription);
                await _subscriptionRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
