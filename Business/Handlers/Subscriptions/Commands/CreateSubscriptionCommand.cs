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
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Subscriptions.Commands
{
    public class CreateSubscriptionCommand : IRequest<IResult>
    {
        public int MemberId { get; set; }
        public int PackageId { get; set; }
        public DateTime StartDate { get; set; }
        public decimal PricePaid { get; set; }
        public string Notes { get; set; }

        public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, IResult>
        {
            private readonly ISubscriptionRepository _subscriptionRepository;
            private readonly IPackageRepository _packageRepository;

            public CreateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IPackageRepository packageRepository)
            {
                _subscriptionRepository = subscriptionRepository;
                _packageRepository = packageRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
            {
                var package = await _packageRepository.GetAsync(p => p.Id == request.PackageId);
                if (package == null)
                    return new ErrorResult(Messages.Unknown);

                var subscription = new Subscription
                {
                    MemberId = request.MemberId,
                    PackageId = request.PackageId,
                    StartDate = request.StartDate,
                    EndDate = request.StartDate.AddDays(package.DurationDays),
                    PricePaid = request.PricePaid,
                    Status = 1, // Active
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
