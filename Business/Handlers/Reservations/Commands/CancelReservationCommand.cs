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

namespace Business.Handlers.Reservations.Commands
{
    public class CancelReservationCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand, IResult>
        {
            private readonly IReservationRepository _reservationRepository;

            public CancelReservationCommandHandler(IReservationRepository reservationRepository)
            {
                _reservationRepository = reservationRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
            {
                var reservation = await _reservationRepository.GetAsync(r => r.Id == request.Id);
                if (reservation == null)
                    return new ErrorResult(Messages.Unknown);

                reservation.Status = 2; // Cancelled
                _reservationRepository.Update(reservation);
                await _reservationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
